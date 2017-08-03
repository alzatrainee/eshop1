using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace PernicekWeb.Views.ViewModel
{
    public class CatalogViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public IQueryable<Colour> Colours { get; set; }
    }
}
