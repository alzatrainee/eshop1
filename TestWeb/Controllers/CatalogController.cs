using System.Collections.Generic;
using System.Linq;
using Catalog.Business;
using Microsoft.AspNetCore.Mvc;

namespace PernicekWeb.Controllers
{
    public class CatalogController : Controller
    {
        private readonly CatalogService _catalogService;


        public CatalogController(CatalogService catalogservice)
        {
            _catalogService = catalogservice;
        }

        [HttpGet]
        public IActionResult Browse(FilterProduct model, int? page = 1, int? itemsPerPage = 9)
        {
            /* vypisuje nam checklist barev, firem a velikosti */
            var col = _catalogService.getAllColours();
            model.Colours = col;
            var fir = _catalogService.GetAllFirms();
            model.Firms = fir;
            
            var siz = _catalogService.GetAllSizes();
            model.Sizes = siz;
            model.SortHigh = 1;
            model.SortLow = 1;
            model.ItemsPerPage = itemsPerPage.Value;
            model.CurrentPage = page.Value;
            
            _catalogService.GetAllProductsBrowse(model, page.Value); // zjisit vsechny produkty
               return View(model);
        }

        [HttpPost]
        public IActionResult Browse(FilterProduct model, int page, int? SortFromHigh, int? SortFromLow, int? itemsPage, int? PriceMin, int? PriceMax)
        {
            List<FilterProduct> tmpModel = new List<FilterProduct>();
            _catalogService.GetAllProductsBrowse(model);
            int isCheckColour = 0;
            int isCheckFirm = 0;
            int isCheckSize = 0;
            if (itemsPage != null)
            {
                model.ItemsPerPage = itemsPage.Value;
            }
            //else
            //{
            //    model.ItemsPerPage = 9;
            //}
            if (page == 0)
            {
                page = 1;
            }
            model.CurrentPage = page;

            for (int i = 0; i < model.Firms.Count(); i++)
            {
                if (model.Firms[i].checkboxAnswer == true)
                {
                    _catalogService.FilterOneFirm(model, model.Firms[i].id_fir, tmpModel);
                    isCheckFirm = 1;
                }
            }
            if (isCheckFirm == 1)
            {
                model.ProductFilter = tmpModel.ToList();
                tmpModel.Clear();
            }

            for (int i = 0; i < model.Colours.Count(); i++)
            {
                if (model.Colours[i].checkboxAnswer == true)
                {
                    _catalogService.FilterOneColour(model, model.Colours[i].rgb, tmpModel);
                    isCheckColour = 1;
                }
            }
            if (isCheckColour == 1)
            {
                model.ProductFilter = tmpModel.GroupBy(i => i.id_pr)
                   .Select(g => g.First()).ToList();
                tmpModel.Clear();
            }
            
            for (int i = 0; i < model.Sizes.Count(); i++)
            {
                if (model.Sizes[i].checkboxAnswer == true)
                {
                    _catalogService.FilterOneSize(model, model.Sizes[i].id_si, tmpModel);
                    isCheckSize = 1;
                }
            }
            if (isCheckSize == 1)
            {
                model.ProductFilter = tmpModel.GroupBy(i => i.id_pr)
                   .Select(g => g.First()).ToList();
                tmpModel.Clear();
            }

            if (PriceMin != null && PriceMax != null)
            {
                _catalogService.SortByPrice(model, PriceMin.Value, PriceMax.Value);
            }            

            if (SortFromHigh > 1 && (SortFromLow == 3 || SortFromLow == 1))
            {
                _catalogService.SortFromHighest(model);
                model.SortHigh += 2;
                model.SortLow = 1;
            }

            if (SortFromLow > 1 && (SortFromHigh == 3 || SortFromHigh == 1))
            {
                _catalogService.SortFromLowest(model);
                model.SortLow += 2;
                model.SortHigh = 1;
            }

            _catalogService.GetFewBrowse(model, page);
            
            return View("Browse", model);
        }

