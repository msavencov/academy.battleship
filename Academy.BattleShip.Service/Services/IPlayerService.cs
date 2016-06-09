using System.Collections.Generic;
using System.Drawing;
using Academy.BattleShip.Service.Models;

namespace Academy.BattleShip.Service.Services
{
    public interface IPlayerService : IService
    {
        Player Find(string key);
        Player Register(string name);
        void UpdateMap(string key, List<Point> cells);
    }
}