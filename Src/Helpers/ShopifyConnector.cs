using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace com.businesscentral
{
    public class ShopifyConnector
    {
        private ConnectorConfig config;
        private ILogger log;
        private string ShopifyBaseApi = string.Empty;
        private string ShopifyAuthInfo = string.Empty;
        
        public ShopifyConnector(ConnectorConfig config, ILogger log)
        {
            this.config = config;
            this.log = log;
            this.ShopifyBaseApi = String.Format("https://{0}{1}",
                                    config.shopifyShopName,
                                    config.shopifyApiUrl);
            this.ShopifyAuthInfo = String.Format("{0}:{1}",
                                    config.shopifyApiKey,
                                    config.shopifyApiPassword);
            this.ShopifyAuthInfo = Convert.ToBase64String(Encoding.Default.GetBytes(this.ShopifyAuthInfo));
        }
        public async Task<ShopifyCustomer> PostShopifyCustomers(ShopifyCustomer inputCustomer)
        {
            ShopifyCustomer shopifyCustomer = null;
            var apiEndPoint = String.Format("{0}{1}", this.ShopifyBaseApi, "customers.json");
            log.LogInformation("PostShopifyCustomers HTTP " + apiEndPoint);
            log.LogInformation("PostShopifyCustomers ShopifyAuthInfo " + ShopifyAuthInfo);

            using (var httpClient = new HttpClient())
            {
                var jsonObject = "{\"customer\": " + JsonConvert.SerializeObject(inputCustomer) + "}";
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                log.LogInformation("PostShopifyCustomers " + jsonObject);

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ShopifyAuthInfo);
                var responseMessage = httpClient.PostAsync(apiEndPoint, content).Result;

                if (responseMessage.IsSuccessStatusCode)
                    shopifyCustomer = JsonConvert.DeserializeObject<ShopifyCustomer>(await responseMessage.Content.ReadAsStringAsync());
                else
                    log.LogError("PostShopifyCustomers HTTP error: " + responseMessage.StatusCode.ToString());

            }
            return shopifyCustomer;
        }

    }

}
