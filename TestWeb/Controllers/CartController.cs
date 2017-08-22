using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog.Business;
using Alza.Core.Identity.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Module.Order.Business;
using Alza.Core.Module.Http;
using Module.Order.Dal.Entities;

namespace PernicekWeb.Controllers
{
    public class CartController : Controller
    {
        public readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly CatalogService _catalogservice;
        public readonly OrderService _orderservice;
        

        public CartController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            CatalogService catalogservice,
            OrderService orderservice
            )
        {
            _catalogservice = catalogservice;
            _userManager = userManager;
            _signInManager = signInManager;
            _orderservice = orderservice;
        }
        
    }
}