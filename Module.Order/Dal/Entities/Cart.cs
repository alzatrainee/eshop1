using System.ComponentModel.DataAnnotations;
using Alza.Module.UserProfile.Dal.Entities;
namespace Module.Order.Dal.Entities
{
    public class Cart
    {
        [Key]
        //ID Cart
        public int id_car { get; set; }

        //ID status
        [Required]
        public int id_st { get; set; }
        public virtual Cart_st Cart_st { get; set; }

        //ID usera
        [Required]
        public int id_us { get; set; }
        public virtual User User { get; set; }
        
    }
}
