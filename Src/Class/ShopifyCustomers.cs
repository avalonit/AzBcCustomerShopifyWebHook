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
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; }

        //[JsonProperty("addresses")]
        //public List<Address> Addresses { get; set; }
    }

    public partial class ShopifyAddress
    {
        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
