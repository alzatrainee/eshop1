using Alza.Core.Identity.Dal.Entities;
//using Alza.Core.Module.Http;
using Pernicek.Models;
using Pernicek.Models.AccountViewModels;
using Alza.Module.UserProfile.Dal.Entities;
using Alza.Module.UserProfile.Dal.Repository.Abstraction;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alza.Core.Module.Http;
using Pernicek.Abstraction;
using Alza.Module.UserProfile.Business;
using Module.Order.Dal.Entities;
using Module.Order.Business;

namespace Pernicek.Controllers
{
    public class AccountController : Controller
    {
        private IHostingEnvironment _env;
        private ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserProfileService _userProfileService;
        private readonly OrderService _orderService;
        public string tmp;


        public AccountController(
            IHostingEnvironment env,
            ILogger<AccountController> logger,
            IStringLocalizer<AccountController> localizerizer,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            UserProfileService userProfileservice,
            OrderService orderService)
        {
            _env = env;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _userProfileService = userProfileservice;
            _orderService = orderService;
        }
               
        //
        // POST: /Account/Login
        [HttpPost]
        [Route("/Account/Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
           
              string tmp = Request.Headers["Referer"].ToString();
            
            try
            {
                //Nesmi byt prihlasen
                if (_signInManager.IsSignedIn(User))
                    return ErrorActionResult("Uživatel již je přihlášen");


                //ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {

                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                   if ( (result.Succeeded && tmp.Equals("http://localhost:23603/Account/Login")) || tmp.Equals("http://localhost:23603/Account/Register"))
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    } else
                    if (result.Succeeded)
                    {
                        return RedirectToLocal(tmp);
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning(2, "someString");
                        ModelState.AddModelError("UserName", "someString");
                        return View("Lockout");
                    }
                    else
                    { 
                            _logger.LogWarning(2, "someString");
                            ModelState.AddModelError(string.Empty, "Invalid Password or Mail");
                        }
                
                    return View(model);
                    
                }

                // If we got this far, something failed, redisplay form
                return View(model);


            }
            catch (Exception e)
            {
                return ExceptionActionResult(e);
            }
        }

     
       

