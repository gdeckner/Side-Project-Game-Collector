using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameDataBase;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Game_Collector_Web.Controllers
{
    public class LoginController : Controller
    {
        private DataBaseMediator mediator = new DataBaseMediator();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register(string userName = null,string password1 = null,string password2 = null)
        {
           if(userName == null)
            {
                return View();
            }
            else
            {
                string result = mediator.CreateLogin(userName, password1, password2);
                ViewData["result"] = result; 
                return View();
            }
            
        }
    }
}
