using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dal.Repository.Abstraction;
using Alza.Module.Catalog.Dal.Repository.Implementation.Ado;
using Microsoft.AspNetCore.Mvc;
using Catalog.Dal.Repository;
using Catalog.Business;
using PernicekWeb.Models.ManageViewModels;

namespace PernicekWeb.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductRepository _catalogservice;

        public CatalogController(IProductRepository catalogservice)
        {

           _catalogservice = catalogservice;
        }

        public IActionResult IndexViewCatalog() {

            var myViewModel = new CatalogViewModel();
            myViewModel.Products = _catalogservice.QueryGetProducts();

            return View(myViewModel);
        }
    }
}