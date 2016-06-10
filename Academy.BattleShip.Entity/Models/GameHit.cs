using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Models
{
    public class GameHit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid GameId { get; set; }

        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
    }
}