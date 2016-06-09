using System.Data.SqlClient;
using System.Linq;

namespace Academy.BattleShip.Entity.Model
{
    public static class Extensions
    {
        public static HitResult Hit(this BattleShipEntities entities, string key, int playerId, short x, short y)
        {
            var parameters = new object[]
            {
                new SqlParameter("@secretKey", key),
                new SqlParameter("@IdPlayer2", playerId),
                new SqlParameter("@X_Pos", x),
                new SqlParameter("@Y_Pos", y),
            };

            return entities.Database.SqlQuery<HitResult>("[dbo].[spGameHit]", parameters).FirstOrDefault()
                   ?? new HitResult
                   {
                       IsError = true,
                       Message = "Empty result set from [dbo].[spGameHit]"
                   };
        }
    }
}