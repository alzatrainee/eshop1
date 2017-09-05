using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PernicekWeb.Models.ItemViewModels
{
    public class Values
    {
        [Required]
        public int likes { get; set; }
        [Required]
        public int dislikes { get; set; }
    }
}
