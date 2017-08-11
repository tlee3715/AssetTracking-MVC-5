using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG102.FinalProject.Domain
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }

        //navigational properties
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
