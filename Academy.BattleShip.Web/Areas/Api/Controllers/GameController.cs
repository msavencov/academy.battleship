using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Academy.BattleShip.Service.Services;
using Academy.BattleShip.Web.Areas.Api.Models;
using Microsoft.Practices.ObjectBuilder2;

namespace Academy.BattleShip.Web.Areas.Api.Controllers
{
    /// <summary>
    /// BattleShip game api interface
    /// </summary>
    [RoutePrefix("api/Game")]
    public partial class GameController
    {
        [HttpGet, Route("NewGame"), ResponseType(typeof(ApiResponse<Guid>))]
        public HttpResponseMessage NewGame(string playerKey)
        {
            var gameId = _gameService.NewGame(playerKey);
            return Request.CreateApiResponse(gameId);
        }

        [HttpGet, Route("CurrentGame"), ResponseType(typeof(ApiResponse<Guid>))]
        public HttpResponseMessage CurrentGame(string playerKey)
        {
            var gameId = _gameService.CurrentGame(playerKey);
            return Request.CreateApiResponse(gameId);
        }

        [HttpPost, Route("GameHit"), ResponseType(typeof(ApiResponse<Service.Models.HitResult>))]
        public HttpResponseMessage Hit([FromBody] HitModel model)
        {
            Service.Models.HitResult result = _gameService.Hit(model.GameId, model.X, model.Y);
            return Request.CreateApiResponse(result);
        }
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
