using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Game_Collector_Web.Models;
using GameDataBase;
using Microsoft.AspNetCore.Http;

namespace Game_Collector_Web.Controllers
{
    public class HomeController : Controller
    {
        private bool loggedIn { get
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
            
            return View();
        }
        public IActionResult Register(string userName = null, string password1 = null, string password2 = null)
        {
            if (userName != null)
            {
                string result = mediator.CreateLogin(userName, password1, password2);
                ViewData["result"] = result;
                

            }
            return View();
        }
        public IActionResult Login(string userName = null, string password = "")
        {

            if (loggedIn)
            {
                return RedirectToAction("Index", "Index");
            }
            else if(userName == null)
            {
                
            }
            else if (mediator.Login(userName, password))
            {

                ViewData["result"] = "Login was successful";
                HttpContext.Session.SetString("LoggedIn", "True");

            }
            else
            {
                ViewData["result"] = "User name or password was incorrect";
                
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
