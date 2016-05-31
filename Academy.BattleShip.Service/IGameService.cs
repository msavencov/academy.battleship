using Academy.BattleShip.Entity;
using Academy.BattleShip.Entity.Model;

namespace Academy.BattleShip.Service
{
    public interface IGameService : IService
    {
        HitResult Hit(string key, int player, short x, short y);
        HitResult UploadMap(string key, bool[][] map);
    }
}
