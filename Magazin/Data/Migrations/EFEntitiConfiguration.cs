using System.Data.Entity.Migrations;
using Data.DAL;

namespace Data.Migrations
{
    internal sealed class EFContextConfiguration : DbMigrationsConfiguration<DataContext>
    {
        public EFContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
