using System;

namespace Academy.BattleShip.Web.Areas.Api.Controllers
{
    public class HitModel
    {
        public Guid GameId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class HitResult : Academy.BattleShip.Service.Models.HitResult
    {
    }
}