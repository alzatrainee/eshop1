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

        /* Pouzivam Get a Post a pomoci toho zobrazuji katalog plus filtruji */
        [HttpGet]
        public IActionResult Browse(FilterProduct model, int? page = 1, int? itemsPerPage = 9) // v tomto pripade je vzdycky stranka c.1 a polozek na stranku je defaultne 9
        {
            /* vypisuje nam checklist barev, firem a velikosti */
            var col = _catalogService.getAllColours();
            model.Colours = col;
            var fir = _catalogService.GetAllFirms();
            model.Firms = fir;
            
            var siz = _catalogService.GetAllSizes();
            model.Sizes = siz;

            /* Pomoci toho pozdeji filtruji podle ceny */
            model.SortHigh = 1;
            model.SortLow = 1;
            model.NumbersLike = 1;
           
            model.ItemsPerPage = itemsPerPage.Value; // Ukladam si polozek na stranku do modelu
            model.CurrentPage = page.Value; // pro zobrazeni soucasne stranky ve View
            
            _catalogService.GetAllProductsBrowse(model, page.Value); // zjisit vsechny produkty a vrati jich pouze 9
               return View(model);
        }

        [HttpPost]
        public IActionResult Browse(FilterProduct model, int page, int? SortFromHigh, int? SortFromLow, int? PriceMin, int? PriceMax, int? itemsPage, int? LikeNumbers)
        {
            List<FilterProduct> tmpModel = new List<FilterProduct>(); // pomocny model k filtraci
            int isCheckColour = 0;
            int isCheckFirm = 0;
            int isCheckSize = 0;

            _catalogService.GetAllProductsBrowse(model); // ziska do model.ProductFilter vsechny produkty
            model.minPrice = PriceMin.Value;
            model.maxPrice = PriceMax.Value;

            if (itemsPage != null)
            {
                model.ItemsPerPage = itemsPage.Value; // prirazuji pocet polozek na stranku do modelu, protoze dale s tim pracuji pro zobrazeni daneho poctu na stranku
            }
            
            if (page == 0)
            {
                page = 1;
            }

            model.CurrentPage = page; // pro graficke zobrazeni soucasne stranky ve view

            /* Prochazim checkbox firem a hledam ktera firma je zaskrtla a ktera neni */
            for (int i = 0; i < model.Firms.Count(); i++)
            {
                if (model.Firms[i].checkboxAnswer == true)
                {
                    _catalogService.FilterOneFirm(model, model.Firms[i].id_fir, tmpModel);
                    isCheckFirm = 1; // nastavuji na hodnotu 1 abych vedel ze se nasel alespon jeden
                }
            }
            /* Pokud se nalezl alespon jeden prirazuji pomocny tmpModel do model.ProductFilter a mazu vse z pomocneho modelu */
            if (isCheckFirm == 1)
            {
                model.ProductFilter = tmpModel.ToList();
                tmpModel.Clear();
            }

            /* Prochazim checkbox barev a hledam ktera barva je zaskrtla a ktera neni */
            for (int i = 0; i < model.Colours.Count(); i++)
            {
                if (model.Colours[i].checkboxAnswer == true)
                {
                    _catalogService.FilterOneColour(model, model.Colours[i].rgb, tmpModel);
                    isCheckColour = 1; // nastavuji na hodnotu 1 abych vedel ze se nasel alespon jeden
                }
            }

            /* Pokud se nalezla alespon jedna zaskrtla barva */
            if (isCheckColour == 1)
            {
                /* do model.ProductFilter prirazuji pomocny tmpModel, ze ktereho zaroven odstranuji duplicity */
                model.ProductFilter = tmpModel.GroupBy(i => i.id_pr)
                   .Select(g => g.First()).ToList();
                tmpModel.Clear();
            }

            /* Prochazim checkbox velikosti a hledam ktera velikost je zaskrtla a ktera neni */
            for (int i = 0; i < model.Sizes.Count(); i++)
            {
                if (model.Sizes[i].checkboxAnswer == true)
                {
                    _catalogService.FilterOneSize(model, model.Sizes[i].id_si, tmpModel);
                    isCheckSize = 1; // nastavuji na hodnotu 1 abych vedel ze se nasel alespon jeden
                }
            }

            /* Pokud se nalezla alespon jedna zaskrtla velikost */
            if (isCheckSize == 1)
            {
                /* do model.ProductFilter prirazuji pomocny tmpModel, ze ktereho zaroven odstranuji duplicity */
                model.ProductFilter = tmpModel.GroupBy(i => i.id_pr)
                   .Select(g => g.First()).ToList();
                tmpModel.Clear();
            }

            model.checkFilter = false;
            if (LikeNumbers == 1 || LikeNumbers == 3)
            {
                model.checkFilter = true;
            }

            if (LikeNumbers > 1 && (SortFromLow == 3 || SortFromLow == 1) && (SortFromLow == 3 || SortFromLow == 1))
            {
                _catalogService.SortFavourite(model);
                model.NumbersLike += 2;
                model.SortHigh = 1;
                model.SortLow = 1;
            }

           
                _catalogService.SortByPrice(model, tmpModel, PriceMax.Value, PriceMin.Value);

            /* Z view ziskavam hodnoty SortFromHigh a SortFromLow, pokud mel uzivatel puvodne razeni od nejlevnejsiho a potom kliknul na razeni od nejdrazsiho  *
             * znamena to, ze SortFromHigh se zmeni na 2 a SortFromLow zustava na 3                                                                             */
            if (SortFromHigh > 1 && (SortFromLow == 3 || SortFromLow == 1) && model.checkFilter)
            {
                _catalogService.SortFromHighest(model);
                model.SortHigh += 2; //zvysuji o dve a vzdy se mi potom z View vrati hodnota 3
                model.SortLow = 1; //nastavuji na 1 a muze se zmenit pouze v pripade ze uzivatel bude chtit radit od nejlevnejsiho
                model.NumbersLike = 1;
            }

            /* Opacny pripad viz. vyse */
            if (SortFromLow > 1 && (SortFromHigh == 3 || SortFromHigh == 1) && model.checkFilter)
            {
                _catalogService.SortFromLowest(model);
                model.SortLow += 2; //zvysuji o dve a vzdy se mi potom z View vrati hodnota 3
                model.SortHigh = 1;//nastavuji na 1 a muze se zmenit pouze v pripade ze uzivatel bude chtit radit od nejdrazsiho
                model.NumbersLike = 1;
            }

            

            _catalogService.GetFewBrowse(model, page); //do model.ProductFilter si ulozim jen produkty ktery vyhovujou dane strance
            
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
            model.NumbersLike = 1;

            _catalogService.GetProductsCategory(id.Value, model);
            model.CurrentPage = page.Value;
            return View(model);
        }

        [HttpPost]
        public IActionResult Category(FilterProduct model, int[] Ident, int page, int? SortFromHigh, int? SortFromLow, int? PriceMin, int? PriceMax, int? LikeNumbers, int? itemsPage = 9)
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

            model.checkFilter = false;
            if (LikeNumbers == 1 || LikeNumbers == 3)
            {
                model.checkFilter = true;
            }

            if (LikeNumbers > 1 && (SortFromLow == 3 || SortFromLow == 1) && (SortFromLow == 3 || SortFromLow == 1))
            {
                _catalogService.SortFavourite(model);
                model.NumbersLike += 2;
                model.SortHigh = 1;
                model.SortLow = 1;
            }


                _catalogService.SortByPrice(model, tmpModel, PriceMax.Value, PriceMin.Value);
            

            /* Z view ziskavam hodnoty SortFromHigh a SortFromLow, pokud mel uzivatel puvodne razeni od nejlevnejsiho a potom kliknul na razeni od nejdrazsiho  *
             * znamena to, ze SortFromHigh se zmeni na 2 a SortFromLow zustava na 3                                                                             */
            if (SortFromHigh > 1 && (SortFromLow == 3 || SortFromLow == 1) && model.checkFilter)
            {
                _catalogService.SortFromHighest(model);
                model.SortHigh += 2; //zvysuji o dve a vzdy se mi potom z View vrati hodnota 3
                model.SortLow = 1; //nastavuji na 1 a muze se zmenit pouze v pripade ze uzivatel bude chtit radit od nejlevnejsiho
                model.NumbersLike = 1;
            }

            /* Opacny pripad viz. vyse */
            if (SortFromLow > 1 && (SortFromHigh == 3 || SortFromHigh == 1) && model.checkFilter)
            {
                _catalogService.SortFromLowest(model);
                model.SortLow += 2; //zvysuji o dve a vzdy se mi potom z View vrati hodnota 3
                model.SortHigh = 1;//nastavuji na 1 a muze se zmenit pouze v pripade ze uzivatel bude chtit radit od nejdrazsiho
                model.NumbersLike = 1;
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
    
