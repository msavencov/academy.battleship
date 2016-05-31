using System.Data.Entity.Migrations;
using Academy.BattleShip.Entity.Model;

namespace Academy.BattleShip.Entity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BattleShipEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BattleShipEntities context)
        {
            context.Admins.AddOrUpdate(t => t.Login, new Admin() {Login = "admin", Password = "123QWEasd"});
        }
    }
}
