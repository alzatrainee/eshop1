using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PernicekWeb.Models.CatalogViewModel
{
    public class Item
    {
        private Catalog.Dal.Entities.Product p = new Catalog.Dal.Entities.Product();

        private List<Image> i = new List<Image>();

        private List<Size> s = new List<Size>();

        private List<Colour> c = new List<Colour>();

        public Item(){}

        public Item(Catalog.Dal.Entities.Product product,List<Image> images, List<Size> sizes, List<Colour> colours)
        {
            this.p = product;
            this.i = images;
            this.s = sizes;
            this.c = colours;
        }

        
        
        public int id_col { get; set; }
        public int id_si { get; set; }


        public Catalog.Dal.Entities.Product product { get => p; set => p = value; }
        public List<Image> images { get => i; set => i = value;  }
        public List<Size> sizes { get => s; set => s = value; }
        public List<Colour> colours { get => c; set => c = value; }

        

        

    }
}
