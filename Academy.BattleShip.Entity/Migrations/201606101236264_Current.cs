namespace Academy.BattleShip.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Current : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShipCells",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Key = c.String(nullable: false, maxLength: 6),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PlayerId1 = c.Int(nullable: false),
                        PlayerId2 = c.Int(nullable: false),
                        Completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId1)
                .ForeignKey("dbo.Players", t => t.PlayerId2)
                .Index(t => new { t.PlayerId1, t.PlayerId2 }, unique: true, name: "IX_PlayerId1PlayerId2");
            
            CreateTable(
                "dbo.GameHits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Guid(nullable: false),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.KeyPools",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 7),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShipCells", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Games", "PlayerId2", "dbo.Players");
            DropForeignKey("dbo.Games", "PlayerId1", "dbo.Players");
            DropForeignKey("dbo.GameHits", "GameId", "dbo.Games");
            DropIndex("dbo.GameHits", new[] { "GameId" });
            DropIndex("dbo.Games", "IX_PlayerId1PlayerId2");
            DropIndex("dbo.ShipCells", new[] { "PlayerId" });
            DropTable("dbo.KeyPools");
            DropTable("dbo.GameHits");
            DropTable("dbo.Games");
            DropTable("dbo.Players");
            DropTable("dbo.ShipCells");
        }
    }
}
