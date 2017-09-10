using System.Collections.Generic;
using System.Linq;
using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using Module.Business.Business;
using Alza.Module.UserProfile.Business;
using Microsoft.AspNetCore.Identity;
using Alza.Core.Identity.Dal.Entities;
using Microsoft.Extensions.Logging;
using Alza.Module.UserProfile.Dal.Context;
using System.Threading.Tasks;
using System.Diagnostics;
using System;
using Catalog.Dal.Entities;

namespace PernicekWeb.Controllers
{
    public class CatalogController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly BusinessService _businessService;
        private readonly UserProfileService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly UserProfileService _userProfileService;
        private readonly UserDbContext _context;

        public CatalogController(CatalogService catalogservice, BusinessService businessService, UserProfileService userservice, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                ILoggerFactory loggerFactory, UserProfileService userProfileservice, UserDbContext context)
        {
            _catalogService = catalogservice;
            _businessService = businessService;
            _userService = userservice;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<CatalogController>();
            _userProfileService = userProfileservice;
            _context = context;
        }


        /****************************************************
         *                  Catalog                         *
         ****************************************************/
        /* Pouzivam Get a Post a pomoci toho zobrazuji katalog plus filtruji */



        [HttpGet]
        public async Task<IActionResult> Browse(FilterProduct model, int? page = 1, int? itemsPerPage = 9) // v tomto pripade je vzdycky stranka c.1 a polozek na stranku je defaultne 9
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

            model.NumbersLike = 1; // pomoci toho filtruji podle oblibenosti

            /* Pro fungovani price range */
            model.minPrice = 1;
            model.maxPrice = 1;

            model.ItemsPerPage = itemsPerPage.Value; // Ukladam si polozek na stranku do modelu
            model.CurrentPage = page.Value; // pro zobrazeni soucasne stranky ve View

            model.FilterFavouriteOn = "btn btn-default";
            model.FilterHighOn = "btn btn-default";
            model.FilterLowOn = "btn btn-default";

            model.FilterPage9On = "btn btn-warning";
            model.FilterPage27On = "btn btn-default";
            model.FilterPage69On = "btn btn-default";

            _catalogService.GetAllProductsBrowse(model, page.Value); // zjisit vsechny produkty a vrati jich pouze 9

            var user = await GetCurrentUserAsync();

