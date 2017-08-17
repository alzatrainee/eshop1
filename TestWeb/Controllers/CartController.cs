using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Catalog.Business;
using Alza.Core.Identity.Dal.Entities;
using Microsoft.AspNetCore.Identity;

namespace PernicekWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly CatalogService _catalogservice;
        public readonly SignInManager<ApplicationUser> _signInManager;

        public CartController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            CatalogService catalogservice)
        {
            _catalogservice = catalogservice;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        // Add item to cart
        //
        // GET: /Cart/Order
        [HttpGet]
        public async Task<ActionResult> Order(int id)
        {

            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
            }
            else
                throw new NotImplementedException();



            return View();
        }

        public ActionResult RemoveFromCart(int id)
        {
            throw new NotImplementedException();
        }
    }
}