using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Model
{
    [Table("PlayerGames")]
    public partial class PlayerGame
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("Id")]
        public int Id { get; set; }

        [Column("PlayerId1")]
        public int PlayerId1 { get; set; }

        [Column("PlayerId2")]
        public int PlayerId2 { get; set; }

        [Column("Start_DT")]
        public DateTime StartDate { get; set; }

        [Column("End_DT")]
        public DateTime? EndDate { get; set; }

        [Column("QtyHits")]
        public int QtyHits { get; set; }

        [Column("QtySuccessfullHits")]
        public int QtySuccessfullHits { get; set; }

        public virtual ICollection<GameHit> GameHits { get; set; } = new HashSet<GameHit>();

        public virtual Player Player1 { get; set; }

        public virtual Player Player2 { get; set; }
    }
}
