using Alza.Core.Identity.Dal.Entities;
using Alza.Module.UserProfile.Business;
using Alza.Module.UserProfile.Dal.Context;
using Catalog.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Module.Business.Business;
using Module.Order.Business;
using Module.Order.Dal.Entities;
using Pernicek.Models.ManageViewModels;
using PernicekWeb.Models.ManageViewModels;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace Pernicek.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {   
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string _externalCookieScheme;
        //  private readonly IEmailSender _emailSender;
        //private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly UserProfileService _userProfileService;
        private readonly UserDbContext _context;
        private readonly OrderService _orderService;
        private readonly BusinessService _businessservice;
        private readonly CatalogService _catalogservice;



        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IOptions<IdentityCookieOptions> identityCookieOptions,
          //     IEmailSender emailSender,
          //     ISmsSender smsSender,
          ILoggerFactory loggerFactory,
          UserProfileService userProfileservice,
            UserDbContext context,
            OrderService orderService,
            BusinessService businessservice,
            CatalogService catalogservice)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            //       _emailSender = emailSender;
            //        _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
             _userProfileService = userProfileservice;
            _context = context;
            _orderService = orderService;
            _businessservice = businessservice;
            _catalogservice = catalogservice;
        }

        //
        // GET: /Manage/Index
        [HttpGet]
        public async Task<IActionResult> Index(ManageMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            var us = await _userManager.GetUserAsync(User);

            var result = _userProfileService.GetUserProfile(user.Id);

            /* z tabulky AspNetUser si bere udaje a prenasi je do modelu */

            var model = new IndexViewModel_1
            {
                mobile = result.mobile,
                name = result.name,
                sec_name = result.surname,
                user = us.UserName,
                email = us.Email
            };

            /* uklada do model.Countries vsechny countries co mame v databazi abychom si je potom mohli v editu zobrazit */
            var countries = _orderService.GetAllCountries();
            model.Countries = countries;

            ///////////////////////////////////////////////////////////
            //////////            WishList             ////////////////

            int id_us = 0; // defaultni id Usera...potrebujeme jen z duvodu pouziti wishListu
            int.TryParse(_userManager.GetUserId(User), out id_us);
            var wishProducts = _businessservice.GetAllWishProductsFromThisUser(id_us); // najdem v BusinessService vsechny produkty, ktere uzivatel si kdysi rozhodl pridat do WishListu !!!

            model.WishList = wishProducts;
            //////////                                ////////////////
            //////////////////////////////////////////////////////////


            /****************************************************
             *          Adresa + Purchase History               *
             ****************************************************/

            var addTmp = _orderService.GetNewOrder(user.Id); //hledam jestli uz uzivatel provedl objednavku a pripadne se mi vyhleda posledni provedena objednavka
            /* pokud ji provedl, objekt te objednavky je ulozen v addTmp */
            if (addTmp != null)
            {
                var address = _orderService.FindSpecificAddress(addTmp.id_ad); // podle objednavky se najde adresa
                var country = _orderService.GetState(address.country); // podle adresy se najde zeme

                /* jednotlive udaje adresy se ulozi do modelu */
                model.Address = address.street;
                model.City = address.city;
                model.HouseNumber = address.house_number;
                model.PostalCode = address.post_code;
                model.Country = country.name;
                model.CountryCode = country.code;

                /* pro Purchase History */
                var ListNewOrder = _orderService.GetNewOrderList(user.Id); // hledam vsechny objednavky, ktere provedl

                int tmpCount = 1; // z duvodu iterace ve View Index

                /* prochazim vsechny objednavky a ukladam si zakladni udaje o objednavce do modelu */
                foreach (var item in ListNewOrder)
                {
                    var price = _orderService.GetPayment(item.id_pay); // zjistuji celkovou cenu objednvaky
                    var OrderDetails = new IndexViewModel_1
                    {
                        date = item.date.ToString("d"),
                        id_ord = item.id_ord,
                        Price = price.price,
                        tmpCount = tmpCount
                    };
                    model.OrderDetails.Add(OrderDetails); // prirazuji do model.OrderDetails protoze potrebuji pozdeji iteravat ve View
                    tmpCount++;
                }
            }
            /* Pokud zadnou objednavku dosud neprovedl */
            else
            {
                var address = _orderService.FindAddresByIdUser(user.Id); //hledam adresu podle user.Id, protoze si adresu mohl vyplnit sam jeste pred objednavkou

                /* Pokud ji uz vyplnil ulozi se adresa do modelu */
                if (address != null)
                {
                    var country = _orderService.GetState(address.country);
                    model.Address = address.street;
                    model.City = address.city;
                    model.HouseNumber = address.house_number;
                    model.PostalCode = address.post_code;
                    model.Country = country.name;
                    model.CountryCode = country.code;
                }
                /* Pokud jeste nevyplnil adresu nebo neprovedl objednavku */
                else
                {
                    ViewData["IsAddress"] = true; // diky tomu se ve View zmeni button z Edit na Add
                }
            }

            return View(model);
        }

        [HttpPost, ActionName("EditAddress")]
        public async Task<IActionResult> EditAddress(IndexViewModel_1 model, int? Country)
        {
            try
            {
                if (!model.HouseNumber.Any(char.IsDigit)) return RedirectToAction("Index"); //kontroluji aby v House Number bylo alespon jedno cislo
                if (!model.PostalCode.Any(char.IsDigit)) return RedirectToAction("Index"); // kontrosluji a v Postal Code bylo alespon jedno cislo
            }
            catch (ArgumentNullException e)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var user_1 = await GetCurrentUserAsync();
                var order = _orderService.GetNewOrder(user_1.Id); //hledam jestli uz uzivatel provedl objednavku a pripadne se mi vyhleda posledni provedena objednavka

                /* pokud ji provedl, objekt te objednavky je ulozen v addTmp */
                if (order != null)
                {
                    var address = _orderService.FindSpecificAddress(order.id_ad); // najdu konkretni adresu k objednavce
                    /* do Address se mi ulozi ty zmeny ktere provedl ve View */
                    address.street = model.Address;
                    address.city = model.City;
                    address.house_number = model.HouseNumber;
                    address.post_code = model.PostalCode;
                    if (Country != null)
                    {
                        address.country = Country.Value; // je to cislo dane zeme, ktere jsme ziskali z View
                    }
                    _orderService.UpdateAddress(address); // aktualizuje se nase databaze
                }
                /* pokud nikdy objednavku neprovedl */
                else
                {
                    try
                    {
                        var address = new Address(model.Address, model.City, model.HouseNumber, model.PostalCode, Country.Value, user_1.Id); // pres konstruktor se poslou udaje z modelu a user Id, protoze si pridava adresu sam
                        _orderService.AddAddress(address); // adresa se ulozi do databaze
                    } catch (Exception e)
                    {
                        return RedirectToAction("Index");
                    }
                }

            }
            return RedirectToAction("Index"); // po provedeni se zobrazi profilova stranka  
        }



        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(IndexViewModel_1 model, string returnull = null)
        {

            //  string tmp = Request.Headers["Referer"].ToString();
            if (ModelState.IsValid)
            {
                var user_1 = await GetCurrentUserAsync();
                var user = await _context.User.SingleOrDefaultAsync(s => s.id_user == user_1.Id); // podle Id z aspnetUser hledam usera v nasi tabulce

                /* hodnoty z modelu se pridaji do tabulky user */
                user.name = model.name;
                user.surname = model.sec_name;
                user.mobile = model.mobile;
                _userProfileService.UpdateUserProfile(user); //tabulka se aktualizuje

                if (model.user != null && model.user != user_1.UserName) // kontroluji jestli se v usera provedla zmena oproti puvodnimu username
                    user_1.UserName = model.user; // hodnota z modelu se preda tabulky ASPNETUSER

                if (model.email != null && !(model.email.Equals(user_1.Email))) // kontroluji jestli je zmena v mailu oproti puvodnimu
                {
                    var userCheck = new ApplicationUser { Email = model.email };
                    user_1.Email = model.email; // hodnota z modelu se preda tabulky ASPNETUSER

                    /* Kontrola jestli uzivatel uz neexistuje podle mailu */
                    if (String.IsNullOrEmpty(userCheck.NormalizedEmail))
                        userCheck.NormalizedEmail = userCheck.Email;
                    var exist = await _userManager.GetUserIdAsync(userCheck); //hledam jestli uz takovy uzivatel existuje a pokud ano do exist se priradi jeho Id

                    /* Pokud existuje vrati se na profilovou stranku beze zmeny */
                    if (exist != "")
                        return RedirectToAction("Index");

                }
                await _userManager.UpdateAsync(user_1); //aktualizuje se tabulka
                await _signInManager.RefreshSignInAsync(user_1); // znovu prihlasi uzivatele
            }
            return RedirectToAction("Index");
        }


        //
        // GET: /Manage/ChangePassword
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }




        //
        // GET: /Manage/SetPassword
        [HttpGet]
        public IActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }

        public async Task<IActionResult> PurchaseHistory(PurchaseHistory model, int id_ord) // id_ord ziskam z View
        {
            var user = await GetCurrentUserAsync();
            var specificOrder = _orderService.GetSpecificOrder(id_ord); //hledam objednavku podle jejiho cisla
            model.id_ord = id_ord;

            /* Ziskavam jednotlive udaje z databaze */
            var shipping = _orderService.GetPriceShipping(specificOrder.id_sh);
            var payment = _orderService.GetPayment(specificOrder.id_pay);
            var address = _orderService.FindSpecificAddress(specificOrder.id_ad);
            var method = _orderService.GetPaymentMethod(payment.id_meth);
            var country = _orderService.GetState(address.country);

            /* Shipping */
            model.ShippingName = shipping.name;
            model.ShippingPrice = shipping.price;
            model.ShippingOption = shipping.id_ship;

            /* Payment */
            model.Price = payment.price;
            model.PaymentMethod = method.name;
            model.PaymentOption = method.id_meth;
            model.TotalItemPrice = payment.price - shipping.price;

            /* Address */
            model.Street = address.street;
            model.HouseNumber = address.house_number;
            model.City = address.city;
            model.PostalCode = address.post_code;
            model.Country = country.name;
            model.date = specificOrder.date.ToString("d");

            var listOrderProduct = _businessservice.getOrderProduct(id_ord); // List vsech produktu k dane objednavce
            foreach (var it in listOrderProduct)
            {
                /* Zjistovani informaci z databaze a ukladani do modelu */
                var product = _catalogservice.GetProduct(it.id_pr);
                var image = _catalogservice.GetImage(it.id_pr);
                var firm = _catalogservice.GetFirm(product.id_fir);
                var size = _catalogservice.GetSize(it.id_si);
                var colour = _catalogservice.GetColour(it.id_col);

                /* Product */
                var viewModel = new PurchaseHistory
                {
                    id_pr = it.id_pr,
                    nameProduct = product.name,
                    image = image.link,
                    Firm = firm.name,
                    quantity = it.amount,
                    colour = colour.name,
                    size = size.uk,
                    Price = product.price,
                    amount = product.price * it.amount
                };
                model.PurchaseH.Add(viewModel);
            }
            return View(model);
        }


        // Converts to pdf from html using node js
        public async Task<IActionResult> Invoice([FromServices] INodeServices nodeServices)
        {
            HttpClient hc = new HttpClient();
            var htmlContent = await hc.GetStringAsync($"http://{Request.Host}/Error/NothingFound");

            var result = await nodeServices.InvokeAsync<byte[]>("./pdf", htmlContent);

            HttpContext.Response.ContentType = "application/pdf";

            HttpContext.Response.Headers.Add("x-filename", "invoice.pdf");
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "x-filename");
            HttpContext.Response.Body.Write(result, 0, result.Length);
            return new ContentResult();
        }




        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
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
        #endregion
    }

}