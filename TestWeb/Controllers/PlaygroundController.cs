using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Catalog.Dal.Context;
using Catalog.Dal.Repository.Abstraction;
using PernicekWeb.Models.PlaygroundViewModels;
using Pernicek.Models.PlaygroundViewModels;

namespace Pernicek.Controllers
{
    public class PlaygroundController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly IProductRepository _iProductRepository;
        private readonly Iprod_colRepository _iprod_colRepository;
        private readonly IProd_siRepository _iProd_siRepository;
        private readonly IImageRepository _iImageRepository;
        private readonly IFirmRepository _iFirmRepository;
        private readonly IProduct_catRepository _iProduct_catRepository;
        private readonly CatalogDbContext _context;

        //private readonly ICategoryRepository _iCategoryRepository;

        public PlaygroundController(CatalogService catalogservice, IProductRepository iProductRepository, Iprod_colRepository iprod_colRepository, IProd_siRepository iProd_siRepository,
                                    IImageRepository iImageRepository, IFirmRepository iFirmRepository, IProduct_catRepository iProduct_catRepository, CatalogDbContext context)
        {
            _iProductRepository = iProductRepository;
            _catalogService = catalogservice;
            _iprod_colRepository = iprod_colRepository;
            _iProd_siRepository = iProd_siRepository;
            _iImageRepository = iImageRepository;
            _iFirmRepository = iFirmRepository;
            _iProduct_catRepository = iProduct_catRepository;
            _context = context;
        }
        
        public IActionResult Index(int? Payment, PlaygroundViewModel model)
        {
            var ulice = model.street;
            var number = model.house_number;
            var city = model.city;
            var psc = model.post_code;
            return View();
        }

        


        /*
        public IActionResult CategoryFilter (int? id, int? ide) 
        {
            List<Product> Products = new List<Product>();
            // var allProducts = _catalogService.GetAllProducts();
            var cate = _catalogService.GetProductCategory(id.Value);
            var res = _catalogService.Get_ProductId(cate.id_cs);
            for (var i = 0; i < res.Count(); i++)
            {


                var result = _catalogService.GetProduct(res[i].id_pr);
                var image = _catalogService.GetImage(result.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                var firm = _catalogService.GetFirm(result.id_fir);

                var model = new Product
                {
                    name = result.name,
                    price = result.price,
                    firm = firm.name,
                    image = image.link,
                    id_pr = res[i].id_pr,
                    id_fir = result.id_fir
                };
                Products.Add(model);
                //  Products = Products.Where(s => s.id_pr == ide.Value).ToList();
            }
            if (ide != null)
            {
                Products = Products.Where(s => s.id_fir == ide.Value).ToList();
            }

            return View(Products);
        }

        public IActionResult FilterAll(int? ide, int? id)
        {
            List<Product> Products = new List<Product>();
            var allProducts = _catalogService.GetAllProducts();
            var velAllProducts = allProducts.Count();
            foreach (var product in allProducts)
            {
                // var result = _catalogService.GetProduct(product.id_pr);
                var image = _catalogService.GetImage(product.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                var firm = _catalogService.GetFirm(product.id_fir);

                var model = new Product
                {
                    name = product.name,
                    price = product.price,
                    firm = firm.name,
                    image = image.link,
                    id_fir = product.id_fir
                };
                Products.Add(model);
            }
            
            if (ide != null)
            {
                Products = Products.Where(s => (s.id_fir == ide.Value)).ToList();
            }
            ViewData["Firmy"] = ide.Value;
            if (id != null)
            {
                Products = Products.Where(s => (s.id_fir == ide.Value) || (s.id_fir == id.Value)).ToList();
            }

            return View(Products);
        }
    
    */

       

        public IActionResult Filterss (string returnUrl = null)
        {
            var col = _catalogService.getAllColours();
            
            int i = 1;
            List<Product> Products = new List<Product>();
            var allProducts = _catalogService.GetAllProducts();
            var velAllProducts = allProducts.Count();
            foreach (var product in allProducts)
            {
                // var result = _catalogService.GetProduct(product.id_pr);
                var image = _catalogService.GetImage(product.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                var firm = _catalogService.GetFirm(product.id_fir);

                var model = new Product
                {
                    name = product.name,
                    price = product.price,
                    firm = firm.name,
                    image = image.link
                };
                Products.Add(model);
                model.ProductFilter = Products;
                model.Colours = col;
                
               
                if (velAllProducts < ++i)
                {
                    return View(model);
                }
            }
            
            return View(Products);
        }

        public IActionResult Category(int? id, FilterProduct model, string[] Colours, int[] Firms, int[] Sizes)
        {            
            var col = _catalogService.getAllColours();
            model.Colours = col;
            var fir = _catalogService.GetAllFirms();
            model.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            model.Sizes = siz;

            var cate = _catalogService.GetProductCategory(id.Value);

            if (Colours.Length > 0 && ModelState.IsValid)
            {
                _catalogService.FilterColour(Colours, model);
            }
            else
            {
                _catalogService.GetAllProductsCategory(id.Value, model);
            } 
            if (model.ProductFilter.Count == 0)
            {
               _catalogService.GetAllProductsCategory(id.Value, model);

            }

            if (Sizes.Length > 0)
            {
                _catalogService.FilterSize(model, Sizes);
            }

            if (Firms.Length > 0)
            {
                _catalogService.FilterFirm(model, Firms);
            }

            return View(model);
        }

        //
        // GET: /Playground/Filter/
        public IActionResult Filter(Product model, string[] Colours, string returnUrl = null)
        {
            //   var cate = _catalogService.GetProductCategory(id.Value);

            var col = _catalogService.getAllColours();
            model.Colours = col;
           // int i = 1, j = 1;
            List<Product> Products = new List<Product>();
            if (Colours.Length > 0 && !ModelState.IsValid)
            {
              
                    foreach (var colour in Colours)
                    {

                        var res = _catalogService.GetProductByRGB(colour, 4);
                        foreach (var item in Products)
                        {
                            var result = _catalogService.GetProduct(item.id_pr);
                            var image = _catalogService.GetImage(result.id_pr);
                            var firm = _catalogService.GetFirm(result.id_fir);
                            var viewModel = new Product
                            {
                                id_pr = item.id_pr,
                                name = result.name,
                                price = result.price,
                                firm = firm.name,
                                image = image.link,
                                id_fir = result.id_fir
                            };
                            Products.Add(viewModel);
                            model.ProductFilter = Products;
                         /*   if (Colours.Length == i && res.Count() < ++j)
                            {
                                model.Colours = col;
                                return View(model);
                            }*/
                        }
                      //  i++;
                    }
                
            } else
            {
               
                var allProducts = _catalogService.GetAllProducts();
                var velAllProducts = allProducts.Count();
                foreach (var product in allProducts)
                {
                    // var result = _catalogService.GetProduct(product.id_pr);
                    var image = _catalogService.GetImage(product.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                    var firm = _catalogService.GetFirm(product.id_fir);

                    var models = new Product
                    {
                        name = product.name,
                        price = product.price,
                        firm = firm.name,
                        image = image.link
                    };
                    Products.Add(models);
                    model.ProductFilter = Products;
                 //   model.Colours = col;


                   /* if (velAllProducts < ++i)
                    {
                        return View(model);
                    }*/
                }
            }
            
            return View(model);
        }
      
    }
}
