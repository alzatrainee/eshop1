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
using PernicekWeb.Models.PlaygroundViewModels;

namespace Pernicek.Controllers
{
    public class PlaygroundController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly IProductRepository _iProductRepository;
        private readonly Iprod_colRepository _iprod_colRepository;

        public PlaygroundController(CatalogService catalogservice, IProductRepository iProductRepository, Iprod_colRepository iprod_colRepository)
        {
            _iProductRepository = iProductRepository;
            _catalogService = catalogservice;
            _iprod_colRepository = iprod_colRepository;
        }

        public IActionResult Index()
        {

            var result = _catalogService.GetProduct(0);
            var res = _catalogService.GetRgb(result.id_pr);
         //   var size = _catalogService.GetId_size(result.id_pr);
            var velikost = res.Count(); // jaky je pocet barev patricich danemu produktu 
          //  var velikost_size = size.Count(); // pocet vsech velikosti u vybraneho produktu
            List<Catalog.Dal.Entities.Colour> pom = new List<Catalog.Dal.Entities.Colour>(); // vytvarime pole, ktere by melo v sobe obsahovat vsechny barvy tohoto produktu  
           // List<Catalog.Dal.Entities.Size> array = new List<Catalog.Dal.Entities.Size>(); // pole, ktere zahrnuje vsechny velikosti vybraneho produktu 


            for( var i = 0; i < velikost; ++i )
            {
                pom.Add(_catalogService.GetColour(res[i].rgb));
            }
          //  for( var i = 0; i < velikost_size; ++i)
            //{
              //  array.Add(_catalogService.GetSize(size[i].id_pr));
            //}


            var model = new Product {

                name = result.name,
                date = result.date,
                price = result.price,
                description = result.description,
                colour = new string[velikost], // vytvorime pole colour pro vypis vsech 
               // size = new string[velikost_size]
            };

            for( var i = 0; i < velikost; ++i) //paradni for-cyklus, ktery ti prida do View vsechny barvy produktu, jenze vypise to bez mezer
            {
                model.colour[i] = pom[i].name;
            }
         //   for( var i = 0; i < velikost_size; ++i )
           // {
             //   model.size[i] = array[i].euro;
           // }
            
            return View(model);
            
            
        }
    }
}
