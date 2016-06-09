namespace Academy.BattleShip.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Current : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cells", "Selected");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cells", "Selected", c => c.Boolean(nullable: false));
        }
    }
}
