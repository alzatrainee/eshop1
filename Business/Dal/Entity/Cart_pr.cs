using Catalog.Dal.Entities;
using Module.Order.Dal.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Module.Business.Dal.Entities
{
    public class Cart_pr
    {

        public Cart_pr(int id_car, int id_pr, int amount, int id_si, string id_col)
        {
            this.id_car = id_car;
            this.id_pr = id_pr;
            this.id_si = id_si;
            this.id_col = id_col;
            if (amount != 0)
                this.amount = amount;
            else
                this.amount = 1;

        }

        public Cart_pr() { }


        //ID Cart
        public int id_car { get; set; }
        
        public int id_pr { get; set; } 
        
        [Required]
        public int amount { get; set; }
        public string id_col { get; set; }
        public int id_si { get; set; }

        //public Colour Colour { get; set; }
        public Size Size { get; set; }
        public Product Product { get; set; }
   

        //public List<Product> LiProduct { get; set; } = new List<Product>();
    }
}
