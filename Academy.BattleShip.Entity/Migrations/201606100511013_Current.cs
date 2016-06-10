using System.Collections.Generic;
using System.Linq;

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
                        Id = c.Guid(nullable: false),
                        PlayerId1 = c.Int(nullable: false),
                        PlayerId2 = c.Int(nullable: false),
                        Completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId1)
                .ForeignKey("dbo.Players", t => t.PlayerId2)
                .Index(t => new { t.PlayerId1, t.PlayerId2 }, unique: true, name: "IX_PlayerId1PlayerId2");
            
            CreateTable(
                "dbo.KeyPools",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 7),
                    })
                .PrimaryKey(t => t.Key);

            var items = from x in Enumerable.Range(10, 90) from y in Enumerable.Range(100, 900) select +y + "-" + x;
            var lines = new List<string>(items);

            IEnumerable<string> iteration;
            var current = 0;

            while ((iteration = lines.Skip(current).Take(1000)).Any())
            {
                var values = iteration.Aggregate(string.Empty, (current1, s) => current1 + (", " + "('" + s + "')")).TrimStart(' ', ',');
                Sql("insert into KeyPools([Key]) values " + values);
                current += 1000;
            }

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShipCells", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Games", "PlayerId2", "dbo.Players");
            DropForeignKey("dbo.Games", "PlayerId1", "dbo.Players");
            DropIndex("dbo.Games", "IX_PlayerId1PlayerId2");
            DropIndex("dbo.ShipCells", new[] { "PlayerId" });
            DropTable("dbo.KeyPools");
            DropTable("dbo.Games");
            DropTable("dbo.Players");
            DropTable("dbo.ShipCells");
        }
    }
}
