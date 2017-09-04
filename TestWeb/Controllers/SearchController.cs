using System.Collections.Generic;
using System.Linq;
using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using Catalog.Dal.Context;


namespace PernicekWeb.Controllers
{
    public class SearchController : Controller
    {
        private readonly CatalogService _catalogService; //save to your future searching by firms & products

        private readonly CatalogDbContext _context;

        public SearchController(CatalogService catalogservice, CatalogDbContext context)
        {
            _catalogService = catalogservice;
            _context = context;
        }


        public IActionResult SearchCategory(string SearchString)
        {

            if( string.IsNullOrEmpty(SearchString) )
            {
                  return RedirectToAction(nameof(ErrorController.EmptyString), "Error");
            }

            if( SearchString.Length < 4)
            {
                return RedirectToAction(nameof(ErrorController.TooShort), "Error");
            }

            SearchString = SearchString.ToLower();

            var category = _catalogService.GetCategoryByName(SearchString);

            if(category.Count == 0)
            {
                return RedirectToAction(nameof(ErrorController.NothingFound), "Error");
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
                return RedirectToAction(nameof(ErrorController.EmptyString), "Error");
            }

            if (SearchString.Length < 4)
            {
                return RedirectToAction(nameof(ErrorController.TooShort), "Error");
            }

            SearchString = SearchString.ToLower();

            var products = _catalogService.GetProductsByName(SearchString);

            if (products.Count == 0)
            {
                return RedirectToAction(nameof(ErrorController.NothingFound), "Error");
            }

            List<int> ListOfId = new List<int>();
            foreach(var product in products)
            {
                ListOfId.Add(product.id_pr);
            }
            
            return RedirectToAction(nameof(CatalogController.ProductsSearch), "Catalog", new { ListOfId = ListOfId });
        }


        public IActionResult SearchFirm( string SearchString )
        {
            if (string.IsNullOrEmpty(SearchString))
            {
                return RedirectToAction(nameof(ErrorController.EmptyString), "Error");
            }

            if (SearchString.Length < 4)
            {
                return RedirectToAction(nameof(ErrorController.TooShort), "Error");
            }


            SearchString = SearchString.ToLower();

            var firms = _catalogService.GetFirmsByName(SearchString);
            if (firms.Count == 0) {
                return RedirectToAction(nameof(ErrorController.NothingFound), "Error");
            }

            return RedirectToAction(nameof(CatalogController.FirmSearch), "Catalog", new { SearchString = SearchString });
        }


    }
}