        [HttpGet]
        public IActionResult Category(int? id, FilterProduct model, int? page = 1)
        {
            /* vypisuje nam checklist barev, firem a velikosti */
            var col = _catalogService.getAllColours();
            model.Colours = col;
            var fir = _catalogService.GetAllFirms();
            model.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            model.Sizes = siz;
            model.IdCategory = id.Value;
            model.SortHigh = 1;
            model.SortLow = 1;
            _catalogService.GetProductsCategory(id.Value, model);
            model.CurrentPage = page.Value;
            // _catalogService.GetAllProductsBrowse(model, page.Value); // zjisit vsechny produkty
            return View(model);
        }

        [HttpPost]
        public IActionResult Category(FilterProduct model, int[] Ident, int page, int? SortFromHigh, int? SortFromLow, int? itemsPage = 9)
        {
            List<FilterProduct> tmpModel = new List<FilterProduct>();
            
            int isCheckColour = 0;
            int isCheckFirm = 0;
            int isCheckSize = 0;
            model.ItemsPerPage = 9;

            if (SortFromHigh == null)
            {
                SortFromHigh = 1;
            }
            else if (SortFromLow == null)
            {
                SortFromLow = 1;
            }
            if (Ident.Length > 0)
            {
                _catalogService.GetProductBrowse(model, Ident);
            }
            else
            {
                _catalogService.GetAllProductsCategory(model.IdCategory, model);
            }

            if (page == 0)
            {
                page = 1;
            }
            model.CurrentPage = page;

            for (int i = 0; i < model.Firms.Count(); i++)
            {
                if (model.Firms[i].checkboxAnswer == true)
                {
                    _catalogService.FilterOneFirm(model, model.Firms[i].id_fir, tmpModel);
                    isCheckFirm = 1;
                }
            }
            if (isCheckFirm == 1)
            {
                model.ProductFilter = tmpModel.ToList();
                tmpModel.Clear();
            }

            for (int i = 0; i < model.Colours.Count(); i++)
            {
                if (model.Colours[i].checkboxAnswer == true)
                {
                    _catalogService.FilterOneColour(model, model.Colours[i].rgb, tmpModel);
                    isCheckColour = 1;
                }
            }
            if (isCheckColour == 1)
            {
                model.ProductFilter = tmpModel.GroupBy(i => i.id_pr)
                   .Select(g => g.First()).ToList();
                tmpModel.Clear();
            }



            for (int i = 0; i < model.Sizes.Count(); i++)
            {
                if (model.Sizes[i].checkboxAnswer == true)
                {
                    _catalogService.FilterOneSize(model, model.Sizes[i].id_si, tmpModel);
                    isCheckSize = 1;
                }
            }
            if (isCheckSize == 1)
            {
                model.ProductFilter = tmpModel.GroupBy(i => i.id_pr)
                   .Select(g => g.First()).ToList();
                tmpModel.Clear();
            }
            if (SortFromHigh > 1 && (SortFromLow == 3 || SortFromLow == 1))
            {
                _catalogService.SortFromHighest(model);
                model.SortHigh += 2;
                model.SortLow = 1;
            }

            if (SortFromLow > 1 && (SortFromHigh == 3 || SortFromHigh == 1))
            {
                _catalogService.SortFromLowest(model);
                model.SortLow += 2;
                model.SortHigh = 1;
            }
            if (Ident.Length == 0)
            {
                _catalogService.GetFewBrowse(model, page);
            }
            else
            {
                ViewData["FirmSearch"] = true;
            }
            
            
            return View(model);
        }

        public IActionResult SortLowest(FilterProduct model)
        {
            var col = _catalogService.getAllColours();
            model.Colours = col;
            var fir = _catalogService.GetAllFirms();
            model.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            model.Sizes = siz;


          //  _catalogService.GetAllProductsBrowse(model);
            _catalogService.SortFromLowest(model);

            return View("Browse", model);
        }

