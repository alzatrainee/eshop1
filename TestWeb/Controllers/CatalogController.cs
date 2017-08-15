using System.Collections.Generic;
using System.Linq;
using Catalog.Dal.Repository.Abstraction;
using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using Catalog.Dal.Context;

namespace PernicekWeb.Controllers
{
    public class CatalogController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly IProductRepository _iProductRepository;
        private readonly Iprod_colRepository _iprod_colRepository;
        private readonly IProd_siRepository _iProd_siRepository;
        private readonly IImageRepository _iImageRepository;
        private readonly IFirmRepository _iFirmRepository;
        private readonly IProduct_catRepository _iProduct_catRepository;

        //private readonly ICategoryRepository _iCategoryRepository;

        public CatalogController(CatalogService catalogservice, IProductRepository iProductRepository, Iprod_colRepository iprod_colRepository, IProd_siRepository iProd_siRepository,
                                    IImageRepository iImageRepository, IFirmRepository iFirmRepository, IProduct_catRepository iProduct_catRepository)
        {
            _iProductRepository = iProductRepository;
            _catalogService = catalogservice;
            _iprod_colRepository = iprod_colRepository;
            _iProd_siRepository = iProd_siRepository;
            _iImageRepository = iImageRepository;
            _iFirmRepository = iFirmRepository;
            _iProduct_catRepository = iProduct_catRepository;
        }

        public IActionResult Browse()
        {
            List<PernicekWeb.Models.CatalogViewModel.Product> Products = new List<Models.CatalogViewModel.Product>();
            var allProducts = _catalogService.GetAllProducts();
            var velAllProducts = allProducts.Count();
            for (var j = 0; j < velAllProducts; j++)
            {

                var result = _catalogService.GetProduct(j);
                var image = _catalogService.GetImage(result.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                var firm = _catalogService.GetFirm(result.id_fir);

                var model = new Models.CatalogViewModel.Product
                {
                    name = result.name,
                    price = result.price,
                    firm = firm.name,
                    image = image.link
                };
                Products.Add(model);
            }
            return View(Products);
        }

        public IActionResult Category(int? id)
        {
            List<Models.CatalogViewModel.Product> Products = new List<Models.CatalogViewModel.Product>();
            var cate = _catalogService.GetProductCategory(id.Value);
            foreach (var category in cate)
            {

                var res = _catalogService.Get_ProductId(category.id_cs);


                // var res = _catalogService.Get_ProductId(cate.);
                foreach (var product in res)
                {


                    var result = _catalogService.GetProduct(product.id_pr);
                    var image = _catalogService.GetImage(product.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                    var firm = _catalogService.GetFirm(result.id_fir);

                    var model = new Models.CatalogViewModel.Product
                    {
                        name = result.name,
                        price = result.price,
                        firm = firm.name,
                        image = image.link,
                        id_pr = product.id_pr
                    };
                    Products.Add(model);
                }
            }
            return View(Products);
        }




        public IActionResult Brsdfsowse() { 
            return View();
        }

        public IActionResult CategorySearch( List<int?> idOfCategories )
        {
            List<Models.CatalogViewModel.Product> Products = new List<Models.CatalogViewModel.Product>();
            var numberOfCategories = idOfCategories.Count();
            List<List<Catalog.Dal.Entities.Cat_sub>> cat_sub = new List<List<Catalog.Dal.Entities.Cat_sub>>();
            
            for( var i = 0; i < numberOfCategories; ++i )
            {                
                cat_sub.Add(_catalogService.GetProductCategory(idOfCategories[i].Value));
            }

            int catSubCount = cat_sub.Count();
            

            for( var i = 0; i < catSubCount; ++i)
            {
                foreach(var cat in cat_sub[i])
                {
                    var list = _catalogService.Get_ProductId(cat.id_cs);

                    foreach (var product in list)
                    {
                        
                        var result = _catalogService.GetProduct(product.id_pr);
                        var image = _catalogService.GetImage(product.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                        var firm = _catalogService.GetFirm(result.id_fir);

                        var model = new Models.CatalogViewModel.Product
                        {
                            name = result.name,
                            price = result.price,
                            firm = firm.name,
                            image = image.link,
                            id_pr = product.id_pr
                        };
                        Products.Add(model);
                    }

                }
            }
            return View("Category", Products);
        }
    }
}