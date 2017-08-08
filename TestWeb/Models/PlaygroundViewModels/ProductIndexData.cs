using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace PernicekWeb.Models.PlaygroundViewModels
{
    public class ProductIndexData
    {
        public IEnumerable<Catalog.Dal.Entities.Product> Products { get; set; }
        public IEnumerable<Catalog.Dal.Entities.Colour> Colours { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}
