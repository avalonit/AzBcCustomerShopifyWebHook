using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace com.businesscentral
{
    public class BusinessCentralConnector
    {
        private ConnectorConfig config;
        private ILogger log;
        private string ApiWebHookEndPoint = string.Empty;
        private string ApiEndPoint = string.Empty;
        private string AuthInfo = string.Empty;
        public BusinessCentralConnector(ConnectorConfig config, ILogger log)
        {
            this.config = config;
            this.log = log;
            this.ApiWebHookEndPoint = String.Format("https://api.businesscentral.dynamics.com/{0}/{1}/", config.apiVersion1, config.tenant);
            this.ApiEndPoint = String.Format("https://api.businesscentral.dynamics.com/{0}/{1}/api/{2}/companies({3})/",
                                    config.apiVersion1, config.tenant, config.apiVersion2, config.companyID);

            this.AuthInfo = Convert.ToBase64String(Encoding.Default.GetBytes(config.authInfo));
        }
        public async Task<Customer> GetCustomerByWebhook(WebHookEvent ev)
        {
            Customer customer = null;

            if (ev == null)
                return null;

            log.LogInformation("GetCustomerByWebhook HTTP (id=): " + ev.Resource);
            var apiEndPoint = this.ApiWebHookEndPoint + ev.Resource;
            log.LogInformation("GetCustomerByWebhook HTTP: " + apiEndPoint);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", this.AuthInfo);
                var responseMessage = await httpClient.GetAsync(apiEndPoint);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var json = await responseMessage.Content.ReadAsStringAsync();
                    log.LogInformation("GetCustomerByWebhook customer json: " + json);
                    customer = JsonConvert.DeserializeObject<Customer>(json);
                }
                else
                    log.LogError("GetCustomerByWebhook HTTP error" + responseMessage.StatusCode.ToString());
            }
            return customer;
        }

    }

}
