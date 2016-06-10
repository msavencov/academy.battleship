using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Models
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; }

        [Index("IX_PlayerId1PlayerId2", Order = 1, IsUnique = true)]
        public int PlayerId1 { get; set; }

        [Index("IX_PlayerId1PlayerId2", Order = 2, IsUnique = true)]
        public int PlayerId2 { get; set; }

        public bool Completed { get; set; }

        [ForeignKey(nameof(PlayerId1))]
        public virtual Player Player1 { get; set; }
        [ForeignKey(nameof(PlayerId2))]
        public virtual Player Player2 { get; set; }
    }
}
