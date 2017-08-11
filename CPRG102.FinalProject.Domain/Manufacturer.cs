using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG102.FinalProject.Domain
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //navigational properties
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<Model> Models { get; set; }
    }
}
