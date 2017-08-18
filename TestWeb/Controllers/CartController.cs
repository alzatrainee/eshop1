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
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);



            var tmp = _orderservice.GetCart(user.Id);

            if (!tmp.isOK)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;

            tmp = _orderservice.GetCartItems(cart.id_car);
            var cartItems = (List<Cart_pr>)tmp.data;
            
            ViewData["CartItem"] = cartItems ;
            return View(cartItems);
        }

        // Add item to cart
        //
        // GET: /Cart/Order
        [HttpGet]
        public async Task<ActionResult> Order(int id)
        {
            /*
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
            }
            else
            {
                //TODO
                var user = await _userManager.GetUserAsync(User);
            }
            */
            var user = await _userManager.GetUserAsync(User);



            var tmp = _orderservice.GetCart(user.Id);

            if(!tmp.isOK)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;

            tmp = _orderservice.AddToCart(cart.id_car, id);

            var item = (Cart_pr)tmp.data;
           
           

            
            





            return View();
        }

        // Delete item from Cart
        //
        // GET: /Cart/Remove
        [HttpGet]
        public async Task<ActionResult> Remove(int id)
        {
            var user = await _userManager.GetUserAsync(User);



            var tmp = _orderservice.GetCart(user.Id);

            if (!tmp.isOK)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;


            tmp = _orderservice.GetCartItem(cart.id_car, id);
            var item = (Cart_pr)tmp.data;

            _orderservice.RemoveFromCart(item);

            return View();
        }
    }
}