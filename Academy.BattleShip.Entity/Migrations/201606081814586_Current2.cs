namespace Academy.BattleShip.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Current2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Cells", newName: "ShipCells");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ShipCells", newName: "Cells");
        }
    }
}
