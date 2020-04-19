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
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var ev = !String.IsNullOrEmpty(requestBody) ? JsonConvert.DeserializeObject<WebHookEvent>(requestBody) : null;

            // Load configuration
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Business Central is queried to get order and sale agent detail 
            var config = new ConnectorConfig(configBuilder);
            BusinessCentralConnector centraConnector = new BusinessCentralConnector(config);
            var order = await centraConnector.GetCustomerByWebhook(ev);


            return new StatusCodeResult(200);
        }
    }

}