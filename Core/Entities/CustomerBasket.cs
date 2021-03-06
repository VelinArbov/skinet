using System.Collections.Generic;

namespace Core.Entities
{
    public class CustomerBasket
    {
          public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        //Need for Stripe

        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }

        public string PaymentIntentId { get; set; }

        //Shipping

        public decimal ShippingPrice { get; set; }
    }
}