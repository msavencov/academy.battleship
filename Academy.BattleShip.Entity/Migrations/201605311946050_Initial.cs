using System.IO;

namespace Academy.BattleShip.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameHits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        ShipCellId = c.Int(),
                        X_Pos = c.Byte(nullable: false),
                        Y_Pos = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlayerGames", t => t.GameId)
                .ForeignKey("dbo.ShipCell", t => t.ShipCellId)
                .Index(t => t.GameId)
                .Index(t => t.ShipCellId);
            
            CreateTable(
                "dbo.PlayerGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId1 = c.Int(nullable: false),
                        PlayerId2 = c.Int(nullable: false),
                        Start_DT = c.DateTime(nullable: false),
                        End_DT = c.DateTime(),
                        QtyHits = c.Int(nullable: false),
                        QtySuccessfullHits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Player", t => t.PlayerId1)
                .ForeignKey("dbo.Player", t => t.PlayerId2)
                .Index(t => t.PlayerId1)
                .Index(t => t.PlayerId2);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        IdPlayer = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        SecretKey = c.String(nullable: false, maxLength: 10, fixedLength: true, unicode: false),
                        isRegistered = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdPlayer);
            
            CreateTable(
                "dbo.MapShip",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        ShipType = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.ShipCell",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShipId = c.Int(nullable: false),
                        X_Pos = c.Byte(),
                        Y_Pos = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MapShip", t => t.ShipId)
                .Index(t => t.ShipId);

            CreateTable(
                "dbo.PlayerShips",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PlayerId = c.Int(nullable: false),
                    ShipTypeId = c.Byte(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.PlayerId);

            var spGameHitFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StoredProcedures", "spGameHit.1.sql");
            SqlFile(spGameHitFile);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerShips", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.PlayerGames", "PlayerId2", "dbo.Player");
            DropForeignKey("dbo.PlayerGames", "PlayerId1", "dbo.Player");
            DropForeignKey("dbo.MapShip", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.ShipCell", "ShipId", "dbo.MapShip");
            DropForeignKey("dbo.GameHits", "ShipCellId", "dbo.ShipCell");
            DropForeignKey("dbo.GameHits", "GameId", "dbo.PlayerGames");
            DropIndex("dbo.PlayerShips", new[] { "PlayerId" });
            DropIndex("dbo.ShipCell", new[] { "ShipId" });
            DropIndex("dbo.MapShip", new[] { "PlayerId" });
            DropIndex("dbo.PlayerGames", new[] { "PlayerId2" });
            DropIndex("dbo.PlayerGames", new[] { "PlayerId1" });
            DropIndex("dbo.GameHits", new[] { "ShipCellId" });
            DropIndex("dbo.GameHits", new[] { "GameId" });
            DropTable("dbo.PlayerShips");
            DropTable("dbo.ShipCell");
            DropTable("dbo.MapShip");
            DropTable("dbo.Player");
            DropTable("dbo.PlayerGames");
            DropTable("dbo.GameHits");
            DropTable("dbo.Admins");

            var spGameHitFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StoredProcedures", "spGameHit.0.sql");
            SqlFile(spGameHitFile);
        }
    }
}
