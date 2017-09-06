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
        public int likesOld { get; set; }
        [Required]
        public int dislikesOld { get; set; }
        [Required]
        public int likesNew { get; set; }
        [Required]
        public int dislikesNew { get; set; }

    }
}
