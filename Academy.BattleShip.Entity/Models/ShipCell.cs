using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Models
{
    public class ShipCell
    {
        public ShipCell() {}

        public ShipCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PlayerId { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public virtual Player Player { get; set; }
    }
}