using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PernicekWeb.Views.ViewModel;
using Catalog.Dal.Repository;

namespace PernicekWeb.Controllers
{
    public class ProductColourController : Controller
    {
        public IActionResult Index()
        {
            var myViewModel = new ProductColourViewModel();
            myViewModel.Products = GetAllProducts()
            {
                
            }
            return View(myViewModel);
        }
    }
}