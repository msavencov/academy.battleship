namespace Academy.BattleShip.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Current3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShipCells", "PlayerId", "dbo.Players");
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlayerId1 = c.Int(nullable: false),
                        PlayerId2 = c.Int(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        Player1_Id = c.Int(nullable: false),
                        Player2_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.Player1_Id)
                .ForeignKey("dbo.Players", t => t.Player2_Id)
                .Index(t => new { t.PlayerId1, t.PlayerId2 }, unique: true, name: "IX_PlayerId1PlayerId2")
                .Index(t => t.Player1_Id)
                .Index(t => t.Player2_Id);
            
            AddForeignKey("dbo.ShipCells", "PlayerId", "dbo.Players", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShipCells", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Games", "Player2_Id", "dbo.Players");
            DropForeignKey("dbo.Games", "Player1_Id", "dbo.Players");
            DropIndex("dbo.Games", new[] { "Player2_Id" });
            DropIndex("dbo.Games", new[] { "Player1_Id" });
            DropIndex("dbo.Games", "IX_PlayerId1PlayerId2");
            DropTable("dbo.Games");
            AddForeignKey("dbo.ShipCells", "PlayerId", "dbo.Players", "Id", cascadeDelete: true);
        }
    }
}
