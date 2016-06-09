using System;
using System.Data.Entity;
using Academy.BattleShip.Entity;
using Academy.BattleShip.Service.Models;

namespace Academy.BattleShip.Service.Services
{
    public partial class GameService : IGameService
    {
        public Guid NewGame(string playerKey)
        {
            throw new NotImplementedException();
        }

        public Guid CurrentGame(string playerKey)
        {
            throw new NotImplementedException();
        }

        public HitResult Hit(Guid gameId, int x, int y)
        {
            throw new NotImplementedException();
        }
        
    }


    public partial class GameService
    {
        private readonly BattleShipEntities _entities;

        public GameService(DbContext entities)
        {
            _entities = (BattleShipEntities)entities;
        }

        public void Dispose()
        {
            _entities.Dispose();
        }
    }
}