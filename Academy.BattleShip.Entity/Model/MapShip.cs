using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Model
{
    [Table("MapShip")]
    public partial class MapShip
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("Id")]
        public int Id { get; set; }

        [Column("PlayerId")]
        public int PlayerId { get; set; }

        [Column("ShipType")]
        public byte ShipType { get; set; }

        public virtual Player Player { get; set; }

        public virtual ICollection<ShipCell> ShipCells { get; set; } = new HashSet<ShipCell>();
    }
}
