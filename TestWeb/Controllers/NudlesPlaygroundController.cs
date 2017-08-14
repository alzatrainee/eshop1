using Catalog.Business;
using Catalog.Dal.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Controllers
{
    public class NudlesPlaygroundController : Controller
    {
        private readonly CatalogService _catalogservice;
        public NudlesPlaygroundController(CatalogService catalogservice)
        {
            _catalogservice = catalogservice;
        }
        public IActionResult Index()
        {
            List<Product> products = _catalogservice.GetAllProducts();
            HttpContext.Items["products"] = products;
            return View();
        }

    }
}
