using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace com.businesscentral
{
    public static class CustomerWebHook
    {
        [FunctionName("CustomerWebHook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequest req,
            ILogger log,
            ExecutionContext context)
        {
            if (!req.Method.Equals("GET") && !req.Method.Equals("POST"))
                return new BadRequestObjectResult("Unexpected " + req.Method + " request");

            // Validation token for webhook registration
            //  reply token to accept webhoob subcriprion
            string validationToken = req.Query["validationToken"];
            if (validationToken != null)
            {
                dynamic data = JsonConvert.SerializeObject(validationToken);
                return new ContentResult { Content = data, ContentType = "application/json; charset=utf-8", StatusCode = 200 };
            }

            // Webhook 
            log.LogInformation("WebHook received");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation("WebHook : " + requestBody);
            var ev = !String.IsNullOrEmpty(requestBody) ? JsonConvert.DeserializeObject<WebHookEvents>(requestBody) : null;

            // Load configuration
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var config = new ConnectorConfig(configBuilder);
            var centraConnector = new BusinessCentralConnector(config, log);
            var customerConverter = new CustomerConverter();
            var shopifyConnector = new ShopifyConnector(config);

            if (!(ev == null || ev.Value == null || ev.Value.Count == 0))
            {
                foreach (var customer in ev.Value.Where(a => a.ChangeType.Equals("created")))
                {
                    // Business Central is queried to get customer detail
                    log.LogInformation("Get customer");
                    var bcCustomer = await centraConnector.GetCustomerByWebhook(customer);
                    if (bcCustomer == null)
                    {
                        log.LogError("Cannot get customer from BC");
                        break;
                    }

                    // Conversion between BC and Shopify customer entity
                    log.LogInformation("Convert entity");
                    ShopifyCustomer shopifyCustomer = customerConverter.ToShopify(bcCustomer);
                    if (shopifyCustomer == null)
                    {
                        log.LogError("Cannot convert customer");
                        break;
                    }

                    // Post new customer to Shopify
                    log.LogInformation("Post customer");
                    await shopifyConnector.PostShopifyCustomers(shopifyCustomer);
                }
            }
            return new StatusCodeResult(200);
        }
    }

}
