using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Model
{
    [Table("GameHits")]
    public partial class GameHit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("GameId")]
        public int GameId { get; set; }

        [Column("ShipCellId")]
        public int? ShipCellId { get; set; }

        [Column("X_Pos")]
        public byte X { get; set; }

        [Column("Y_Pos")]
        public byte Y { get; set; }

        public virtual PlayerGame PlayerGame { get; set; }

        public virtual ShipCell ShipCell { get; set; }
    }
}