        //
        // GET: /Account/Register
        [HttpGet]
        [Route("/Account/Register")]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
          //  ViewData["CheckIfUserExist"] = true;
            try
            {
                //Nesmi byt prihlasen
                if (_signInManager.IsSignedIn(User))
                    _signInManager.SignOutAsync();

                tmp = Request.Headers["Referer"].ToString();

                ViewData["ReturnUrl"] = tmp;
               

                return View("Register");
            }
            catch (Exception e)
            {
                return ExceptionActionResult(e);
            }
        }


        public IActionResult Transfer(string returnUrl)
        {
           // var bum = ViewData["ReturnUrl"];
            return RedirectToLocal(returnUrl);

        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [Route("/Account/Register")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            model.CheckIfUserExist = 0;
           // model.Success = false;
            try
            {
                //Nesmi byt prihlasen
                if (_signInManager.IsSignedIn(User))
                {
                    //await _signInManager.SignOutAsync();
                    return ErrorActionResult("Uživatel již je přihlášen");
                }

                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                    //Kontrola jestli uzivatel uz neexistuje
                    if (String.IsNullOrEmpty(user.NormalizedEmail))
                        user.NormalizedEmail = user.Email;
                    var exist = await _userManager.GetUserIdAsync(user);

                    if (exist != "")
                    {
                        ViewData["CheckIfUserExist"] = false;
                        return View("Register", model);
                    }

                    //osetreni username

                    string usernameTemp = model.Email.Split('@')[0];

                    user.UserName = usernameTemp;


                    //Create AspNet Identity User
                    IdentityResult res = await _userManager.CreateAsync(user, model.Password);
                    IdentityResult res2 = null;
                    if (res.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: true);
                        //zjisteni ulozeneho Id uzivatele
                        var resId = await _userManager.GetUserIdAsync(user);
                        user.Id = Int32.Parse(resId);

                        //prirazeni uzivatele do Role
                        res2 = await _userManager.AddToRoleAsync(user, "User");

                        if (res2.Succeeded)
                        {
                            var user_1 = new User(user.Id, model.name, model.sec_name, model.mobile);
                           
                            var cart = new Module.Order.Dal.Entities.Cart(user.Id);                            



                            _userProfileService.AddUserProfile(user_1);
                            _orderService.AddCart(cart);
                            //  model.Success = true;
                            model.UrlAddress = returnUrl;
                            model.CheckRegister = 2;
                            ViewData["Success"] = true;
                            return View(model);

                        }
                        
                    }
                    else
                    {
                        return RedirectToAction("CHYBA");
                    }



                    //??
                    return RedirectToAction("Forbidden");
                }



                // If we got this far, something failed, redisplay form
                return View(model);

            }
            catch (Exception e)
            {
                return ExceptionActionResult(e);
            }
        }

       //
       // POST: /Account/Logout
       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        //
        // GET: /Account/Forbidden
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Forbidden()
        {
            try
            {
                //OSTATNI
                //var user = _userManager.GetUserAsync(User).Result;
                //if (user == null)
                //    return ErrorActionResult("user == null");


                //_logger.LogError("Forbidden - userId = " + user.Id);
                //_logger.LogError(user.EmailConfirmed.ToString());
                //_logger.LogError(user.NormalizedUserName.ToString());
                //_logger.LogError(user.LockoutEnd.ToString());


                //var userRole = _userManager.GetRolesAsync(user);

                //foreach (var item in userRole.Result)
                //{
                //    _logger.LogError(item);

                //}


                return View();
            }
            catch (Exception e)
            {
                return ExceptionActionResult(e);
            }
        }



        
        //
        // POST: /Account/LogOff
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            catch (Exception e)
            {
                return ExceptionActionResult(e);
            }
        }


        #region Helpers




        public void ErrorToModel(AlzaAdminDTO dto, LegoViewModel model)
        {
            model.ErrorNo = dto.errorNo;
            foreach (var item in dto.errors)
            {
                model.Errors.Add(item);
            }
        }
        public AlzaAdminDTO InvalidModel()
        {
            AlzaAdminDTO invalidresult = AlzaAdminDTO.False;
            foreach (var item in ModelState.ToList())
            {

                foreach (var item2 in item.Value.Errors)
                {
                    invalidresult.errors.Add(item.Key + " - " + item2.ErrorMessage);
                }

            }
            return invalidresult;
        }














        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AlzaAdminDTO InvalidIdentityResultDTO(IdentityResult result)
        {
            Guid errNo = Guid.NewGuid();
            StringBuilder res = new StringBuilder();

            foreach (var error in result.Errors)
            {
                res.AppendLine(error.Description);
            }

            _logger.LogError(errNo + " - " + res.ToString());
            return AlzaAdminDTO.Error(errNo, res.ToString());

        }

        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public RedirectToActionResult InvalidIdentityResultActionResult(IdentityResult result)
        {
            Guid errNo = Guid.NewGuid();
            StringBuilder res = new StringBuilder();

            foreach (var error in result.Errors)
            {
                res.AppendLine(error.Description);
            }

            _logger.LogError(errNo + " - " + res.ToString());


            LegoViewModel model = new LegoViewModel();
            model.ErrorNo = errNo;
            model.Errors.Add(res.ToString());

            return RedirectToAction("someString", "someString", model);
        }



        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AlzaAdminDTO ErrorDTO(string text)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogError(errNo + " - " + text);
            return AlzaAdminDTO.Error(errNo, "someString");
        }

        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IActionResult ErrorActionResult(string text)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogError(errNo + " - " + text);

            LegoViewModel model = new LegoViewModel();
            model.ErrorNo = errNo;
            model.Errors.Add(text);



            /*************************************************************************/
            //BUG Notification

            //var userId = _userManager.GetUserId(User);
            //if (String.IsNullOrEmpty(userId))
            //    userId = "0";

            //var bug = new BugNotification
            //{
            //    UserProfileId = Int32.Parse(userId),
            //    Severity = "Error",
            //    ErrorNo = errNo,
            //    CreatedDate = DateTime.Now,
            //    Note = text
            //};
            //_mediator.PublishAsync(bug);

            /*************************************************************************/



            //FINALni varianta Custom ActionResult
            return new AlzaActionResult("someString", model);

        }

        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public RedirectToActionResult ErrorActionResult(AlzaAdminDTO err)
        {
            _logger.LogError(err.errorNo + " - " + err.errorText);

            LegoViewModel model = new LegoViewModel();
            model.ErrorNo = err.errorNo;
            model.Errors.Add(err.errorText);

            return RedirectToAction("someString", "someString", model);
        }



        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AlzaAdminDTO ExceptionDTO(Exception e)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogError(errNo + " - " + e.Message + Environment.NewLine + e.StackTrace);
            return AlzaAdminDTO.Error(errNo, e.Message + Environment.NewLine + e.StackTrace);
        }

        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IActionResult ExceptionActionResult(Exception e)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogError(errNo + " - " + e.Message + Environment.NewLine + e.StackTrace);


            LegoViewModel model = new LegoViewModel();
            model.ErrorNo = errNo;
            model.Errors.Add(e.Message + Environment.NewLine + e.StackTrace);



            /*************************************************************************/
            //BUG Notification

            //var userId = _userManager.GetUserId(User);
            //if (String.IsNullOrEmpty(userId))
            //    userId = "0";

            //var bug = new BugNotification
            //{
            //    UserProfileId = Int32.Parse(userId),
            //    Severity = "Critical",
            //    ErrorNo = errNo,
            //    CreatedDate = DateTime.Now,
            //    Note = e.Message + Environment.NewLine + e.StackTrace
            //};
            //_mediator.PublishAsync(bug);

            /*************************************************************************/



            return new AlzaActionResult("someString", model);

        }



        #endregion









        //Odstranit
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

        private IActionResult RedirectToLocal_1(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/");
            }
        }

    }
}
