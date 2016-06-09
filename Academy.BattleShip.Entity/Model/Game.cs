using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.BattleShip.Entity.Model
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

        public virtual Player Player1 { get; set; }
        public virtual Player Player2 { get; set; }
    }
}
