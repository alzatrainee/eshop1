using System.ComponentModel.DataAnnotations;
using Catalog.Dal.Entities;

namespace Module.Order.Dal.Entities
{
    public class Cart_pr
    {
        //ID Cart
        [Key]
        public int id_car { get; set; }
        public virtual Cart Cart { get; set; }

        [Key]
        public int id_pr { get; set; } 
        public virtual Product Product { get; set; }

        [Required]
        public int ammount { get; set; }

    }
}
