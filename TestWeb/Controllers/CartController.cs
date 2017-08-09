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
        public readonly CatalogService _catalogservice;

        public CartController(CatalogService catalogservice)
        {
            _catalogservice = catalogservice;
        }
        public IActionResult Index()
        {
            return View();
        }
        // Add item to cart

        public ActionResult AddToCart(int id)
        {
            if (ViewData["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(_catalogservice.GetProduct(id),1));
                ViewData["cart"] = cart;
            }
            else
            {

            }
            return View("Cart");

        }

        public ActionResult RemoveFromCart(int id)
        {
            throw new NotImplementedException();
        }
    }
}