using System.Collections.Generic;
using System.Runtime.InteropServices;
using Academy.BattleShip.Service.Models;

namespace Academy.BattleShip.Service
{
    public interface IGameService : IService
    {
        int NewGame(int playerId);
        HitResult Hit(int gameId, int x, int y);
    }

    public class Game
    {
        public int Player { get; set; }
        public int Type { get; set; }
    }
}
