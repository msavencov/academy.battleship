using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Model
{
    [Table("PlayerShips")]
    public partial class PlayerShip
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("Id")]
        public int Id { get; set; }

        [Column("PlayerId")]
        public int PlayerId { get; set; }

        [Column("ShipTypeId")]
        public byte ShipTypeId { get; set; }

        public virtual Player Player { get; set; }
    }
}
