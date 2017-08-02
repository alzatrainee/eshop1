using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace PernicekWeb.Views.ViewModel
{
    public class ProductColourViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Colour> Colours { get; set; }
    }
}
