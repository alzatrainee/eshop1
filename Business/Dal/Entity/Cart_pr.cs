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

        public int id_si { get; set; }

        public string id_col { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }

        public Cart_pr(int id_car, int id_pr, int amount, int id_si, string id_col)
        {
            this.id_car = id_car;
            this.id_pr = id_pr;
            this.amount = amount;
            this.id_si = id_si;
            this.id_col = id_col;
        }

        public Cart_pr() { }

    }
}
