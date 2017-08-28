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
        public string id_col { get; set; }
        public int id_si { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }

        public Cart_pr(int id_car, int Idecko, int amount, int Sizes, string Colours)
        {
            this.id_car = id_car;
            this.id_pr = Idecko;
            this.amount = amount;
            this.id_si = Sizes;
            this.id_col = Colours;
        }

        public Cart_pr()
        {
            amount = 0;
        }

        public List<Product> LiProduct { get; set; } = new List<Product>();
    }
}
