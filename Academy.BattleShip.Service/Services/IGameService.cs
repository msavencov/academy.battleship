using System;
using System.Collections.Generic;
using Academy.BattleShip.Service.Models;

namespace Academy.BattleShip.Service.Services
{
    public interface IGameService : IService
    {
        Guid NewGame(string playerKey);
        Guid CurrentGame(string playerKey);
        HitResult Hit(Guid gameId, int x, int y);
        GameStats GameStats(Guid gameId);
        List<GameStats> GameStats(string playerKey);
        List<GameStats> GameStats();
    }
}
