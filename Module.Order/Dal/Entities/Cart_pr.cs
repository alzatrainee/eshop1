using System.ComponentModel.DataAnnotations;
using Catalog.Dal.Entities;

namespace Module.Order.Dal.Entities
{
    public class Cart_pr
    {
        //ID Cart
        [Key]
        public int id_car { get; set; }
        public Cart Cart { get; set; }

        [Key]
        public int id_pr { get; set; } 
        public Product Product { get; set; }

        [Required]
        public int amount { get; set; }

    }
}
