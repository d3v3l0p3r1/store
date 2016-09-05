using System;
using System.Data.Entity.Migrations;
using Base.Security.Entities;
using Data.DAL;
using Data.Entities;

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
