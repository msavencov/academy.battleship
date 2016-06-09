using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Model
{
    public class HitResult
    {
        [Column("IsError")]
        public bool IsError { get; set; }
        [Column("Mesasge")]
        public string Message { get; set; }
    }
}
