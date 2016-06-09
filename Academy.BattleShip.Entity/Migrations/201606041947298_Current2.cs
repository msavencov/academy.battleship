using System.Collections.Generic;
using System.Linq;

namespace Academy.BattleShip.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Current2 : DbMigration
    {
        public override void Up()
        {
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
            DropTable("dbo.KeyPools");
        }
    }
}
