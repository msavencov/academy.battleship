using System.Data.Entity;
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

        public HitResult UploadMap(string key, bool[][] map)
        {
            throw new System.NotImplementedException();
        }
    }
}