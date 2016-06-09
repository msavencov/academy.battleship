using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Model
{
    [Table("Player")]
    public partial class Player
    {
        [Key, Column("IdPlayer"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50), Column("Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(10), Column("SecretKey")]
        public string SecretKey { get; set; }

        [Column("isRegistered")]
        public bool Registered { get; set; }

        public virtual ICollection<MapShip> MapShips { get; set; } = new HashSet<MapShip>();

        public virtual ICollection<PlayerGame> PlayerGames { get; set; } = new HashSet<PlayerGame>();

        public virtual ICollection<PlayerGame> PlayerGames1 { get; set; } = new HashSet<PlayerGame>();

        public virtual ICollection<PlayerShip> PlayerShips { get; set; } = new HashSet<PlayerShip>();
    }
}
