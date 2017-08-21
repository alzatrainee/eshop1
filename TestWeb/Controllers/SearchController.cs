using System.Collections.Generic;
using System.Linq;
using Catalog.Dal.Repository.Abstraction;
using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using Catalog.Dal.Context;


namespace PernicekWeb.Controllers
{
    public class SearchController : Controller
    {
        private readonly CatalogService _catalogService; //save to your future searching by firms & products
        private readonly IProductRepository _iProductRepository;
        private readonly Iprod_colRepository _iprod_colRepository;
        private readonly IProd_siRepository _iProd_siRepository;
        private readonly IImageRepository _iImageRepository;
        private readonly IFirmRepository _iFirmRepository;
        private readonly IProduct_catRepository _iProduct_catRepository;
        private readonly CatalogDbContext _context;

        public SearchController(CatalogService catalogservice, IProductRepository iProductRepository, Iprod_colRepository iprod_colRepository, IProd_siRepository iProd_siRepository,
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


        public IActionResult SearchCategory(string SearchString)
        {

            if( string.IsNullOrEmpty(SearchString) )
            {
                return RedirectToAction("Error: you wrote nothing to the area."); // pridat Error stranku
            }

            if( SearchString.Length < 4)
            {
                return RedirectToAction("Your query has less, that 4 symbols."); // Pridat specialni hlasku 
            }

            SearchString = SearchString.ToLower();

            var category = _catalogService.GetCategoryByName(SearchString);

            if(category.Count == 0)
            {
                return RedirectToAction("Error: you are."); // stranka, ktera nahlasi, ze pozadavkum neodpovida zadna kategorie 
            }
            List<int> idOfCategories = new List<int>();
            var numberOfCategories = category.Count();
            for( int i = 0; i < numberOfCategories; ++i )
            {
                idOfCategories.Add(category[i].id_cat);
            }

            return RedirectToAction(nameof(CatalogController.CategorySearch), "Catalog", new {  idOfCategories = idOfCategories });
        }

        public IActionResult SearchProducts(string SearchString)
        {

            if (string.IsNullOrEmpty(SearchString))
            {
                return RedirectToAction("Error: you wrote nothing to the area."); // pridat Error stranku
            }

            if (SearchString.Length < 4)
            {
                return RedirectToAction("Your query has less, that 4 symbols."); // Pridat specialni hlasku 
            }

            SearchString = SearchString.ToLower();

            var products = _catalogService.GetProductsByName(SearchString);

            if (products.Count == 0)
            {
                return RedirectToAction("Error: you are."); // stranka, ktera nahlasi, ze pozadavkum neodpovida zadna kategorie 
            }
            List<int> ListOfId = new List<int>();
            foreach(var product in products)
            {
                ListOfId.Add(product.id_pr);
            }
            
            return RedirectToAction(nameof(CatalogController.ProductsSearch), "Catalog", new { ListOfId = ListOfId });
        }

       

    }
}
