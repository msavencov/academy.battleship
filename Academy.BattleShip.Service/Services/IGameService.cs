using System;
using Academy.BattleShip.Service.Models;

namespace Academy.BattleShip.Service.Services
{
    public interface IGameService : IService
    {
        Guid NewGame(string playerKey);
        Guid CurrentGame(string playerKey);
        HitResult Hit(Guid gameId, int x, int y);
    }
    
}
