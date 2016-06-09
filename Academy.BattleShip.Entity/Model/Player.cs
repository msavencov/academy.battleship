using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Model
{
    public class Player
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }

        [Required, StringLength(6, MinimumLength = 6)]
        public string Key { get; set; }
        
        public virtual ICollection<ShipCell> Cells { get; set; } = new HashSet<ShipCell>();
        public virtual ICollection<Game> MyGames { get; set; } = new HashSet<Game>();
        public virtual ICollection<Game> OpponentGames { get; set; } = new HashSet<Game>();


    }
}