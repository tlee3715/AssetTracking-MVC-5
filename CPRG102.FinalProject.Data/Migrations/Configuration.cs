namespace CPRG102.FinalProject.Data.Migrations
{
    using CPRG102.FinalProject.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CPRG102.FinalProject.Data.AssetContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CPRG102.FinalProject.Data.AssetContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Manufacturers.AddOrUpdate(
                o => o.Name,
                new Manufacturer { Name = "Dell" },
                new Manufacturer { Name = "HP" },
                new Manufacturer { Name = "Acer" },
                new Manufacturer { Name = "Apple" },
                new Manufacturer { Name = "Samsung" },
                new Manufacturer { Name = "LG" },
                new Manufacturer { Name = "Avaya" },
                new Manufacturer { Name = "Polycom" },
                new Manufacturer { Name = "Cisco" }
                );

            context.Models.AddOrUpdate(
                o => o.Name,
                new Model { Name = "Inspiron", ManufacturerId = 1 },
                new Model { Name = "XPS", ManufacturerId = 1 },
                new Model { Name = "Elite", ManufacturerId = 2 },
                new Model { Name = "Aspire", ManufacturerId = 3 },
                new Model { Name = "Latitude E4550", ManufacturerId = 1 },
                new Model { Name = "Latitude E5550", ManufacturerId = 1 },
                new Model { Name = "Macbook Air", ManufacturerId = 4 },
                new Model { Name = "Macbook Pro", ManufacturerId = 4 },
                new Model { Name = "iPad mini", ManufacturerId = 4 },
                new Model { Name = "iPad Air", ManufacturerId = 4 },
                new Model { Name = "Galaxy Tab3", ManufacturerId = 5 },
                new Model { Name = "S200", ManufacturerId = 3 },
                new Model { Name = "STQ414", ManufacturerId = 3 },
                new Model { Name = "22MP", ManufacturerId = 6 },
                new Model { Name = "Pavilion", ManufacturerId = 2 },
                new Model { Name = "iPhone 5", ManufacturerId = 4 },
                new Model { Name = "iPhone 6", ManufacturerId = 4 },
                new Model { Name = "Galaxy S4", ManufacturerId = 5 },
                new Model { Name = "Galaxy S5", ManufacturerId = 5 },
                new Model { Name = "Galaxy Note5", ManufacturerId = 5 },
                new Model { Name = "9612G", ManufacturerId = 7 },
                new Model { Name = "SoundPoint 331", ManufacturerId = 8 },
                new Model { Name = "SPA525G2", ManufacturerId = 9 }
                );

            context.AssetTypes.AddOrUpdate(
                o => o.Name,
                new AssetType { Name = "Desktop PC" },
                new AssetType { Name = "Laptop" },
                new AssetType { Name = "Tablet" },
                new AssetType { Name = "Monitor" },
                new AssetType { Name = "Mobile Phone" },
                new AssetType { Name = "Desk Phone" }
                );
        }
    }
}
