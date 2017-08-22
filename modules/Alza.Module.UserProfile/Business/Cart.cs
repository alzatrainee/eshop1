using Alza.Module.UserProfile.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Alza.Module.UserProfile.Business
{
    public class Cart
    {

        public Cart(int id)
        {
            id_user = id;
            id_car = id;
        }

        [Key]
        //ID Cart
        public int id_car { get; set; }

        public int id_user { get; set; }

    }
}
