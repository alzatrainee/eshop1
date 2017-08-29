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
        [HttpGet]

        public async Task<ActionResult> bla()
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

            return View(cartItems);
        }

        // Add item to cart
        //
        // GET: /Cart/Order
        [HttpGet]
        public async Task<ActionResult> Index(int Idecko, string Colours, int Sizes)
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

            tmp = _businessservice.AddToCart(cart.id_car, Idecko, Sizes, Colours);

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
                Firm = firm.name/*,
                colour = item.colour,
                size = item.size
            };
            viewModel.OrdProd.Add(viewModel);   */   
          //  return RedirectToAction(nameof(PlaygroundController.Index), "Index", viewModel);
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody]OrderProduct viewModel)
        {

            var user = await _userManager.GetUserAsync(User);
            var result = _businessservice.GetProductsCart(user.Id);

            foreach (var item in result)
            {
                var product = _catalogservice.GetProduct(item.id_pr);
                var image = _catalogservice.GetImage(item.id_pr);
               // var firm = _catalogservice.GetFirm(product.id_fir);
                //viewModel.id_pr = item.id_pr;
                viewModel.nameProduct = product.name;
                viewModel.Price = item.amount * product.price;
                viewModel.image = image.link;
               //     Firm = firm.name
               /*,
                colour = item.colour,
                size = item.size*/

                viewModel.OrdProd.Add(viewModel);
            }
            return Json(viewModel);
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

        [HttpGet]
        public async Task<ActionResult> Order(OrderProduct model)
        {
            var user = await _userManager.GetUserAsync(User);
            //var result = _businessservice.GetProductsCart(user.Id);
            var tmp = _businessservice.GetCartItems(user.Id);
            var result = (List<Cart_pr>)tmp.data;



            foreach (var item in result)
            {
                var product = _catalogservice.GetProduct(item.id_pr);
                var image = _catalogservice.GetImage(item.id_pr);
                var firm = _catalogservice.GetFirm(product.id_fir);
                model.id_pr = item.id_pr;
                model.nameProduct = product.name;
                model.Price = item.amount * product.price;
                model.image = image.link;
                model.Firm = firm.name;
                model.amount = item.amount;
                model.size = item.Size.uk;
                model.colour = _catalogservice.GetColour(item.id_col).name;
                //model.colour
                //     Firm = firm.name
                /*,
                 colour = item.colour,
                 size = item.size*/

                model.OrdProd.Add(model);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> FinishOrder(int? Payment, int? ShippingOption, OrderProduct model)
        {
            decimal sumPrice = 0;

            var user = await _userManager.GetUserAsync(User);

            if ( Payment == null) // nesmi nastat, uzivatel si musi vybrat zpusob platby
            {
                return View();
            }

            if (ShippingOption == null)
            {
                ShippingOption = 1; // nesmi nastat, uzivatel si musi vybrat dopravu
            }
            

            /* Pridani adresy do databaze */
            var address = new Address(model.street, model.city, model.house_number, model.post_code);
            _orderService.AddAddress(address);

            var payment = new Payment(Payment.Value, 1); // 1 je payment status
            _orderService.AddPayment(payment);

            /* Vytvoreni NewOrder a prida do databaze bez id_pay */
            var NewOrder = new NewOrder(user.Id, 1, address.id_ad, ShippingOption.Value, payment.id_pay); // 1 je status objednavky
            _orderService.AddNewOrder(NewOrder);


            /* Ziskani produkty z Cart_pr podle usera a vlozeni do databaze Order_prod */
            var orderProd = _businessservice.GetConnectCart(user.Id);
            foreach (var item in orderProd)
            {
                var orPr = new Order_prod(NewOrder.id_ord, item.id_pr, item.amount);
                var pri = _catalogservice.GetProduct(item.id_pr);
                sumPrice += pri.price;
                _businessservice.AddOrder_prod(orPr);
                /* Vymazani produktu z Cart_pr */
                _businessservice.DeleteCart_pr(item);
            }


            /* Vypocitani celkove ceny plus pridani Payment do databaze */
             var ship = _orderService.GetPriceShipping(ShippingOption.Value);
            sumPrice += ship.price;
            payment.price = sumPrice;
            _orderService.UpdatePayment(payment);


            /* Pokus o pridani id_pay do NewOrder 
             NewOrder.id_pay = payment.id_pay;
             _orderService.UpdateNewOrder(NewOrder);*/


            return View();
        }
    }
}
