using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.BattleShip.Entity.Model
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Column("Login")]
        public string Login { get; set; }

        [Required]
        [StringLength(200)]
        [Column("Password")]
        public string Password { get; set; }
    }
}
