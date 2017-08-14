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
using PernicekWeb.Models.ItemViewModels;

namespace Pernicek.Controllers
{
    public class ItemController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly IProductRepository _iProductRepository;
        private readonly Iprod_colRepository _iprod_colRepository;
        private readonly IProd_siRepository _iProd_siRepository;
        private readonly IImageRepository _iImageRepository;
        private readonly IFirmRepository _iFirmRepository;
        private readonly IProduct_catRepository _iProduct_catRepository;
        private readonly ICat_subRepository _iCat_subRepository;
        private readonly ICategoryRepository _iCategoryRepository;

        public ItemController(CatalogService catalogservice, IProductRepository iProductRepository, Iprod_colRepository iprod_colRepository, IProd_siRepository iProd_siRepository,
                              IImageRepository iImageRepository, IFirmRepository iFirmRepository, IProduct_catRepository iProduct_catRepository, ICat_subRepository iCat_subRepository,
                              ICategoryRepository iCategoryRepository
            )
        {
            _iProductRepository = iProductRepository;
            _catalogService = catalogservice;
            _iprod_colRepository = iprod_colRepository;
            _iProd_siRepository = iProd_siRepository;
            _iImageRepository = iImageRepository;
            _iFirmRepository = iFirmRepository;
            _iProduct_catRepository = iProduct_catRepository;
            _iCat_subRepository = iCat_subRepository;
            _iCategoryRepository = iCategoryRepository;
        }

        public IActionResult Index(int? id)
        {

            var result = _catalogService.GetProduct(id.Value);
            var res = _catalogService.GetRgb(result.id_pr);
            var size = _catalogService.GetID_size(result.id_pr);
            var image = _catalogService.getAllImages(result.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
            var firm = _catalogService.GetFirm(result.id_fir);
            var product_cat = _catalogService.Get_Product_cat(result.id_pr);
            var number_of_product_cat = product_cat.Count(); // je vzdy 2 
            List<Catalog.Dal.Entities.Cat_sub> cat_sub = new List<Catalog.Dal.Entities.Cat_sub>();


            for( var i = 0; i < number_of_product_cat; ++i) // maximalni pocet iteraci je vzdy 2
            {
                cat_sub.Add(_catalogService.GetCat_Sub(product_cat[i].id_cs));
            }

            List<Catalog.Dal.Entities.Category> categories = new List<Catalog.Dal.Entities.Category>();

            for( var i = 0; i < 2; ++i )
            {
                categories.Add(_catalogService.GetCategory(cat_sub[i].id_cat));
            }

            //List<Catalog.Dal.Entities.Colour> pom = new List<Catalog.Dal.Entities.Colour>();

            // var cat_sub = _catalogService.GetCat_Sub(category.);
            var velikost = res.Count(); // jaky je pocet barev patricich danemu produktu 
            var velikost_size = size.Count(); // pocet vsech velikosti u vybraneho produktu
            var number_of_images = image.Count(); //  pocet vsech obrazku daneho productu
            List<Catalog.Dal.Entities.Colour> pom = new List<Catalog.Dal.Entities.Colour>(); // vytvarime pole, ktere by melo v sobe obsahovat vsechny barvy tohoto produktu  
            List<Catalog.Dal.Entities.Size> array_sizes = new List<Catalog.Dal.Entities.Size>(); // pole, ktere zahrnuje vsechny velikosti vybraneho produktu 



            for (var i = 0; i < velikost; ++i)
            {
                pom.Add(_catalogService.GetColour(res[i].rgb));
            }
            for (var i = 0; i < velikost_size; ++i)
            {
                array_sizes.Add(_catalogService.GetSize(size[i].id_si));

            }
            var model = new Product
            {

                name = result.name,
                date = result.date,
                price = result.price,
                description = result.description,
                firm = firm.name,
                colour = new string[velikost], // vytvorime pole colour pro vypis vsech 
                size = new int[velikost_size],
                image = new string[number_of_images],
                category = categories[1].name,
                sub_category = categories[0].name
            };

            for (var i = 0; i < velikost; ++i) //paradni for-cyklus, ktery ti prida do View vsechny barvy produktu, jenze vypise to bez mezer, ale je to problem View()
            {
                model.colour[i] = pom[i].name;
            }
            for (var i = 0; i < velikost_size; ++i)
            {
                model.size[i] = array_sizes[i].uk;
            }

            for (var i = 0; i < number_of_images; ++i)
            {
                model.image[i] = image[i].link;
            }

            return View(model);


        }

        public IActionResult Item()
        {
            return View();
        }
        
    }
}
