using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Academy.BattleShip.Service;

namespace Academy.BattleShip.Web.Controllers
{
    public class HomeController : Controller
    {
        IPlayerService _playerService;

        public HomeController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}