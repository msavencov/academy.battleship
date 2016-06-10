using System;

namespace Academy.BattleShip.Service.Models
{
    public class GameStats
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public Guid GameId { get; set; }
        public int TotalHits { get; set; }
        public int Hits { get; set; }
        public bool Completed { get; set; }
    }
}