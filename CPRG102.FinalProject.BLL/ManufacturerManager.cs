using CPRG102.FinalProject.Data;
using CPRG102.FinalProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG102.FinalProject.BLL
{
    public class ManufacturerManager
    {
        AssetContext context = new AssetContext();

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            var manufacturers = context.Manufacturers.ToList();
            return manufacturers;
        }

        public void AddModel(Manufacturer manufacturer)
        {
            context.Manufacturers.Add(manufacturer);
            context.SaveChanges(); 
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            var manu = context.Manufacturers.Find(manufacturer.Id);
            manu.Name = manufacturer.Name;
            context.SaveChanges(); 
        }

        public Manufacturer Find(int id)
        {
            var manufacturer = context.Manufacturers.Find(id);
            return manufacturer;
        }
    }
}
