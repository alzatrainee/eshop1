﻿using Catalog.Business;
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
            
            return RedirectToLocal(Request.Headers["Referer"].ToString());
        }

        /* Pro modal Shopping Basket - pouzivame Ajax */
        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody]OrderProduct viewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _businessservice.GetProductsCart(user.Id); //zjistuji vsechny produkty v kosiku podle user Id

            foreach (var item in result)
            {
                /* Hledam jednotlive udaje v databazi a prirazuji je do modelu */
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
        /// 

        /* Pro vymazavani produktu z modalu Shopping Basket pomoci Ajax */
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

        /* Pro View Order, pouzivame Get a Post */
        [HttpGet]
        public async Task<IActionResult> Order(OrderProduct viewModel)
        {
            viewModel = await OrderShow(); // zavolam funkci ktera do viewModelu prida dulezite informace
            var countries = _orderService.GetAllCountries(); // nactu si vsechny zeme
            viewModel.Country = countries; // ulozim je do modelu
            return View(viewModel);
        }
       
        /// <summary>
        /// Ulozeni order informaci do modelu
        /// </summary>
        /// <returns></returns>
        public async Task<OrderProduct> OrderShow()
        {
            OrderProduct viewModel = new OrderProduct();
            decimal sumPrice = 0; // pro pocitani celkove ceny

            var user = await _userManager.GetUserAsync(User);
            var result = _businessservice.GetProductsCart(user.Id); // hledam vsechny produkty v kosiku podle user id

            foreach (var item in result)
            {
                /* Hledam informace z databaze a prirazuji je do modelu, ohledne produktu ceny, mnozstvi */
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
                sumPrice += model.amount; //pocitani celkove ceny
                viewModel.OrdProd.Add(model);
            }

            viewModel.OverallPrice = sumPrice; //prirazeni celkove ceny do modelu

            var OrderList = _orderService.GetNewOrderList(user.Id); // nalezam vsechny objedavky uzivatele
            /* zjistuji jestli nejaka objednavka existuje a davam mu potom v orderu na vyber z jeho minulych adres */
            if (OrderList.Count > 0) 
            {
                ViewData["ExistAddress"] = true; // podle toho ve View ukazujeme vyber minulych adres
                foreach (var item in OrderList)
                {
                    var address = _orderService.FindSpecificAddress(item.id_ad); //zjistuji konkretni adresu podle objednavky
                    var tmpAddress = viewModel.AddressCheck.Where(p => p.id_ad == item.id_ad).FirstOrDefault(); //kontroluji jestli jsem ji uz nahodou do modelu nepridal
                    /* Pokud neni jeste v modelu najdu si zemi a pridam adresu do modelu */
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
            /* Pokud jeste zadnou objednavku neprovedl */
            else
            {
                var address = _orderService.FindAddresByIdUser(user.Id); //hledam jestli si adresu nevyplnil sam na profilove strance
                /* Pokud si ji vyplnil ulozim si ji do modelu a zobrazi se mu pri vyplnovani adresy u orderu */
                if (address != null)
                {
                    var country = _orderService.GetState(address.country);
                    //viewModel.street = address.street;
                    //viewModel.city = address.city;
                    //viewModel.house_number = address.house_number;
                    //viewModel.post_code = address.post_code;
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
                    ViewData["ExistAddress"] = true;  // podle toho ve View ukazujeme vyber minulych adres
                }
            }
            return viewModel; // vracim model z kterym pak muzu dal pracovat
        }

        /* Toto zobrazuji ve View Summary */
        [HttpPost]
        public async Task<IActionResult> Order(int? ShippingOption, int? Payment, int? AddressChoose, int? Country, OrderProduct viewModel) 
        {
            decimal sumPrice = 0; // pro pocitani celkove ceny

            /* Pokud si uzivatel nevybere zpusob platby vrati se zpatky na order a musi to znovu vyplnit */
            if (Payment == null) 
            {
                viewModel = await OrderShow();
                ViewData["EmptyPayment"] = true;
                return View(viewModel);
            }

            /* Pokud si uzivatel nevybere zpusob dopravy vrati se zpatky na order a musi to znovu vyplnit */
            if (ShippingOption == null)
            {
                viewModel = await OrderShow();
                ViewData["EmptyShipping"] = true;
                return View(viewModel);
            }

            var user = await _userManager.GetUserAsync(User);
            var result = _businessservice.GetProductsCart(user.Id); //hledam vsechny jeho produkty v kosiku

            foreach (var item in result)
            {
                /* Jednotlive produkty ukladam do modelu */
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
                sumPrice += model.amount; //pocitani celkove ceny
                viewModel.OrdProd.Add(model);
            }

            /* Zjistuji podle hodnoty z View co za shipping a payment metodu si vybral */
            var shipping = _orderService.GetPriceShipping(ShippingOption.Value); 
            var method = _orderService.GetPaymentMethod(Payment.Value);

            
            

            /* Pro zobrazeni graficky ve View co si vybral za metody shipping a payment */
            viewModel.PaymentMethodNumber = method.id_meth;
            viewModel.ShippingOptionNumber = shipping.id_ship;

            /* Ulozeni ceny, shipping a payment do modelu */
            viewModel.OverallPrice = sumPrice;
            viewModel.ShippingOption = shipping.name;
            viewModel.Payment = method.name;

            /* Pokud si vybral adresu */
            if (AddressChoose != null)
            {
                /* Ulozi se mu adresa podle jeho volby do modelu */
                var address = _orderService.FindSpecificAddress(AddressChoose.Value);
                var country = _orderService.GetState(address.country);
                viewModel.street = address.street;
                viewModel.house_number = address.house_number;
                viewModel.city = address.city;
                viewModel.post_code = address.post_code;
                viewModel.id_ad = AddressChoose.Value; // predavam do FinishOrder i id adresy
                viewModel.nameCountry = country.name;
                viewModel.codeCountry = country.code; // predavam do FinishOrder i code zeme
            }

            /* Pouze pro pripad kdy zadaval novou adresu */
            if (Country != null)
            {
                viewModel.codeCountry = Country.Value;
            }
            
            /* Kontroluji jestli vyplni vsechny udaje u adresy a pokud ne vrati mu to order a musi to vyplnit znovu */
            if (viewModel.street == null || viewModel.house_number == 0 || viewModel.codeCountry == 0 || viewModel.city == null || viewModel.post_code == 0)
            {
                viewModel = await OrderShow();
                ViewData["EmptyAddress"] = true;
                return View(viewModel);
            }
           
            return View("Summary", viewModel); 
        }


        /// <summary>
        /// Dokonceni objednavky ulozeni vsech udaju do databaze
        /// </summary>
        /// <param name="ShippingOption"></param>
        /// <param name="Payment"></param>
        /// <param name="AddressChoose"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> FinishOrder(int? ShippingOption, int? Payment, int? AddressChoose, OrderProduct model)
        {
            decimal sumPrice = 0; // pocitani celkove ceny

            var user = await _userManager.GetUserAsync(User);

            int addressId; // ukladam si id adresy

            /* Pokud si nevybral zadnou adresu a zadaval adresu novou rucne ulozi se mu adresa pomoci konstruktoru do databaze */
            if (model.id_ad == 0)
            {
                var address = new Address(model.street, model.city, model.house_number, model.post_code, model.codeCountry);
                _orderService.AddAddress(address);
                addressId = address.id_ad;
            }
            /* Pokud si adresu vybral ulozi se jen id adresy */
            else
            {
                addressId = model.id_ad;
            }

            /* Pridavam do databaze udaje o payment */
            var payment = new Payment(Payment.Value, 1, 0); // 1 je payment status, 0 je nastavena protoze jeste nevime celkovou cenu
            _orderService.AddPayment(payment);

            /* Vytvoreni NewOrder a prida do databaze */
            var date = DateTime.Today;
            var NewOrder = new NewOrder(user.Id, 1, addressId, ShippingOption.Value, payment.id_pay, date); // 1 je status objednavky
            _orderService.AddNewOrder(NewOrder);


            /* Ziskani produkty z Cart_pr podle usera a vlozeni do databaze Order_prod */
            var orderProd = _businessservice.GetConnectCart(user.Id);
            foreach (var item in orderProd)
            {
                var orPr = new Order_prod(NewOrder.id_ord, item.id_pr, item.amount, item.id_col, item.id_si);
                var pri = _catalogservice.GetProduct(item.id_pr);
                sumPrice += pri.price; // pocitani celkove ceny
                _businessservice.AddOrder_prod(orPr);

                /* Vymazani produktu z Cart_pr */
                _businessservice.DeleteCart_pr(item);
            }


            /* Vypocitani celkove ceny plus aktualizace tabulky payment o celkovou cenu */
            var ship = _orderService.GetPriceShipping(ShippingOption.Value);
            model.OverallPrice += ship.price;
            payment.price = model.OverallPrice;
            _orderService.UpdatePayment(payment);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public async Task<IActionResult> DeleteAll()
        {
            string tmp = Request.Headers["Referer"].ToString(); // zjistuji na jake strance jsem byl predtim
            var user = await _userManager.GetUserAsync(User);

            var orderProd = _businessservice.GetConnectCart(user.Id);
            foreach (var item in orderProd)
            {
                /* Vymazani produktu z Cart_pr */
                _businessservice.DeleteCart_pr(item);
            }

            return RedirectToLocal(tmp); //vratim se na predchozi stranku
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
