using System.Linq;
using Catalog.Dal.Entities;

namespace PernicekWeb.Models.ManageViewModels
{
    public class CatalogViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public IQueryable<Colour> Colours { get; set; }
    }
}
