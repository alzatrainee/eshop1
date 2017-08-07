using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PernicekWeb.Controllers
{
    public class ItemsController : Controller
    {
        //
        // POST: /Items/Browse
        public IActionResult Browse()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
