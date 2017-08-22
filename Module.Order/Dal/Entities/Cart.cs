using System.ComponentModel.DataAnnotations;
using Alza.Module.UserProfile.Dal.Entities;
using Alza.Module.UserProfile.Business;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Order.Dal.Entities
{
    public class Cart 
    {
        public Cart() { }

        public Cart(int id)
        {
            id_user = id;
            id_car = id;
            id_st = 0;
        }

        //ID Cart
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_car { get; set; }

        //ID usera
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_user { get; set; }
        public User User = new User();

        //ID status
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? id_st { get; set; }
        public Cart_st Cart_st = new Cart_st();

       

    }
}
