using System.Data.Entity;


//Enable-Migrations -ProjectName 'Academy.BattleShip.Entity' -ContextTypeName 'Academy.BattleShip.Entity.Model.BattleShipEntities' -ConnectionStringName 'BattleShipEntities' -EnableAutomaticMigrations
//Add-Migration Initial -ProjectName 'Academy.BattleShip.Entity' -ConfigurationTypeName 'Academy.BattleShip.Entity.Migrations.Configuration' -ConnectionStringName 'BattleShipEntities'
//Update-Database -ProjectName 'Academy.BattleShip.Entity' -ConfigurationTypeName 'Academy.BattleShip.Entity.Migrations.Configuration' -ConnectionStringName 'BattleShipEntities' -Verbose

namespace Academy.BattleShip.Entity.Model
{
    public class BattleShipEntities : DbContext
    {
        public BattleShipEntities() : base("BattleShipEntities") {}
        
        public virtual DbSet<GameHit> GameHits { get; set; }
        public virtual DbSet<MapShip> MapShips { get; set; }
        public virtual DbSet<PlayerGame> PlayerGames { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerShip> PlayerShips { get; set; }
        public virtual DbSet<ShipCell> ShipCells { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MapShip>()
                .HasMany(e => e.ShipCells)
                .WithRequired(e => e.MapShip)
                .HasForeignKey(e => e.ShipId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlayerGame>()
                .HasMany(e => e.GameHits)
                .WithRequired(e => e.PlayerGame)
                .HasForeignKey(e => e.GameId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.SecretKey)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.MapShips)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.PlayerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.PlayerGames)
                .WithRequired(e => e.Player1)
                .HasForeignKey(e => e.PlayerId1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.PlayerGames1)
                .WithRequired(e => e.Player2)
                .HasForeignKey(e => e.PlayerId2)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasMany(e => e.PlayerShips)
                .WithRequired(e => e.Player)
                .HasForeignKey(e => e.PlayerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShipCell>()
                .HasMany(e => e.GameHits)
                .WithOptional(e => e.ShipCell)
                .HasForeignKey(e => e.ShipCellId);
            
        }
    }

    
}