        public IActionResult SortHighest(FilterProduct model)
        {
            var col = _catalogService.getAllColours();
            model.Colours = col;
            var fir = _catalogService.GetAllFirms();
            model.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            model.Sizes = siz;


           // _catalogService.GetAllProductsBrowse(model);
            _catalogService.SortFromHighest(model);

            return View("Browse", model);
        }

        public IActionResult Empty()
        {
           return View();
        }




        public IActionResult CategorySearch(List<int?> idOfCategories, FilterProduct viewModel)
        {
            var col = _catalogService.getAllColours();
            viewModel.Colours = col;
            var fir = _catalogService.GetAllFirms();
            viewModel.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            viewModel.Sizes = siz;

            //List<FilterProduct> Products = new List<FilterProduct>();
            var numberOfCategories = idOfCategories.Count();
            List<List<Catalog.Dal.Entities.Cat_sub>> cat_sub = new List<List<Catalog.Dal.Entities.Cat_sub>>();

            for (var i = 0; i < numberOfCategories; ++i)
            {
                cat_sub.Add(_catalogService.GetProductCategory(idOfCategories[i].Value));
            }

            int catSubCount = cat_sub.Count();


            for (var i = 0; i < catSubCount; ++i)
            {
                foreach (var cat in cat_sub[i])
                {
                    var list = _catalogService.Get_ProductId(cat.id_cs);

                    foreach (var product in list)
                    {

                        var result = _catalogService.GetProduct(product.id_pr);
                        var image = _catalogService.GetImage(product.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                        var firm = _catalogService.GetFirm(result.id_fir);

                        var model = new FilterProduct
                        {
                            name = result.name,
                            price = result.price,
                            firm = firm.name,
                            image = image.link,
                            id_pr = product.id_pr
                        };
                        viewModel.ProductFilter.Add(model);
                    }
                   
                }
            }
            return View("Category", viewModel);
        }

        public IActionResult ProductsSearch( List<int> ListOfId, FilterProduct model )
        {
            List<Catalog.Dal.Entities.Product> products = new List<Catalog.Dal.Entities.Product>();
            FilterProduct AllProductsInOne = new FilterProduct();

            var col = _catalogService.getAllColours();
            AllProductsInOne.Colours = col;
            var fir = _catalogService.GetAllFirms();
            AllProductsInOne.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            AllProductsInOne.Sizes = siz;

            foreach (var i in ListOfId)
            {
                products.Add(_catalogService.GetProduct(i));
            }
            foreach (var product in products)
            {
                var firma = _catalogService.GetFirm(product.id_fir);
                var image = _catalogService.GetImage(product.id_pr);
                var result = _catalogService.GetProduct(product.id_pr);

                var viewModel = new FilterProduct
                {
                    name = result.name,
                    price = result.price,
                    firm = firma.name,
                    image = image.link,
                    id_pr = product.id_pr
                };
                AllProductsInOne.ProductFilter.Add(viewModel);
            }
            return View("Category", AllProductsInOne);
        }

        public IActionResult FirmSearch( string SearchString)
        {
            
            FilterProduct AllProductsInOne = new FilterProduct();

            var col = _catalogService.getAllColours();
            AllProductsInOne.Colours = col;
            var fir = _catalogService.GetAllFirms();
            AllProductsInOne.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            AllProductsInOne.Sizes = siz;

            List<Catalog.Dal.Entities.Firm> firms = _catalogService.GetFirmsByName(SearchString);
            int FirmsAmount = firms.Count();
            List<FilterProduct> NotPermanentList = new List<FilterProduct>(); //tahle cinnost se stava uz uplne neochopitelnou... ani pro me
            for(int i = 0; i < FirmsAmount; ++i)
            {
                NotPermanentList = _catalogService.GetProductByFirmId(firms[i].id_fir); // List FilterProductu
                foreach(var product in NotPermanentList)
                {
                    AllProductsInOne.ProductFilter.Add(product);
                }
            }
            ViewData["FirmSearch"] = true;
            return View("Category", AllProductsInOne);
        }
    }
}
    
