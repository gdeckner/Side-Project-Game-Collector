    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Game_Collector_Web.Controllers
{
    public class LoggedInController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}