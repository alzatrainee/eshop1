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
        public int? id_st { get; set; }
        public Cart_st Cart_st = new Cart_st();

        //ID usera
        [Required]
        public int id_user { get; set; }
        public User User = new User();
        
    }
}
