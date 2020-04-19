using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Logging;

namespace com.businesscentral
{
    public class ShopifyConnector
    {
        private ConnectorConfig config;
        private ILogger log;
        private string ShopifyBaseApi = string.Empty;
        public ShopifyConnector(ConnectorConfig config, ILogger log)
        {
            this.config = config;
            this.log = log;
            this.ShopifyBaseApi = String.Format("https://{0}:{1}@{2}/{3}/",
                                    config.shopifyApiKey,
                                    config.shopifyApiPassword,
                                    config.shopifyShopName,
                                    config.shopifyApiUrl);

        }
        public async Task<ShopifyCustomer> PostShopifyCustomers(ShopifyCustomer inputCustomer)
        {
            ShopifyCustomer shopifyCustomer = null;
            var apiEndPoint = String.Format("{0}/{1}", this.ShopifyBaseApi, "customers.json");
            log.LogInformation("PostShopifyCustomers HTTP " + apiEndPoint);

            using (var httpClient = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(inputCustomer);
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var responseMessage = httpClient.PostAsync(apiEndPoint, content).Result;

                if (responseMessage.IsSuccessStatusCode)
                    shopifyCustomer = JsonConvert.DeserializeObject<ShopifyCustomer>(await responseMessage.Content.ReadAsStringAsync());
                else
                    log.LogError("GetCustomerByWebhook HTTP error" + responseMessage.StatusCode.ToString());

            }
            return shopifyCustomer;
        }

    }

}
