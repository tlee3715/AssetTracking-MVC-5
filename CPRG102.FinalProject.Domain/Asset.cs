using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG102.FinalProject.Domain
{
    public class Asset
    {
        public int Id { get; set; }
        public string TagNumber { get; set; }
        public int AssetTypeId { get; set; }
        public int ManufacturerId { get; set; }
        public int ModelId { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string SerialNumber { get; set; }

        //navigational property
        public virtual AssetType AssetType { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Model Model { get; set; }
    }
}
