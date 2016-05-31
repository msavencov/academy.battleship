using System.Collections.Generic;
using Academy.BattleShip.Entity;
using Academy.BattleShip.Entity.Model;

namespace Academy.BattleShip.Service
{
    public interface IPlayerService : IService
    {
        Player Find(string key);
        IEnumerable<Player> GetPlayers();
    }
}