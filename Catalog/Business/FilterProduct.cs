using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Business
{
    public class FilterProduct
    {
        [Key]
        public int id_pr { get; set; }
        [StringLength(200)]
        public string name { get; set; }
        [StringLength(500)]
        public string description { get; set; }
        public decimal price { get; set; }
        public DateTime date { get; set; }
        public int id_fir { get; set; }
        public string firm { get; set; }
        //   public Catalog.Dal.Entities.Firm Firm { get; set; } = new Catalog.Dal.Entities.Firm();
        public string[] colour { get; set; } // pole stringu obsahujici nazvy barev tohoto produktu ... P.S. koukni jeste na finkci getRGB, vlastne kvuli ni jsi nemohl vypsat vsechny barvy, jelikoz ti vracela jen jednu prvni ze vseho seznamu        
        public int[] size { get; set; }
        public string image { get; set; }
        public string category;
        public string sub_category;
        public int number_of_color;
        public bool isChecked { get; set; }
        public decimal minPrice { get; set; }
        public double page { get; set; }
        public List<Catalog.Dal.Entities.Product> Ident { get; set; } = new List<Catalog.Dal.Entities.Product>();

        public List<Catalog.Dal.Entities.Colour> Colours { get; set; } = new List<Catalog.Dal.Entities.Colour>();
        public List<Catalog.Dal.Entities.Firm> Firms { get; set; } = new List<Catalog.Dal.Entities.Firm>();
        public List<Catalog.Dal.Entities.Size> Sizes { get; set; } = new List<Catalog.Dal.Entities.Size>();
        public List<Catalog.Dal.Entities.Prod_si> SizeFilter { get; set; } = new List<Catalog.Dal.Entities.Prod_si>();

        public List<int> Firmy { get; set; } = new List<int>();

        public int[] FirmsArray { get; set; }
        public int[] SizesArray { get; set; }
        public string[] ColoursArray { get; set; }

        public int SortHigh { get; set; }
        public int SortLow { get; set; }

        public int IdCategory { get; set; }

        public List<FilterProduct> ProductFilter { get; set; } = new List<FilterProduct>();

    }
}
