using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog.Business;


namespace PernicekWeb.Controllers
{
    public class CartController : Controller
    {
        List<Item> cart = new List<Item>();
        public readonly CatalogService _catalogservice;

        public CartController(CatalogService catalogservice)
        {
            _catalogservice = catalogservice;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Check if an item exists in cart 
        private int Contains(int id, List<Item> cart)
        {
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].P.id_pr == id)
                    return i;
            return -1;
        }
        // Add item to cart
        public ActionResult Order(int id)
        {
            
            if (cart == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(_catalogservice.GetProduct(id),1));
            }
            else
            {
                int index = Contains(id, cart);
                if (index == -1)
                    cart.Add(new Item(_catalogservice.GetProduct(id), 1));
                else
                    cart[index].Quantity++;
            }
            return View(cart);

        }

        public ActionResult RemoveFromCart(int id)
        {
            throw new NotImplementedException();
        }
    }
}