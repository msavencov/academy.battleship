using System;
using System.Web.Mvc;
using Academy.BattleShip.Service.Services;

namespace Academy.BattleShip.Web.Controllers
{
    public partial class AdminController
    {
        public ActionResult Stats()
        {
            var stat = _gameService.GameStats(Guid.Empty);
            var stats = _gameService.GameStats("100-10");
            return View();
        }
    }

    public partial class AdminController : Controller
    {
        private GameService _gameService;

        public AdminController(GameService gameService)
        {
            _gameService = gameService;
        }
    }
}