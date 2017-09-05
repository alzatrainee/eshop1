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
using System.Linq;

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
        public async Task<ActionResult> Add(int id, int size, string colour)
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
            if (user == null)
                return RedirectToAction("Login");


            var tmp = _businessservice.GetCart(user.Id);

            if (tmp.isEmpty)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;

            Cart_pr cartItem = new Cart_pr(cart.id_car, id, 1, size, colour);

            tmp = _businessservice.AddToCart(cartItem);

            var item = (Cart_pr)tmp.data;

            return RedirectToAction("Order");
        }


        [HttpGet]
        public async Task<IActionResult> Index(int Idecko, string Colours, int Sizes)
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
            if (user == null)
                return RedirectToAction("Register","Account");




            var tmp = _businessservice.GetCart(user.Id);

            if (tmp.isEmpty)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;

            Cart_pr cartItem = new Cart_pr(cart.id_car, Idecko, 1, Sizes, Colours);

            tmp = _businessservice.AddToCart(cartItem);

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
            //string red = Request.Headers["Referer"].ToString();
            return RedirectToLocal(Request.Headers["Referer"].ToString());
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Refresh([FromBody]OrderProduct viewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _businessservice.GetProductsCart(user.Id);

            foreach (var item in result)
            {
                var product = _catalogservice.GetProduct(item.id_pr);
                var image = _catalogservice.GetImage(item.id_pr);
                var firm = _catalogservice.GetFirm(product.id_fir);
                var model = new OrderProduct
                {
                    id_pr = item.id_pr,
                    nameProduct = product.name,
                    amount = item.amount * product.price,
                    image = image.link,
                    Firm = firm.name,
                    Price = product.price,
                    quantity = item.amount,
                    colour = _catalogservice.GetColour(item.id_col).name,
                    size = item.Size.uk,
                    id_col = _catalogservice.GetColour(item.id_col).rgb,
                    id_si = item.Size.id_si
                };
                viewModel.OrdProd.Add(model);
            }
            return Json(viewModel);
        }




        // Delete item from Cart
        //
        // GET: /Cart/Remove
        [HttpGet]
        public async Task<ActionResult> Remove(int id, int size, string colour)

        {
            var user = await _userManager.GetUserAsync(User);



            var tmp = _businessservice.GetCart(user.Id);

            if (tmp.isEmpty)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;

            Cart_pr CartItem = new Cart_pr(cart.id_car, id, 1, size, colour);
            tmp = _businessservice.GetCartItem(CartItem);
            var item = (Cart_pr)tmp.data;

            _businessservice.DecreaseAmount(item);

            return RedirectToAction("Order");
        }

        [HttpGet]
        public async Task<ActionResult> RemoveItem(int id, int size, string colour)

        {
            var user = await _userManager.GetUserAsync(User);



            var tmp = _businessservice.GetCart(user.Id);

            if (tmp.isEmpty)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;

            Cart_pr CartItem = new Cart_pr(cart.id_car, id, 1, size, colour);
            tmp = _businessservice.GetCartItem(CartItem);
            var item = (Cart_pr)tmp.data;

            _businessservice.DeleteCart_pr(item);

            return RedirectToAction("Order");
        }






        /////////////////////////////////////
        ////////// Dulezite pro AJAX////////   /////////////////// Nudle, Nemazat ! /////////////////////
        /// /////////////////////////////////
        [HttpPost]
        public async Task<ActionResult> RemoveAJAX([FromBody]ModelOrderAJAX model)
        {
            var user = await _userManager.GetUserAsync(User);



            var tmp = _businessservice.GetCart(user.Id);

            if (tmp.isEmpty)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;

            Cart_pr CartItem = new Cart_pr(cart.id_car, model.id, 1, model.size, model.colour);
            tmp = _businessservice.GetCartItem(CartItem);
            var item = (Cart_pr)tmp.data;


            _businessservice.DeleteCart_pr(item);

            return Json(model);
        }


        [HttpGet]
        public async Task<ActionResult> DumpCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);



            var tmp = _businessservice.GetCart(user.Id);

            if (tmp.isEmpty)
            {
                throw new Exception("Cart not found.");
            }

            var cart = (Cart)tmp.data;

            _businessservice.DumpCart(cart.id_car);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Order(OrderProduct viewModel)
        {
            viewModel = await OrderShow();
            var countries = _orderService.GetAllCountries();
            viewModel.Country = countries;
            return View(viewModel);
        }

        public async Task<OrderProduct> OrderShow()
        {
            OrderProduct viewModel = new OrderProduct();
            decimal sumPrice = 0;
            var user = await _userManager.GetUserAsync(User);
            var result = _businessservice.GetProductsCart(user.Id);

            foreach (var item in result)
            {
                var product = _catalogservice.GetProduct(item.id_pr);
                var image = _catalogservice.GetImage(item.id_pr);
                var firm = _catalogservice.GetFirm(product.id_fir);
                var model = new OrderProduct
                {
                    id_pr = item.id_pr,
                    nameProduct = product.name,
                    Price = product.price,
                    image = image.link,
                    Firm = firm.name,
                    amount = item.amount * product.price,
                    colour = _catalogservice.GetColour(item.id_col).name,
                    id_col = item.id_col,
                    size = item.Size.uk,
                    id_si = item.Size.id_si,
                    quantity = item.amount
                };
                sumPrice += model.amount;
                viewModel.OrdProd.Add(model);
            }

            viewModel.OverallPrice = sumPrice;
            var addTmp = _orderService.GetNewOrderList(user.Id);
            if (addTmp.Count > 0)
            {
                foreach (var item in addTmp)
                {
                    var address = _orderService.FindSpecificAddress(item.id_ad);
                    var tmpAddress = viewModel.AddressCheck.Where(p => p.id_ad == item.id_ad).FirstOrDefault();
                    if (tmpAddress == null)
                    {
                        var country = _orderService.GetState(address.country);
                        var addressModel = new OrderProduct
                        {
                            street = address.street,
                            house_number = address.house_number,
                            city = address.city,
                            post_code = address.post_code,
                            id_ad = item.id_ad,
                            nameCountry = country.name
                        };
                        viewModel.AddressCheck.Add(addressModel);
                    }
                }
            }
            else
            {
                var address = _orderService.FindAddresByIdUser(user.Id);
                if (address != null)
                {
                    var country = _orderService.GetState(address.country);
                    viewModel.street = address.street;
                    viewModel.city = address.city;
                    viewModel.house_number = address.house_number;
                    viewModel.post_code = address.post_code;
                    var addressModel = new OrderProduct
                    {
                        street = address.street,
                        city = address.city,
                        house_number = address.house_number,
                        post_code = address.post_code,
                        id_ad = address.id_ad,
                        nameCountry = country.name
                    };
                    viewModel.AddressCheck.Add(addressModel);
                }
            }
            return viewModel;
        }

        [HttpPost]
        public async Task<IActionResult> Order(int? ShippingOption, int? Payment, int? AddressChoose, int? Country, OrderProduct viewModel)
        {
            decimal sumPrice = 0;
            if (Payment == null) // nesmi nastat, uzivatel si musi vybrat zpusob platby
            {
                viewModel = await OrderShow();
                return View(viewModel);
            }

            if (ShippingOption == null)
            {
                viewModel = await OrderShow();
                return View(viewModel);
            }

            var user = await _userManager.GetUserAsync(User);
            var result = _businessservice.GetProductsCart(user.Id);

            foreach (var item in result)
            {
                var product = _catalogservice.GetProduct(item.id_pr);
                var image = _catalogservice.GetImage(item.id_pr);
                var firm = _catalogservice.GetFirm(product.id_fir);
                var model = new OrderProduct
                {
                    id_pr = item.id_pr,
                    nameProduct = product.name,
                    Price = product.price,
                    image = image.link,
                    Firm = firm.name,
                    quantity = item.amount,
                    colour = _catalogservice.GetColour(item.id_col).name,
                    id_col = item.id_col,
                    size = item.Size.uk,
                    id_si = item.Size.id_si,
                    amount = product.price * item.amount
                };
                sumPrice += model.amount;
                viewModel.OrdProd.Add(model);
            }
            var shipping = _orderService.GetPriceShipping(ShippingOption.Value);
            var method = _orderService.GetPaymentMethod(Payment.Value);

            viewModel.OverallPrice = sumPrice;
            viewModel.PaymentMethodNumber = method.id_meth;
            viewModel.ShippingOptionNumber = shipping.id_ship;

            viewModel.ShippingOption = shipping.name;
            viewModel.Payment = method.name;
            if (AddressChoose != null)
            {
                var address = _orderService.FindSpecificAddress(AddressChoose.Value);
                var country = _orderService.GetState(address.country);
                viewModel.street = address.street;
                viewModel.house_number = address.house_number;
                viewModel.city = address.city;
                viewModel.post_code = address.post_code;
                viewModel.id_ad = AddressChoose.Value;
                viewModel.nameCountry = country.name;
                viewModel.codeCountry = country.code;
            }
            if (Country != null)
            {
                viewModel.codeCountry = Country.Value;
            }
            

            if (viewModel.street == null || viewModel.house_number == 0 || viewModel.codeCountry == 0 || viewModel.city == null || viewModel.post_code == 0)
            {
                viewModel = await OrderShow();
                return View(viewModel);
            }
           
            return View("Summary", viewModel);
        }

        

        [HttpPost]
        public async Task<ActionResult> FinishOrder(int? ShippingOption, int? Payment, int? AddressChoose, OrderProduct model)
        {
            decimal sumPrice = 0;

            var user = await _userManager.GetUserAsync(User);

            if (Payment == null) // nesmi nastat, uzivatel si musi vybrat zpusob platby
            {
                return View();
            }

            if (ShippingOption == null)
            {
                return View(); // nesmi nastat, uzivatel si musi vybrat dopravu
            }

            int addressId;
            //var addTmp = _orderService.GetNewOrder(user.Id);
            //if (addTmp != null)
            //{
            //    address = _orderService.FindSpecificAddress(addTmp.id_ad);
            //    address.street = model.street;
            //    address.city = model.city;
            //    address.house_number = model.house_number;
            //    address.post_code = model.post_code;

            //    _orderService.UpdateAddress(address);
            //}
            //else
            //{
            
            if (model.id_ad == 0)
            {

                /* Pridani adresy do databaze */
          //      var country = new Country();
                var address = new Address(model.street, model.city, model.house_number, model.post_code, model.codeCountry);
                _orderService.AddAddress(address);
                addressId = address.id_ad;
            }
            else
            {
                addressId = model.id_ad;
            }

            var payment = new Payment(Payment.Value, 1, 0); // 1 je payment status
            _orderService.AddPayment(payment);

            /* Vytvoreni NewOrder a prida do databaze bez id_pay */
            var date = DateTime.Today;
            var NewOrder = new NewOrder(user.Id, 1, addressId, ShippingOption.Value, payment.id_pay, date); // 1 je status objednavky
            _orderService.AddNewOrder(NewOrder);


            /* Ziskani produkty z Cart_pr podle usera a vlozeni do databaze Order_prod */
            var orderProd = _businessservice.GetConnectCart(user.Id);
            foreach (var item in orderProd)
            {
                var orPr = new Order_prod(NewOrder.id_ord, item.id_pr, item.amount, item.id_col, item.id_si);
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

        public async Task<IActionResult> DeleteAll()
        {
            string tmp = Request.Headers["Referer"].ToString();
            var user = await _userManager.GetUserAsync(User);

            var orderProd = _businessservice.GetConnectCart(user.Id);
            foreach (var item in orderProd)
            {
                /* Vymazani produktu z Cart_pr */
                _businessservice.DeleteCart_pr(item);
            }

            return RedirectToLocal(tmp);

        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect(returnUrl);
            }
        }
    }
}
