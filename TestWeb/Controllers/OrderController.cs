using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Module.Order.Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Alza.Core.Identity.Dal.Entities;
using Module.Order.Business;
using Module.Business.Dal.Entities;
using Module.Business.Business;
using Module.Business.Dal.Entity;
using Pernicek.Models.PlaygroundViewModels;

namespace PernicekWeb.Controllers
{
    public class OrderController : Controller
    {

        public readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly CatalogService _catalogservice;
        public readonly BusinessService _businessservice;
        public readonly OrderService _orderService;


        public OrderController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            CatalogService catalogservice,
            BusinessService businessservice,
             OrderService orderService
            )
        {
            _catalogservice = catalogservice;
            _userManager = userManager;
            _signInManager = signInManager;
            _businessservice = businessservice;
            _orderService = orderService;
        }
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);



            var tmp = _businessservice.GetCart(user.Id);

            if (!tmp.isOK)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;

            tmp = _businessservice.GetCartItems(cart.id_car);
            var cartItems = (List<Cart_pr>)tmp.data;

            ViewData["CartItem"] = cartItems;
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



            var tmp = _businessservice.GetCart(user.Id);

            if (tmp.isEmpty)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;

            tmp = _businessservice.AddToCart(cart.id_car, id);

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



            var tmp = _businessservice.GetCart(user.Id);

            if (tmp.isEmpty)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;


            tmp = _businessservice.GetCartItem(cart.id_car, id);
            var item = (Cart_pr)tmp.data;

            _businessservice.RemoveFromCart(item);

            return View();
        }

        public async Task<ActionResult> NewOrder(int? Payment, int? Shipping, PlaygroundViewModel model)
        {
            decimal sumPrice = 0;

            var address = new Address(model.street, model.city, model.house_number, model.post_code);

            var user = await _userManager.GetUserAsync(User);
            var NewOrder = new NewOrder(user.Id, 1, address.id_ad, Shipping.Value);

            var orderProd = _businessservice.GetConnectCart(user.Id);
            foreach (var item in orderProd)
            {
                var orPr = new Order_prod(NewOrder.id_ord, item.id_pr, item.amount);
                sumPrice += item.Product.price;
            }

            var ship = _orderService.GetPriceShipping(Shipping.Value);
            sumPrice += ship.price;
            var payment = new Payment(Payment.Value, 1, sumPrice);
            NewOrder.id_pay = payment.id_pay;

            return View();
        }
    }
}
