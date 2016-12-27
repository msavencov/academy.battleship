namespace Academy.BattleShip.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Current2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "MapValidated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "MapValidated");
        }
    }
}
