using System.Collections.Generic;
using System.Linq;
using Catalog.Dal.Repository.Abstraction;
using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using Catalog.Dal.Context;
using PernicekWeb.Models.CatalogViewModel;

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
       
        [HttpGet]
        public IActionResult Browse(FilterProduct model)
        {
            /* vypisuje nam checklist barev, firem a velikosti */
            var col = _catalogService.getAllColours();
            model.Colours = col;
            var fir = _catalogService.GetAllFirms();
            model.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            model.Sizes = siz;

            _catalogService.GetAllProductsBrowse(model); // zjisit vsechny produkty
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Browser(int[] Ident, string[] Colours, FilterProduct model,  int[] Firms, int[] Sizes)
        {   
            /* vypisuje nam checklist barev, firem a velikosti */
            var col = _catalogService.getAllColours();
            model.Colours = col;
            var fir = _catalogService.GetAllFirms();
            model.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            model.Sizes = siz;

            /* ulozi do model.ProductFilter vsechny produkty ktere se vypsaly na strance, ke zjisteni pouzivame hodnoty v Ident[] */
            _catalogService.GetProductBrowse(model, Ident);

            if (Colours.Length > 0)
                {
                    _catalogService.FilterColour(Colours, model);
                }

                if (Sizes.Length > 0)
                {
                    _catalogService.FilterSize(model, Sizes);

                }

                if (Firms.Length > 0)
                {
                    _catalogService.FilterFirm(model, Firms);
                }
            
            return View("Browse", model); // pouzivame View v Browse a predavame mu nas vyfiltrovany model
        }

        [HttpGet]
        public IActionResult Category(int? id, FilterProduct model)
        {
            /* vypisuje nam checklist barev, firem a velikosti */
            var col = _catalogService.getAllColours();
            model.Colours = col;
            var fir = _catalogService.GetAllFirms();
            model.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            model.Sizes = siz;

            _catalogService.GetAllProductsCategory(id.Value, model); // zjisti vsechny produkty v kategorii dane id
            return View(model);
        }

        [HttpGet] 
        public IActionResult Categories(int? id, int[] Ident, FilterProduct model, string[] Colours, int[] Firms, int[] Sizes)
        {
            /* vypisuje nam checklist barev, firem a velikosti */
            var col = _catalogService.getAllColours();
            model.Colours = col;
            var fir = _catalogService.GetAllFirms();
            model.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            model.Sizes = siz;

            /* ulozi do model.ProductFilter vsechny produkty ktere se vypsaly na strance, ke zjisteni pouzivame hodnoty v Ident[] */
            _catalogService.GetProductBrowse(model, Ident);

            if (Colours.Length > 0)
            {
                _catalogService.FilterColour(Colours, model);
            }

            if (Sizes.Length > 0)
            {
                _catalogService.FilterSize(model, Sizes);
            }

            if (Firms.Length > 0)
            {
                _catalogService.FilterFirm(model, Firms);
            }

            return View("Category", model); // pouzivame View v Category a predavame mu nas vyfiltrovany model
        }

        public IActionResult Empty()
        {
           return View();
        }




        public IActionResult CategorySearch(List<int?> idOfCategories, FilterProduct viewModel)
        {
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

        public IActionResult ProductsSearch( List<int> ListOfId )
        {
            List<Catalog.Dal.Entities.Product> products = new List<Catalog.Dal.Entities.Product>();
            FilterProduct AllProductsInOne = new FilterProduct();
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

        public IActionResult FirmSearch( string SearchString )
        {
            FilterProduct AllProductsInOne = new FilterProduct();
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
            return View("Category", AllProductsInOne);
        }
    }
}
    
