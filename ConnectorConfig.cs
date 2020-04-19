using System;
using Microsoft.Extensions.Configuration;

namespace com.businesscentral
{

    public partial class ConnectorConfig
    {
        public ConnectorConfig(IConfigurationRoot config)
        {
            if (config != null)
            {
                tenant = config["tenant"];
                companyID = config["companyID"];
                apiVersion1 = config["apiVersion1"];
                apiVersion2 = config["apiVersion2"];
                authInfo = config["authInfo"];

                shopifyApiKey = config["shopifyApiKey"];
                shopifyApiPassword = config["shopifyApiPassword"];
                shopifyShopName = config["shopifyShopName"];
                shopifyApiUrl = config["shopifyApiUrl"];
            }
            // If you are customizing here it means you
            //  should give a look on how use
            //  azure configuration file
            if (String.IsNullOrEmpty(tenant))
                tenant = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxdxxxxxxx/Sandbox";
            if (String.IsNullOrEmpty(companyID))
                companyID = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";
            if (String.IsNullOrEmpty(apiVersion1))
                apiVersion1 = "v2.0";
            if (String.IsNullOrEmpty(apiVersion2))
                apiVersion2 = "v1.0";
            if (String.IsNullOrEmpty(authInfo))
                authInfo = "your_username:yout_web_service_access_key";

            if (String.IsNullOrEmpty(shopifyApiKey))
                shopifyApiKey = "your_shopify_api_username";
            if (String.IsNullOrEmpty(shopifyApiPassword))
                shopifyApiPassword = "your_shopify_api_password";
            if (String.IsNullOrEmpty(shopifyShopName))
                shopifyShopName = "{your_name}.myshopify.com";
            if (String.IsNullOrEmpty(shopifyApiUrl))
                shopifyApiUrl = "admin/api/2020-04";
        }

        public String tenant;
        public String companyID;
        public String apiVersion1;
        public String apiVersion2;
        public String authInfo;
        public String shopifyApiKey;
        public String shopifyApiPassword;
        public String shopifyShopName;
        public String shopifyApiUrl;

    }
}
