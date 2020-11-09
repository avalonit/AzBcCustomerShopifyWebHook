
namespace com.businesscentral
{

    public partial class CustomerConverter
    {
        public ShopifyCustomer ToShopify(Customer customer)
        {
            if (customer == null)
                return null;

            var shopifyCustomer = new ShopifyCustomer();

            shopifyCustomer.FirstName = customer.Number.ToString();

            if (!string.IsNullOrEmpty(customer.DisplayName))
                shopifyCustomer.LastName = customer.DisplayName;

            if (!string.IsNullOrEmpty(customer.PhoneNumber))
                shopifyCustomer.Phone = customer.PhoneNumber;

            if (!string.IsNullOrEmpty(customer.Email))
                shopifyCustomer.Email = customer.Email;

            return shopifyCustomer;
        }
    }


}
