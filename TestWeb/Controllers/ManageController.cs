using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pernicek.Models.ManageViewModels;
using Alza.Core.Identity.Dal.Entities;
using Alza.Module.UserProfile.Business;
using Alza.Module.UserProfile.Dal.Context;
using Microsoft.EntityFrameworkCore;
using Module.Order.Business;
using Module.Business.Business;
using Catalog.Business;
using PernicekWeb.Models.ManageViewModels;
using System.Linq;
using Module.Order.Dal.Entities;
// using Uzivatel.Services;

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
            var model = new IndexViewModel_1
            {
                mobile = result.mobile,
                name = result.name,
                sec_name = result.surname,
                user = us.UserName,
                email = us.Email
            };

            var addTmp = _orderService.GetNewOrder(user.Id);
            if (addTmp != null)
            {
                var address = _orderService.FindSpecificAddress(addTmp.id_ad);
                model.Address = address.street;
                model.City = address.city;
                model.HouseNumber = address.house_number;
                model.PostalCode = address.post_code;
                var addressModel = new IndexViewModel_1
                {
                    Address = address.street,
                    City = address.city,
                    HouseNumber = address.house_number,
                    PostalCode = address.post_code
                };
                model.AddressCheck.Add(addressModel);
            }
            else
            {
                var address = _orderService.FindAddresByIdUser(user.Id);
                if (address != null)
                {
                    model.Address = address.street;
                    model.City = address.city;
                    model.HouseNumber = address.house_number;
                    model.PostalCode = address.post_code;
                    var addressModel = new IndexViewModel_1
                    {
                        Address = address.street,
                        City = address.city,
                        HouseNumber = address.house_number,
                        PostalCode = address.post_code
                    };
                    model.AddressCheck.Add(addressModel);
                }
                else
                {
                    ViewData["IsAddress"] = true;
                }
            }

           // var ideorder = _orderService.GetNewOrderList(user.Id);
            return View(model);
        }
       
        [HttpPost, ActionName("EditAddress")]
        public async Task<IActionResult> EditAddress(IndexViewModel_1 model, string returnull = null)
        {
            if (!ModelState.IsValid)
            {
                var user_1 = await GetCurrentUserAsync();
                var addTmp = _orderService.GetNewOrder(user_1.Id);
                if (addTmp != null)
                {
                    var address = _orderService.FindSpecificAddress(addTmp.id_ad);
                    address.street = model.Address;
                    address.city = model.City;
                    address.house_number = model.HouseNumber;
                    address.post_code = model.PostalCode;
                    _orderService.UpdateAddress(address);
                }
                else
                {
                    var address = new Address(model.Address, model.City, model.HouseNumber, model.PostalCode, user_1.Id);
                    _orderService.AddAddress(address);
                }
               


            }
                return RedirectToAction("Index");          
        }



        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(IndexViewModel_1 model, string returnull = null)
        {
            //  string tmp = Request.Headers["Referer"].ToString();
            if (!ModelState.IsValid)
            {
                var user_1 = await GetCurrentUserAsync();
                var user = await _context.User.SingleOrDefaultAsync(s => s.id_user == user_1.Id);

                user.name = model.name;
                user.surname = model.sec_name;
                user.mobile = model.mobile;


                _userProfileService.UpdateUserProfile(user);

                if (model.user != null) //kdyz se vyplni jen jedno tak aby se druhy neprepsal na null
                    user_1.UserName = model.user;
                if (model.email != null)
                {
                    var userCheck = new ApplicationUser {Email = model.email};
                    user_1.Email = model.email;
                    //Kontrola jestli uzivatel uz neexistuje
                    if (String.IsNullOrEmpty(userCheck.NormalizedEmail))
                        userCheck.NormalizedEmail = userCheck.Email;
                    var exist = await _userManager.GetUserIdAsync(userCheck);

                    if (exist != "")
                        return RedirectToAction("Index");

                }
                await _userManager.UpdateAsync(user_1);
                await _signInManager.RefreshSignInAsync(user_1);
            }
            return RedirectToAction("Index");
        }
        
      /*  [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string returnnull = null)
        {
           
            var user = await GetCurrentUserAsync();

            var userToUpdate = await _context.User.SingleOrDefaultAsync(s => s.id_user == user.Id);
            if (await TryUpdateModelAsync<User>(
                userToUpdate,
                "",
                s => s.name, s => s.surname, s => s.mobile))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex *//*)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(userToUpdate);
        }*/

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

        public async Task<IActionResult> PurchaseHistory(PurchaseHistory model, int id_ord)
        {
            var user = await GetCurrentUserAsync();
            var tmp = _orderService.GetNewOrder(user.Id);

            var shipping = _orderService.GetPriceShipping(tmp.id_sh);
            var payment = _orderService.GetPayment(tmp.id_pay);
            var address = _orderService.FindSpecificAddress(tmp.id_ad);
            var method = _orderService.GetPaymentMethod(payment.id_meth);


            /* Shipping */
            model.ShippingName = shipping.name;
            model.ShippingPrice = shipping.price;

            /* Payment */
            model.Price = payment.price;
            model.PaymentMethod = method.name;

            /* Address */
            model.Street = address.street;
            model.HouseNumber = address.house_number;
            model.City = address.city;
            model.PostalCode = address.post_code;

            var listOrderProduct = _businessservice.getOrderProduct(id_ord);
            foreach (var it in listOrderProduct)
            {
                var product = _catalogservice.GetProduct(it.id_pr);
                var image = _catalogservice.GetImage(it.id_pr);
                var firm = _catalogservice.GetFirm(product.id_fir);

                /* Product */
                var viewModel = new PurchaseHistory
                {
                    id_pr = it.id_pr,
                    nameProduct = product.name,
                    image = image.link,
                    Firm = firm.name,
                    quantity = it.amount
                };
                //     viewModel.colour = _catalogservice.GetColour().name;
                //     viewModel.size = it.Size.uk;
                model.PurchaseH.Add(viewModel);
            }
            return View(model);
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
