using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Controllers
{
    public class Item
    {
        private Product p = new Product();
        private int quantity;

        public Item() { }

        public Item(Product product, int quantity)
        {
            this.p = product;
            this.quantity = quantity;
        }

        public int Quantity { get => quantity; set => quantity = value; }
        public Product P { get => p; set => p = value; }
    }
}