            if(user != null)
            {
                foreach(var product in model.ProductFilter)
                {
                    if(_businessService.AlreadyHasThisProductInList(user.Id, product.id_pr))
                    {
                        model.HaveThisProductInWishList.Add(1); // ano, je ve wishListu
                    } else
                    {
                        model.HaveThisProductInWishList.Add(0); // ne, produkt do wishlistu naseho Usera nepatri
                    }
                }

                foreach(var latestOfer in model.LatestOffer)
                {
                    if (_businessService.AlreadyHasThisProductInList(user.Id, latestOfer.id_pr))
                    {
                        model.HaveLatestProductInWishList.Add(1); // ano, je ve wishListu
                    }
                    else
                    {
                        model.HaveLatestProductInWishList.Add(0); // ne, produkt do wishlistu naseho Usera nepatri
                    }

                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Browse (FilterProduct model, int page, int? SortFromHigh, int? SortFromLow, int? PriceMin, int? PriceMax, int? itemsPage, int? LikeNumbers)
        {
            List<Product> tmpModel = new List<Product>(); // pomocny model k filtraci
            int isCheckColour = 0;
            int isCheckFirm = 0;
            int isCheckSize = 0;
            
            _catalogService.GetAllProductsBrowse(model); // ziska do model.ProductFilter vsechny produkty
            
            /* ukladam soucasny price range do modelu, ktery pak predam do View, kde se podle toho nastavi price range */
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

            model.FilterPage9On = "btn btn-default";
            model.FilterPage27On = "btn btn-default";
            model.FilterPage69On = "btn btn-default";

            if (itemsPage == 9) model.FilterPage9On = "btn btn-warning";
            if (itemsPage == 27) model.FilterPage27On = "btn btn-warning";
            if (itemsPage == 69) model.FilterPage69On = "btn btn-warning";


            model.CurrentPage = page; // pro graficke zobrazeni soucasne stranky ve view


            /****************************************************
             *                   CHECKBOX                       *
             ****************************************************/
           
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
                model.ProductList = tmpModel.ToList();
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
                model.ProductList = tmpModel.GroupBy(i => i.id_pr)
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
                model.ProductList = tmpModel.GroupBy(i => i.id_pr)
                   .Select(g => g.First()).ToList();
                tmpModel.Clear();
            }
            

            /****************************************************
             *             RAZENI PODLE OBLIBENOSTI             *
             ****************************************************/
            
            model.FilterFavouriteOn = "btn btn-default";
            model.FilterHighOn = "btn btn-default";
            model.FilterLowOn = "btn btn-default";

            /* Pokud jsem jeste nefiltroval podle oblibenosti, 
             * nebo uz jsem filtroval nastavim checkFilter na true a pozdeji ho pouzivam u razeni podle ceny */
            model.checkFilter = false;
            if (LikeNumbers == 1 || LikeNumbers == 3)
            {
                model.checkFilter = true;
            }

            /* Kontroluji jestli uz jsem kliknul na tlacitko radit podle oblibenosti a pokud ano je v LikeNumbers 2 nebo 3, pokud ne je v LikeNumbers 1
             * zaroven kontroluji, aby razeni podle cen bud nebyla jeste kliknuta: SortFromLow nebo SortFromHigh se rovnaji 1,
             * nebo prave uz byla: SortFromLow nebo SortFromHigh se rovnaji 3 a chci to zmenit */
            if (LikeNumbers > 1 && (SortFromLow == 3 || SortFromLow == 1) && (SortFromHigh == 3 || SortFromHigh == 1))
            {
                _catalogService.SortFavourite(model);
                model.NumbersLike += 2; //zvysuji o dve a pres model se posle do View a kdyz budu chtit dal neco filtrovat vrati se 3 z View do LikeNumbers
                model.SortHigh = 1;
                model.SortLow = 1;
                model.FilterFavouriteOn = "btn btn-warning";
            }

            /****************************************************
             *             RAZENI PODLE CENY                    *
             ****************************************************/

            if (PriceMin.Value != 10 || PriceMax != 1995)
            {
                _catalogService.SortByPrice(model, tmpModel, PriceMax.Value, PriceMin.Value);
            }

            /* Kontroluji jestli uz jsem kliknul na tlacitko radit podle ceny pokud ano je v SortFromHigh 2 nebo 3, pokud ne je v SortFromHigh 1
             * zaroven kontroluji, aby razeni podle nejlevnejsiho bud nebyla jeste kliknuta: SortFromLow se rovna 1,
             * nebo prave uz byla: SortFromLow se rovna 3 a chci to zmenit a stejnym zpusobem uz mam zkontrolovano jestli kliknul nebo ne na razeni podle oblibenosti*/
            if (SortFromHigh > 1 && (SortFromLow == 3 || SortFromLow == 1) && model.checkFilter)
            {
                _catalogService.SortFromHighest(model);
                model.SortHigh += 2; //zvysuji o dve a vzdy se mi potom z View vrati hodnota 3 v SortFromHigh 
                model.SortLow = 1; //nastavuji na 1 a muze se zmenit pouze v pripade ze uzivatel bude chtit radit od nejlevnejsiho
                model.NumbersLike = 1;
                model.FilterHighOn = "btn btn-warning";
            }

            /* Opacny pripad viz. vyse */
            if (SortFromLow > 1 && (SortFromHigh == 3 || SortFromHigh == 1) && model.checkFilter)
            {
                _catalogService.SortFromLowest(model);
                model.SortLow += 2; //zvysuji o dve a vzdy se mi potom z View vrati hodnota 3 v SortFromLow
                model.SortHigh = 1;//nastavuji na 1 a muze se zmenit pouze v pripade ze uzivatel bude chtit radit od nejdrazsiho
                model.NumbersLike = 1;
                model.FilterLowOn = "btn btn-warning";
            }

           
            _catalogService.GetFewBrowse(model, page); //do model.ProductFilter si ulozim jen produkty ktery vyhovujou dane strance a filtrum
            

            var user = await GetCurrentUserAsync();

            if (user != null)
            {
                foreach (var product in model.ProductFilter)
                {
                    if (_businessService.AlreadyHasThisProductInList(user.Id, product.id_pr))
                    {
                        model.HaveThisProductInWishList.Add(1); // ano, je ve wishListu
                    }
                    else
                    {
                        model.HaveThisProductInWishList.Add(0); // ne, produkt do wishlistu naseho Usera nepatri
                    }
                }

                foreach (var latestOfer in model.LatestOffer)
                {
                    if (_businessService.AlreadyHasThisProductInList(user.Id, latestOfer.id_pr))
                    {
                        model.HaveLatestProductInWishList.Add(1); // ano, je ve wishListu
                    }
                    else
                    {
                        model.HaveLatestProductInWishList.Add(0); // ne, produkt do wishlistu naseho Usera nepatri
                    }

                }
            }

            

            return View("Browse", model);
        }

        /****************************************************
         *                  Kategorie                       *
         ****************************************************/
        [HttpGet]
        public async Task<IActionResult> Category(int? id, FilterProduct model, int? page = 1)
        {
            /* vypisuje nam checklist barev, firem a velikosti */
            var col = _catalogService.getAllColours();
            model.Colours = col;
            var fir = _catalogService.GetAllFirms();
            model.Firms = fir;
            var siz = _catalogService.GetAllSizes();
            model.Sizes = siz;

            if (id == null)
            {
                id = 1;
            }
            model.IdCategory = id.Value; // predavam do View a pozdeji to z nej i dostavam zpet

            /* Pomoci toho pozdeji filtruji podle ceny */
            model.SortHigh = 1;
            model.SortLow = 1;

            model.NumbersLike = 1; // pomoci toho filtruji podle oblibenosti

            /* Pro fungovani price range */
            model.minPrice = 1;
            model.maxPrice = 1;

            model.FilterFavouriteOn = "btn btn-default";
            model.FilterHighOn = "btn btn-default";
            model.FilterLowOn = "btn btn-default";

            model.CurrentPage = page.Value; // pro zobrazeni soucasne stranky ve View

            _catalogService.GetProductsCategory(id.Value, model); //zjisti vsechny produkty a 9 jich ulozi do modelu

            var user = await GetCurrentUserAsync();

            if (user != null)
            {
                foreach (var product in model.ProductFilter)
                {
                    if (_businessService.AlreadyHasThisProductInList(user.Id, product.id_pr))
                    {
                        model.HaveThisProductInWishList.Add(1); // ano, je ve wishListu
                    }
                    else
                    {
                        model.HaveThisProductInWishList.Add(0); // ne, produkt do wishlistu naseho Usera nepatri
                    }
                }

                foreach (var latestOfer in model.LatestOffer)
                {
                    if (_businessService.AlreadyHasThisProductInList(user.Id, latestOfer.id_pr))
                    {
                        model.HaveLatestProductInWishList.Add(1); // ano, je ve wishListu
                    }
                    else
                    {
                        model.HaveLatestProductInWishList.Add(0); // ne, produkt do wishlistu naseho Usera nepatri
                    }

                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Category(FilterProduct model, int[] Ident, int page, int? SortFromHigh, int? SortFromLow, int? PriceMin, int? PriceMax, int? LikeNumbers, int? itemsPage = 9)
        {
            List<Product> tmpModel = new List<Product>(); // pomocny model k filtraci

            int isCheckColour = 0;
            int isCheckFirm = 0;
            int isCheckSize = 0;

            model.ItemsPerPage = 9; // defaultne je nastaven pocet produktu na stranku u kategorii 9

            /* ukladam soucasny price range do modelu, ktery pak predam do View, kde se podle toho nastavi price range */
            model.minPrice = PriceMin.Value;
            model.maxPrice = PriceMax.Value;

            if (SortFromHigh == null)
            {
                SortFromHigh = 1;
            }
            else if (SortFromLow == null)
            {
                SortFromLow = 1;
            }

            if (LikeNumbers == null)
            {
                LikeNumbers = 1;
            }

            /* Pouzivame tuto metodu i pro filtrovani u Search 
             * V Ident jsou vsechny produkty, ktery se zobrazuji na stranku pokud to bylo pres search */
            if (Ident.Length > 0)
            {
                _catalogService.GetProductBrowse(model, Ident); //ulozeni vsech produktu z Search do modelu
            }
            /* Pokud nebyl pouzit Search uloz do modelu vsechny produkty z dane kategorie */
            else
            {
                _catalogService.GetAllProductsCategory(model.IdCategory, model);
            }

            if (page == 0)
            {
                page = 1;
            }

            model.CurrentPage = page;


            /****************************************************
             *                   CHECKBOX                       *
             ****************************************************/

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
                model.ProductList = tmpModel.ToList();
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

            /* Pokud se nalezl alespon jeden prirazuji pomocny tmpModel do model.ProductFilter a mazu vse z pomocneho modelu */
            if (isCheckColour == 1)
            {
                model.ProductList = tmpModel.GroupBy(i => i.id_pr)
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

            /* Pokud se nalezl alespon jeden prirazuji pomocny tmpModel do model.ProductFilter a mazu vse z pomocneho modelu */
            if (isCheckSize == 1)
            {
                model.ProductList = tmpModel.GroupBy(i => i.id_pr)
                   .Select(g => g.First()).ToList();
                tmpModel.Clear();
            }


            /****************************************************
             *             RAZENI PODLE OBLIBENOSTI             *
             ****************************************************/

            /* Pokud jsem jeste nefiltroval podle oblibenosti, 
             * nebo uz jsem filtroval nastavim checkFilter na true a pozdeji ho pouzivam u razeni podle ceny */
            model.checkFilter = false;
            if (LikeNumbers == 1 || LikeNumbers == 3)
            {
                model.checkFilter = true;
            }

            model.FilterFavouriteOn = "btn btn-default";
            model.FilterHighOn = "btn btn-default";
            model.FilterLowOn = "btn btn-default";

            /* Kontroluji jestli uz jsem kliknul na tlacitko radit podle oblibenosti a pokud ano je v LikeNumbers 2 nebo 3, pokud ne je v LikeNumbers 1
             * zaroven kontroluji, aby razeni podle cen bud nebyla jeste kliknuta: SortFromLow nebo SortFromHigh se rovnaji 1,
             * nebo prave uz byla: SortFromLow nebo SortFromHigh se rovnaji 3 a chci to zmenit */
            if (LikeNumbers > 1 && (SortFromLow == 3 || SortFromLow == 1) && (SortFromLow == 3 || SortFromLow == 1))
            {
                _catalogService.SortFavourite(model);
                model.NumbersLike += 2; //zvysuji o dve a pres model se posle do View a kdyz budu chtit dal neco filtrovat vrati se 3 z View do LikeNumbers
                model.SortHigh = 1;
                model.SortLow = 1;
                model.FilterFavouriteOn = "btn btn-warning";
            }


            /****************************************************
             *             RAZENI PODLE CENY                    *
             ****************************************************/

            if (PriceMin.Value != 10 || PriceMax != 1995)
            {
                _catalogService.SortByPrice(model, tmpModel, PriceMax.Value, PriceMin.Value);
            }


            /* Kontroluji jestli uz jsem kliknul na tlacitko radit podle ceny pokud ano je v SortFromHigh 2 nebo 3, pokud ne je v SortFromHigh 1
              * zaroven kontroluji, aby razeni podle nejlevnejsiho bud nebyla jeste kliknuta: SortFromLow se rovna 1,
              * nebo prave uz byla: SortFromLow se rovna 3 a chci to zmenit a stejnym zpusobem uz mam zkontrolovano jestli kliknul nebo ne na razeni podle oblibenosti*/
            if (SortFromHigh > 1 && (SortFromLow == 3 || SortFromLow == 1) && model.checkFilter)
            {
                _catalogService.SortFromHighest(model);
                model.SortHigh += 2; //zvysuji o dve a vzdy se mi potom z View vrati hodnota 3 v SortFromHigh 
                model.SortLow = 1; //nastavuji na 1 a muze se zmenit pouze v pripade ze uzivatel bude chtit radit od nejlevnejsiho
                model.NumbersLike = 1;
                model.FilterHighOn = "btn btn-warning";
            }

            /* Opacny pripad viz. vyse */
            if (SortFromLow > 1 && (SortFromHigh == 3 || SortFromHigh == 1) && model.checkFilter)
            {
                _catalogService.SortFromLowest(model);
                model.SortLow += 2; //zvysuji o dve a vzdy se mi potom z View vrati hodnota 3 v SortFromLow
                model.SortHigh = 1;//nastavuji na 1 a muze se zmenit pouze v pripade ze uzivatel bude chtit radit od nejdrazsiho
                model.NumbersLike = 1;
                model.FilterLowOn = "btn btn-warning";
            }


            /* Pokud jsem neprovadel filtr pres Search, ale jen z kategorii zobrazim dany pocet produktu na urcenou stranku */
            if (Ident.Length == 0)
            {
                _catalogService.GetFewBrowse(model, page);
            }
            /* Pokud jsem provadel Search ulozim do ViewData true a pracuji s nim ve View */
            else
            {
                _catalogService.GetSortSearch(model);
                ViewData["FirmSearch"] = true;
            }

            var user = await GetCurrentUserAsync();

            if (user != null)
            {
                foreach (var product in model.ProductFilter)
                {
                    if (_businessService.AlreadyHasThisProductInList(user.Id, product.id_pr))
                    {
                        model.HaveThisProductInWishList.Add(1); // ano, je ve wishListu
                    }
                    else
                    {
                        model.HaveThisProductInWishList.Add(0); // ne, produkt do wishlistu naseho Usera nepatri
                    }
                }

                foreach (var latestOfer in model.LatestOffer)
                {
                    if (_businessService.AlreadyHasThisProductInList(user.Id, latestOfer.id_pr))
                    {
                        model.HaveLatestProductInWishList.Add(1); // ano, je ve wishListu
                    }
                    else
                    {
                        model.HaveLatestProductInWishList.Add(0); // ne, produkt do wishlistu naseho Usera nepatri
                    }

                }
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

            viewModel.SortHigh = 1;
            viewModel.SortLow = 1;
            viewModel.NumbersLike = 1; // pomoci toho filtruji podle oblibenosti

            viewModel.FilterFavouriteOn = "btn btn-default";
            viewModel.FilterHighOn = "btn btn-default";
            viewModel.FilterLowOn = "btn btn-default";

            viewModel.minPrice = 1;
            viewModel.maxPrice = 1;
            ViewData["CategorySearch"] = true;
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

            AllProductsInOne.SortHigh = 1;
            AllProductsInOne.SortLow = 1;
            AllProductsInOne.NumbersLike = 1;


            AllProductsInOne.FilterFavouriteOn = "btn btn-default";
            AllProductsInOne.FilterHighOn = "btn btn-default";
            AllProductsInOne.FilterLowOn = "btn btn-default";

            AllProductsInOne.minPrice = 1;
            AllProductsInOne.maxPrice = 1;
            ViewData["ProductSearch"] = true;
            return View("Category", AllProductsInOne);
        }

        public IActionResult FirmSearch(string SearchString)
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
            for (int i = 0; i < FirmsAmount; ++i)
            {
                NotPermanentList = _catalogService.GetProductByFirmId(firms[i].id_fir); // List FilterProductu
                foreach (var product in NotPermanentList)
                {
                    AllProductsInOne.ProductFilter.Add(product);
                }
            }

            /* Pomoci toho pozdeji filtruji podle ceny */
            AllProductsInOne.SortHigh = 1;
            AllProductsInOne.SortLow = 1;
            AllProductsInOne.NumbersLike = 1;

            AllProductsInOne.minPrice = 1;
            AllProductsInOne.maxPrice = 1;
            ViewData["FirmSearch"] = true;


            AllProductsInOne.FilterFavouriteOn = "btn btn-default";
            AllProductsInOne.FilterHighOn = "btn btn-default";
            AllProductsInOne.FilterLowOn = "btn btn-default";


            return View("Category", AllProductsInOne);
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
            {
                return _userManager.GetUserAsync(HttpContext.User);
            }
        }
    }
    
