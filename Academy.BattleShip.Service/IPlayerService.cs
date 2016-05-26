using System.Collections.Generic;
using Academy.BattleShip.Entity;

namespace Academy.BattleShip.Service
{
    public interface IPlayerService : IService
    {
        Player Find(string key);
        IEnumerable<Player> GetPlayers();
    }
}