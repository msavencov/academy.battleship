using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Academy.BattleShip.Service.Services;
using Academy.BattleShip.Web.Areas.Api.Models;

namespace Academy.BattleShip.Web.Areas.Api.Controllers
{
    [RoutePrefix("api/Stats")]
    public partial class StatsController
    {
        [HttpGet, Route("All"), ResponseType(typeof(ApiResponse<IEnumerable<GroupedStats>>))]
        public HttpResponseMessage All()
        {
            var gameStatses = _gameService.GameStats();
            var totalGames = gameStatses.Count(t => t.Completed);

            var groupedStats = from s in gameStatses
                               group s by s.Player1 into sg
                               let completed = sg.Where(t=>t.Completed)
                               let playerGames = completed.Count()
                               let totalHits = completed.Sum(t => t.TotalHits)
                               let cellHits = completed.Sum(t => t.Hits)
                               select new GroupedStats
                               {
                                   Player = sg.Key,
                                   Games = playerGames,
                                   TotalHits = totalHits,
                                   Hits = cellHits
                               };

            var result = groupedStats as GroupedStats[] ?? groupedStats.ToArray();
            foreach (var stat in result)
            {
                double all = totalGames > 0 ? (double)stat.Games / totalGames : 0;
                double player = stat.Hits > 0 ? (double)stat.TotalHits / stat.Hits : 0;
                stat.Rating = all * player;
            }
            return Request.CreateApiResponse(result);
        }
    }

    public class GroupedStats
    {
        public string Player { get; set; }
        public int Games { get; set; }
        public int TotalHits { get; set; }
        public int Hits { get; set; }
        public double Rating { get; set; }
    }

    public partial class StatsController : ApiController
    {
        IGameService _gameService;

        public StatsController(IGameService gameService)
        {
            _gameService = gameService;
        }
    }
}
