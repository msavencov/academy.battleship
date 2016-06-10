using System.Data.Entity;
using Academy.BattleShip.Entity.Models;

//Enable-Migrations -ProjectName 'Academy.BattleShip.Entity' -ContextTypeName 'Academy.BattleShip.Entity.Model.BattleShipEntities' -ConnectionStringName 'BattleShipEntities' -EnableAutomaticMigrations
//Add-Migration Current -ProjectName 'Academy.BattleShip.Entity' -ConfigurationTypeName 'Academy.BattleShip.Entity.Migrations.Configuration' -ConnectionStringName 'BattleShipEntities'
//Update-Database -ProjectName 'Academy.BattleShip.Entity' -ConfigurationTypeName 'Academy.BattleShip.Entity.Migrations.Configuration' -ConnectionStringName 'BattleShipEntities' -Verbose

namespace Academy.BattleShip.Entity
{
    public class BattleShipEntities : DbContext
    {
        public BattleShipEntities() : base("BattleShipEntities") {}
        
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<ShipCell> Cells { get; set; }
        public virtual DbSet<KeyPool> Keys { get; set; }
        public virtual DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().HasRequired(t=>t.Player1).WithMany(t=>t.MyGames).WillCascadeOnDelete(false);
            modelBuilder.Entity<Game>().HasRequired(t=>t.Player2).WithMany(t=>t.OpponentGames).WillCascadeOnDelete(false);
            modelBuilder.Entity<ShipCell>().HasRequired(t=>t.Player).WithMany(t=>t.Cells).WillCascadeOnDelete(false);
            modelBuilder.Entity<GameHit>().HasRequired(t=>t.Game).WithMany(t=>t.GameHits).WillCascadeOnDelete(false);
        }
    }
}