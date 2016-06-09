using System.Data.Entity;
using Academy.BattleShip.Entity.Model;

//Enable-Migrations -ProjectName 'Academy.BattleShip.Entity' -ContextTypeName 'Academy.BattleShip.Entity.Model.BattleShipEntities' -ConnectionStringName 'BattleShipEntities' -EnableAutomaticMigrations
//Add-Migration Initial -ProjectName 'Academy.BattleShip.Entity' -ConfigurationTypeName 'Academy.BattleShip.Entity.Migrations.Configuration' -ConnectionStringName 'BattleShipEntities'
//Update-Database -ProjectName 'Academy.BattleShip.Entity' -ConfigurationTypeName 'Academy.BattleShip.Entity.Migrations.Configuration' -ConnectionStringName 'BattleShipEntities' -Verbose

namespace Academy.BattleShip.Entity
{
    public class BattleShipEntities : DbContext
    {
        public BattleShipEntities() : base("BattleShipEntities") {}
        
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<ShipCell> Cells { get; set; }
        public virtual DbSet<KeyPool> Keys { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}