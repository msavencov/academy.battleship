using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using Academy.BattleShip.Entity;
using Academy.BattleShip.Entity.Model;

namespace Academy.BattleShip.Service
{
    public class GameService : IGameService
    {
        readonly BattleShipEntities _entities;

        public GameService(DbContext entities)
        {
            _entities = (BattleShipEntities) entities;
        }
        
        public void Dispose()
        {
            _entities.Dispose();
        }

        public HitResult Hit(string key, int player, short x, short y)
        {
            return _entities.Hit(key, player, x, y);
        }

        public HitResult UploadMap(string key, string player, List<List<bool>> map)
        {

            throw new System.NotImplementedException();
        }

        public List<List<bool>> FindGame(string secretKey, out string playerName)
        {
            playerName = string.Empty;

            var player = _entities.Players.Include(t=>t.MapShips).FirstOrDefault(t => t.SecretKey == secretKey);
            if (player == null) return Extensions.Iterate((x, y) => false);

            var query = (from s in _entities.MapShips
                join c in _entities.ShipCells on s.Id equals c.ShipId
                where s.PlayerId == player.Id
                select s.ShipCells
                ).SelectMany(t => t);
            var list = query.ToList();

            var result = Extensions.Iterate((x, y) =>
            {
                return list.Any(t => t.X == x && t.Y == y);
            });

            return result;
        }
    }
}