using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace com.businesscentral
{
    public class ShopifyConnector
    {
        private ConnectorConfig config;
        private string ShopifyBaseApi = string.Empty;
        public ShopifyConnector(ConnectorConfig config)
        {
            this.config = config;
            this.ShopifyBaseApi = String.Format("https://{0}:{1}@{2}/{3}/",
                                    config.shopifyApiKey,
                                    config.shopifyApiPassword,
                                    config.shopifyShopName,
                                    config.shopifyApiPassword);

        }
        public async Task<ShopifyCustomer> PostShopifyCustomers(ShopifyCustomer inputCustomer)
        {
            ShopifyCustomer shopifyCustomer = null;
            var apiEndPoint = String.Format("{0}/{1}", this.ShopifyBaseApi, "/customers.json");

            using (var httpClient = new HttpClient())
            {
                var jsonObject = JsonConvert.SerializeObject(inputCustomer);
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var responseMessage = httpClient.PostAsync(apiEndPoint, content).Result;

                if (responseMessage.IsSuccessStatusCode)
                    shopifyCustomer = JsonConvert.DeserializeObject<ShopifyCustomer>(await responseMessage.Content.ReadAsStringAsync());
            }
            return shopifyCustomer;
        }

    }

}
