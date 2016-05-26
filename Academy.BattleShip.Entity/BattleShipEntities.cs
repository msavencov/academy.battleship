using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Academy.BattleShip.Entity
{
    partial class BattleShipEntities
    {
        public BattleShipEntities(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public HitResult Hit(string key, int playerId, short x, short y)
        {
            var parameters = new object[]
            {
                new SqlParameter("@secretKey", key), 
                new SqlParameter("@IdPlayer2", playerId), 
                new SqlParameter("@X_Pos", x), 
                new SqlParameter("@Y_Pos", y), 
            };

            return Database.SqlQuery<HitResult>("[dbo].[spGameHit]", parameters).FirstOrDefault()
                   ?? new HitResult
                   {
                       IsError = true,
                       Message = "SP Internal Error"
                   };
        }
    }

    public class HitResult
    {
        [Column("IsError")]
        public bool IsError { get; set; }
        [Column("Mesasge")]
        public string Message { get; set; }
    }
}