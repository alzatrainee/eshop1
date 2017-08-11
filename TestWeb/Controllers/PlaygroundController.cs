﻿using Catalog.Business;
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

        public IActionResult Index()
        {
            List<Product> Products = new List<Product>();
            var allProducts = _catalogService.GetAllProducts();
            var velAllProducts = allProducts.Count();
            for (var j = 0; j < velAllProducts; j++)
            {
               
                var result = _catalogService.GetProduct(j);
                var image = _catalogService.GetImage(result.id_pr); // pole, ktere zahrnuje vsechny images patrici vybranemu productu
                var firm = _catalogService.GetFirm(result.id_fir);
                              
                var model = new Product
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

        public IActionResult Filter()
        {
            var category = new Category
            {
                id_cat = 6
            };
            return View(category);
        }
        public IActionResult Category(int? id) //do ide se ulozi adidas zkusit to filtrovat jeste pred vypisem vseho pomocti getfirmname
        {
            List<Product> Products = new List<Product>();
           // var allProducts = _catalogService.GetAllProducts();
            var cate = _catalogService.GetProductCategory(id.Value);
           // var velAllProducts = allProducts.Count();
           
                //var cate = _catalogService.GetProductCategory(id.Value);
                //var cat = _catalogService.GetCategory(id.Value);
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
            /* if (ide != null)
             {
                 Products = Products.Where(s => s.id_fir == ide.Value).ToList();
             }*/
            //  IEnumerable<Product> results = Products.Where(s => s.firm.Contains(ide));

            ViewData["Category"] = id.Value;



            return View(Products);
        }

        public IActionResult CategoryFilter (int? id, int? ide) //do ide se ulozi adidas zkusit to filtrovat jeste pred vypisem vseho pomocti getfirmname
        {
            List<Product> Products = new List<Product>();
            // var allProducts = _catalogService.GetAllProducts();
            var cate = _catalogService.GetProductCategory(id.Value);
            // var velAllProducts = allProducts.Count();

            //var cate = _catalogService.GetProductCategory(id.Value);
            //var cat = _catalogService.GetCategory(id.Value);
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

    }
}
