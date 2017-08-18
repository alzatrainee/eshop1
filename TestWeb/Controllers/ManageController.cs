using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pernicek.Models;
using Pernicek.Models.ManageViewModels;
using Alza.Core.Identity.Dal.Entities;
using Alza.Core.Module.Http;
using Pernicek.Abstraction;
using Alza.Core.Identity.Dal.Repository;
using Alza.Module.UserProfile.Business;
using Alza.Module.UserProfile.Dal.Repository.Abstraction;
using Alza.Module.UserProfile.Dal.Entities;
using Pernicek.Models.AccountViewModels;
using Alza.Module.UserProfile.Dal.Context;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUserRepository _iUserRepository;
        private readonly UserDbContext _context;


        public ManageController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          IOptions<IdentityCookieOptions> identityCookieOptions,
          //     IEmailSender emailSender,
          //     ISmsSender smsSender,
          ILoggerFactory loggerFactory,
          UserProfileService userProfileservice,
            IUserRepository iUserRepository,
            UserDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            //       _emailSender = emailSender;
            //        _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<ManageController>();
             _userProfileService = userProfileservice;
            _iUserRepository = iUserRepository;
            _context = context;
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
            
            var result = _userProfileService.GetUserProfile(user.Id);
           // var ahoj = result.name;
            var model = new IndexViewModel_1
            {
                mobile = result.mobile,
                name = result.name,
                sec_name = result.surname
            };
            return View(model);
        }
       
        [HttpPost, ActionName("EditAccount")]
        public async Task<IActionResult> EditAccount(IndexViewModel_1 model, string returnull = null)
        {
            if (!ModelState.IsValid)
            {
                var user_1 = await GetCurrentUserAsync();
                // var model = await _userManager.FindByIdAsync(user_1);
                if (model.user != null) //kdyz se vyplni jen jedno tak aby se druhy neprepsal na null
                    user_1.UserName = model.user;
                if (model.email != null)
                {
                    user_1.Email = model.email;
                    //Kontrola jestli uzivatel uz neexistuje
                    if (String.IsNullOrEmpty(user_1.Email))
                        user_1.NormalizedEmail = user_1.Email;
                    var exist = await _userManager.GetUserIdAsync(user_1);

                    if (exist != "")
                        return RedirectToAction("EditAccount");

                }
                await _userManager.UpdateAsync(user_1);
                await _signInManager.RefreshSignInAsync(user_1); //znovu prihlasi uzivatele a aktualizuje tak jeho mail
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
