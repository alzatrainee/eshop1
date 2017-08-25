using Catalog.Dal.Entities;
using Module.Order.Dal.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Module.Business.Dal.Entities
{
    public class Cart_pr
    {
        //ID Cart
        public int id_car { get; set; }
        
        public int id_pr { get; set; } 
        
        [Required]
        public int amount { get; set; }
     //   public string id_col { get; set; }
     //   public int id_si { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }

        public List<Product> LiProduct { get; set; } = new List<Product>();
    }
}
