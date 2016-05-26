using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Academy.BattleShip.Entity;

namespace Academy.BattleShip.Service
{
    public class PlayerService : IPlayerService
    {
        private BattleShipEntities _entities;

        public PlayerService(DbContext entities)
        {
            _entities = (BattleShipEntities) entities;
        }

        public void Dispose()
        {
            _entities.Dispose();
        }

        public Player Find(string key)
        {
            return _entities.Players.FirstOrDefault(t => t.SecretKey == key);
        }

        public IEnumerable<Player> GetPlayers()
        {
            return _entities.Players.AsEnumerable();
        }
    }
}
