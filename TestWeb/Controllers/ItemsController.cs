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
using Size = Catalog.Dal.Entities.Size;

namespace PernicekWeb.Controllers
{
    public class ItemsController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly IProductRepository _iProductRepository;
        private readonly Iprod_colRepository _iprod_colRepository;
        private readonly CatalogDbContext _context;

        public ItemsController(CatalogService catalogservice, IProductRepository iProductRepository, Iprod_colRepository iprod_colRepository, CatalogDbContext context)
        {
            _iProductRepository = iProductRepository;
            _catalogService = catalogservice;
            _iprod_colRepository = iprod_colRepository;
            _context = context;
        }

        //
        // POST: /Items/Browse
        public IActionResult Browse() {

            var products = _catalogService.GetProduct(0);
            var colors = _catalogService.GetRgb(products.id_pr);
            var colorQnt = colors.Count();

            List<Catalog.Dal.Entities.Colour> tmpColor = new List<Catalog.Dal.Entities.Colour>();

            for (var i = 0; i < colorQnt; i++) {
                tmpColor.Add( _catalogService.GetColour(colors[i].rgb) );
            }

            List<Product> catalogOfProducts = new List<Product>();

            foreach ( var product in _catalogService.GetProduct() ) {
                
            }

//            var product = new Product() {
//                
//                name = products.name,
//                date = products.date,
//                price = products.price,
//                description = products.description,
//                colour = new string[colorQnt]
//            };
//
//            for (var i = 0; i < colorQnt; i++) {
//                product.colour[i] = tmpColor[i].name;
//            }

            return View(product);
        }

        public IActionResult Item()
        {
           return View();
        }


        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var item = await _context.Product.ToListAsync();
            return View(item);
        }
    }
}
