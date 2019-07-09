using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameDataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Game_Collector_Web.Controllers
{
    public class LoggedInController : Controller
    {
        private bool loggedIn
        {
            get
            {
                string logged = HttpContext.Session.GetString("LoggedIn");
                if (logged == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }
        DataBaseMediator mediator = new DataBaseMediator();
        public IActionResult Index()
        {
            if (!loggedIn)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        public IActionResult LogOut()
        {
            if (!loggedIn) { return RedirectToAction("Login", "Home"); }
            else
            {
                HttpContext.Session.SetString("LoggedIn", "false");
                HttpContext.Session.SetString("UserName", null);
                return RedirectToAction("Index", "Home");
  }

        }
    }
}