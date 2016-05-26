using Academy.BattleShip.Entity;

namespace Academy.BattleShip.Service
{
    public interface IGameService : IService
    {
        HitResult Hit(string key, int player, short x, short y);
        HitResult UploadMap(string key, bool[][] map);
    }
}
