using System;
using System.Web.Http;
using Academy.BattleShip.Service.Services;
using Microsoft.Practices.ObjectBuilder2;

namespace Academy.BattleShip.Web.Areas.Api.Controllers
{
    /// <summary>
    /// BattleShip game api interface
    /// </summary>
    public partial class GameController
    {
        
    }
    
    public partial class GameController : ApiController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        
    }

}
