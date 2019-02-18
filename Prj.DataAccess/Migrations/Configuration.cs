namespace Prj.DataAccess.Migrations
{
    using Context;
    using Prj.Domain.Enums;
    using Prj.Domain.Locations;
    using Prj.Domain.Supports;
    using Prj.Domain.Users;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AppDbContext context)
        {
            context.Roles.AddOrUpdate(
                r => r.Name,
                new Role { Name = "Administrator", FaName = "مدیر اصلی", PersonnelType = PersonnelType.Administrator },
                new Role { Name = "Manager", FaName = "مدیر داخلی", PersonnelType = PersonnelType.Manager },
                new Role { Name = "FinancialManager", FaName = "مدیر مالی", PersonnelType = PersonnelType.FinancialManager },
                new Role { Name = "ContactManager", FaName = "مدیر محتوا", PersonnelType = PersonnelType.ContactManager }
                );

           

            //if (!context.Settings.Any())
            //context.Settings.AddOrUpdate(
            //    new Setting()
            //    {
            //        Id = 0,
            //        CostTender = 1000,
            //        MinDayTenderExpired = 7,
            //        MaxDayTenderExpired = 30,
            //        DeadlineCertificate = 10,
            //        MaxWageAmountTender = 3000,
            //        WagePercectTender = 1,
            //        MaxChargeValue = 100000000,
            //        MinChargeValue = 10000
            //    }
            //    );
        }
    }
}
