using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GameDataBase;
using GameDataBase.Models;

namespace Game_Collector_Web.Controllers
{
    public class GamesController : Controller
    {
        private string UserName
        {
            get
            {
                string name = HttpContext.Session.GetString("UserName");
                return name;
            }


        }
        private bool LoggedIn
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
            return View();
        }

        public IActionResult ListMyGames()
        {

            if (!LoggedIn) { return RedirectToAction("Login", "Home"); }
            else
            {
                IList<DisplayResults> userGameResults = new List<DisplayResults>();
                userGameResults = mediator.GetUserList(UserName);

                return View(userGameResults);
            }
    
        }
        public IActionResult SearchGames()
        {
            if (!LoggedIn) { return RedirectToAction("Login", "Home"); }
            return View();
        }
        public IActionResult AddGames()
        {
            if (!LoggedIn) { return RedirectToAction("Login", "Home"); }
            return View();
        }
    }
}