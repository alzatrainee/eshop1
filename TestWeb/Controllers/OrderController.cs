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
using PernicekWeb.Models.OrderViewModels;
using Pernicek.Controllers;

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
        public async Task<ActionResult> Order(int Idecko, string Colours, int Sizes, OrderProduct model)
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

            tmp = _businessservice.AddToCart(cart.id_car, Idecko);

            var item = (Cart_pr)tmp.data;
            /*
            var product = _catalogservice.GetProduct(item.id_pr);
            var image = _catalogservice.GetImage(item.id_pr);
            var firm = _catalogservice.GetFirm(product.id_fir);
            var viewModel = new OrderProduct
            {
                id_pr = item.id_pr,
                nameProduct = product.name,
                Price = item.amount * product.price,
                image = image.link,
                Firm = firm.name,
                colour = item.colour,
                size = item.size
            };
            model.OrdProd.Add(viewModel);      
            return RedirectToAction(nameof(PlaygroundController.Index), "Index", viewModel);
            */
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

        public async Task<ActionResult> NewOrder(int? Payment, int? ShippingOption, OrderProduct model)
        {
            decimal sumPrice = 0;

            if ( Payment == null) // nesmi nastat, uzivatel si musi vybrat zpusob platby
            {
                return View();
            }

            if (ShippingOption == null)
            {
                return View(); // nesmi nastat, uzivatel si musi vybrat dopravu
            }


            /* Pridani adresy do databaze */
            var address = new Address(model.street, model.city, model.house_number, model.post_code);
            _orderService.AddAddress(address);


            /* Vytvoreni NewOrder a prida do databaze bez id_pay */
            var user = await _userManager.GetUserAsync(User);
            var NewOrder = new NewOrder(user.Id, 1, address.id_ad, ShippingOption.Value); // 1 je status objednavky
            _orderService.AddNewOrder(NewOrder);


            /* Ziskani produkty z Cart_pr podle usera a vlozeni do databaze Order_prod */
            var orderProd = _businessservice.GetConnectCart(user.Id);
            foreach (var item in orderProd)
            {
                var orPr = new Order_prod(NewOrder.id_ord, item.id_pr, item.amount);
                sumPrice += item.Product.price; // tady zalezi jestli je to i v tom Productu, pokud ne bude to chtit jinej pristup
                _businessservice.AddOrder_prod(orPr);
            }


            /* Vypocitani celkove ceny plus pridani Payment do databaze */
            var ship = _orderService.GetPriceShipping(ShippingOption.Value);
            sumPrice += ship.price;
            var payment = new Payment(Payment.Value, 1, sumPrice); // 1 je payment status
            _orderService.AddPayment(payment);


            /* Pokus o pridani id_pay do NewOrder */
            NewOrder.id_pay = payment.id_pay;
            _orderService.UpdateNewOrder(NewOrder); //timhle si nejsem jistej bude to chtit otestovat a pripradne upravit

            return View();
        }
    }
}
