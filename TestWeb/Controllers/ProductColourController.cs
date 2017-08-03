using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dal.Repository.Abstraction;
using Alza.Module.Catalog.Dal.Repository.Implementation.Ado;
using Microsoft.AspNetCore.Mvc;
using PernicekWeb.Views.ViewModel;
using Catalog.Dal.Repository;
using Catalog.Business;

namespace PernicekWeb.Controllers
{
    public class ProductColourController : Controller
    {
        private readonly IProductRepository _catalogservice;

        public ProductColourController(IProductRepository catalogservice)
        {

           _catalogservice = catalogservice;
        }

//        public ProductColourController(ProductRepository catalogservice)
//        {
//
//            _catalogservice = catalogservice;
//        }

        public IActionResult Index() {

            var myViewModel = new ProductColourViewModel();
            myViewModel.Products = _catalogservice.GetAllProducts();

            return View(myViewModel);
        }
    }
}