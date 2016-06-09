using System.ComponentModel.DataAnnotations;

namespace Academy.BattleShip.Entity.Models
{
    public class KeyPool
    {
        [Key, MaxLength(7)]
        public string Key { get; set; }
    }
}