using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace com.businesscentral
{
    public partial class ShopifyCustomers
    {
        [JsonProperty("customer")]
        public ShopifyCustomer Customer { get; set; }
    }

    public partial class ShopifyCustomer
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("accepts_marketing")]
        public bool AcceptsMarketing { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("orders_count")]
        public long OrdersCount { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("total_spent")]
        public string TotalSpent { get; set; }

        [JsonProperty("last_order_id")]
        public object LastOrderId { get; set; }

        [JsonProperty("note")]
        public object Note { get; set; }

        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; }

        [JsonProperty("multipass_identifier")]
        public object MultipassIdentifier { get; set; }

        [JsonProperty("tax_exempt")]
        public bool TaxExempt { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("last_order_name")]
        public object LastOrderName { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("addresses")]
        public List<Address> Addresses { get; set; }

        [JsonProperty("accepts_marketing_updated_at")]
        public DateTimeOffset AcceptsMarketingUpdatedAt { get; set; }

        [JsonProperty("marketing_opt_in_level")]
        public object MarketingOptInLevel { get; set; }

        [JsonProperty("tax_exemptions")]
        public List<object> TaxExemptions { get; set; }

        [JsonProperty("admin_graphql_api_id")]
        public string AdminGraphqlApiId { get; set; }

        [JsonProperty("default_address")]
        public ShopifyAddress DefaultAddress { get; set; }
    }

    public partial class ShopifyAddress
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("company")]
        public object Company { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public object Address2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("province_code")]
        public string ProvinceCode { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }
    }
}
