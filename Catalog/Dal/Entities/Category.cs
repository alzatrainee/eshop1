using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Entities
{
    public class Category //: Entity
    {
        [Key]
        public int id_cat { get; set; }
        [StringLength(200)]
        public string name { get; set; }
        public string description { get; set; }
        


        //VAZBY
      //  public int? ParentId { get; set; }

        //NAVIGATION
        public List<Product_cat> ProductCategories { get; set; } = new List<Product_cat>();
    }


    
}
