using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dal.Context;
using Microsoft.EntityFrameworkCore;
using Pernicek.Models.PlaygroundViewModels;
using Catalog.Dal.Repository.Abstraction;
using PernicekWeb.Models.PlaygroundViewModels;

namespace Pernicek.Controllers
{
    public class PlaygroundController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly IProductRepository _iProductRepository;
        private readonly Iprod_colRepository _iprod_colRepository;

        public PlaygroundController(CatalogService catalogservice, IProductRepository iProductRepository, Iprod_colRepository iprod_colRepository)
        {
            _iProductRepository = iProductRepository;
            _catalogService = catalogservice;
            _iprod_colRepository = iprod_colRepository;
        }

        public IActionResult Index()
        {
            
            var result = _catalogService.GetProduct(0);
            var res = _catalogService.GetRgb(result.id_pr);
            var pom = _catalogService.GetColour(res.rgb);

            var model = new Product
            {
                colour = pom.name,
                name = result.name,
                date = result.date,
                price = result.price,
                description = result.description
            };
            return View(model);
            
            
        }
    }
}
