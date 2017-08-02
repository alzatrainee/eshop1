using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dal.Context;
using Microsoft.EntityFrameworkCore;
using Pernicek.Models.PlaygroundViewModels;

namespace Pernicek.Controllers
{
    public class PlaygroundController : Controller
    {
        private readonly CatalogService _catalogservice;

        public PlaygroundController(CatalogService catalogservice)
        {
           
            _catalogservice = catalogservice;
        }

        public IActionResult Index()
        {

            //var result = _catalogservice.QueryGetProducts().data;
            //var result = _catalogservice.FindByName("nude").data;
            ViewData["Products"] = "Ahoj";
            ViewData["Colour"] = "Cau";
           
            return View();
            
            
        }
    }
}
