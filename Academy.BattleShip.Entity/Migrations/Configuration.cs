using System.Data.Entity.Migrations;

namespace Academy.BattleShip.Entity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BattleShipEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BattleShipEntities context)
        {

        }
    }
}
