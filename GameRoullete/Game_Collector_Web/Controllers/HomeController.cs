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
    //TODO Switch to authentication, page displayed is based on whether user is logged in or not. not allowed to access login register if logged in, will reroute
    public class HomeController : Controller
    {
        private bool LoggedIn { get
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
        private string UserName
        {
            get
            {
                string name = HttpContext.Session.GetString("UserName");
                return name;
            }


        }
        DataBaseMediator mediator = new DataBaseMediator();
        public IActionResult Index()
        {
            if (LoggedIn)
            {
                return RedirectToAction("Index", "LoggedIn");
            }
            return View();
        }
        public IActionResult Register(string userName = null, string password1 = null, string password2 = null)
        {
            if(LoggedIn)
            {
                return RedirectToAction("Index", "LoggedIn");
            }
            else if (userName != null)
            {
                string result = mediator.CreateLogin(userName, password1, password2);
                if(result.Contains('!'))
                {
                    HttpContext.Session.SetString("LoggedIn", "True");
                    HttpContext.Session.SetString("UserName", userName);
                    return RedirectToAction("Index", "LoggedIn");
                }
                ViewData["result"] = result;
                

            }
            return View();
        }
        public IActionResult Login(string userName = null, string password = "")
        {

            if (LoggedIn)
            {
                return RedirectToAction("Index", "LoggedIn");
            }
            else if(userName == null)
            {
                //Do nothing
            }
            else if (mediator.Login(userName, password))
            {

                ViewData["result"] = "Login was successful";
                HttpContext.Session.SetString("LoggedIn", "True");
                HttpContext.Session.SetString("UserName", userName);
                return RedirectToAction("Index", "LoggedIn");

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
