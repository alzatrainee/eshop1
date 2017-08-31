using Catalog.Dal.Entities;
using Module.Business.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Business.Business.Entity
{
    public class CartItemBO
    {
        public CartItemBO(Cart_pr item, Product product, Size size, Colour colour)
        {
            this.item = item;
            this.size = size;
            this.colour = colour;
            this.product = product;
        }

        public CartItemBO() { }

        public Cart_pr item { get; set; }
        public Product product { get; set; }
        public Colour colour { get; set; }
        public Size size { get; set; }
    }
}
