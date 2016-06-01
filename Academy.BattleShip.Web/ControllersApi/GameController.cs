using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Services.Description;
using Academy.BattleShip.Service;

namespace Academy.BattleShip.Web.ControllersApi
{
    public class GameController : ApiController
    {
        IPlayerService _playerService;

        public GameController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public string Test()
        {
            return "dsadasd";
        }
    }
    
}
