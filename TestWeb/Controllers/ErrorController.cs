using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PernicekWeb.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult EmptyString()
        {
            return View();
        }

        public IActionResult TooShort() {
            return View();
        }

        //pokrocila verze s argumenty
        //        public IActionResult NothingFound(string searchArg) {
        //            return View( "NothingFound", searchArg );
        //        }

        public IActionResult NothingFound()
        {
            return View();
        }
    }
}