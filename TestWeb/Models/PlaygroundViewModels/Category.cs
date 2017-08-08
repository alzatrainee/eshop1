using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.PlaygroundViewModels
{
    public class Category
    {
        public IEnumerable<Catalog.Dal.Entities.Category> Categories { get; set; } // = new IEnumerable<Catalog.Dal.Entities.Category>();
        public IEnumerable<Catalog.Dal.Entities.Product> Products { get; set; }// = new IEnumerable<Catalog.Dal.Entities.Product>();
    }
}
