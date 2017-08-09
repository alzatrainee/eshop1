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

        public ActionResult AddToCart(int id)
        {
            //var addedItem = _catalogservice.GetProduct(id);

            throw new NotImplementedException();
        }

        public ActionResult RemoveFromCart(int id)
        {
            throw new NotImplementedException();
        }
    }
}