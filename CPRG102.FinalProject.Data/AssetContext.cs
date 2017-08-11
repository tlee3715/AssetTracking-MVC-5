using CPRG102.FinalProject.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG102.FinalProject.Data
{
    public class AssetContext : DbContext
    {
        public AssetContext() : base("name=AssetConnection") { }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Model> Models { get; set; }
    }
}
