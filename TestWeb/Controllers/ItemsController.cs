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

namespace PernicekWeb.Controllers
{
    public class ItemsController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly IProductRepository _iProductRepository;
        private readonly Iprod_colRepository _iprod_colRepository;
        private readonly CatalogDbContext _context;

        public ItemsController(CatalogService catalogservice, IProductRepository iProductRepository, Iprod_colRepository iprod_colRepository, CatalogDbContext context)
        {
            _iProductRepository = iProductRepository;
            _catalogService = catalogservice;
            _iprod_colRepository = iprod_colRepository;
            _context = context;
        }

        //
        // POST: /Items/Browse
        public IActionResult Browse()
        {
            return View();
        }

        public IActionResult Item()
        {
           return View();
        }


        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var item = await _context.Product.ToListAsync();
            return View(item);
        }
    }
}
