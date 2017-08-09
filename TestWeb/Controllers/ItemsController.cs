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
using Colour = Catalog.Dal.Entities.Colour;
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

            var products = GetMyFuckingProducts();

            return View(products);
        }

        private IEnumerable<Product> GetMyFuckingProducts() {
            
            //tahle promena bude zaviset na tom, kolik produktu se zobrazi na jedne strance
            var productsQnt = 5;
            List<Product> myFuckingProducts = new List<Product>();

            for (var i = 0; i < productsQnt; i++) {

                //vytahuju produkt
                var product = _catalogService.GetProduct(i);
                var colorsOfProduct = _catalogService.GetRgb(product.id_pr);
                var howManyColorsForProduct = colorsOfProduct.Count();

                List<Catalog.Dal.Entities.Colour> fuckingColorsOfProduct = new List<Catalog.Dal.Entities.Colour>();
                
                //vytahuju barvy aktualniho produktu
                for (var j = 0; j < howManyColorsForProduct; j++) {
                    fuckingColorsOfProduct.Add( _catalogService.GetColour(colorsOfProduct[j].rgb) );
                }

                var fuckingProduct = new Product() {
                    name = product.name,
                    date = product.date,
                    price = product.price,
                    description = product.description,
                    colour = new string[howManyColorsForProduct]
                };

                for (var j = 0; j < howManyColorsForProduct; j++) {
                    fuckingProduct.colour[j] = fuckingColorsOfProduct[j].name;
                }

                myFuckingProducts.Add(fuckingProduct);
            }

            return myFuckingProducts;
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
