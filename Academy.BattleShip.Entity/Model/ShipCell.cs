using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Model
{
    [Table("ShipCell")]
    public partial class ShipCell
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("Id")]
        public int Id { get; set; }

        [Column("ShipId")]
        public int ShipId { get; set; }

        [Column("X_Pos")]
        public byte? X { get; set; }

        [Column("Y_Pos")]
        public byte? Y { get; set; }

        public virtual ICollection<GameHit> GameHits { get; set; } = new HashSet<GameHit>();

        public virtual MapShip MapShip { get; set; }
    }
}
