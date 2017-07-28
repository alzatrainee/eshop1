using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pernicek.Controllers
{
    public class PlaygroundController : Controller
    {
        CatalogService _catalogservice;
        public PlaygroundController(CatalogService catalogservice)
        {
            _catalogservice = catalogservice;
        }

        public IActionResult Index()
        {
            ViewData["Colour"] = _catalogservice.getAllColours().data;
            return View();
        }
    }
}
