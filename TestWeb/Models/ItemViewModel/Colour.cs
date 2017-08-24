using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.ItemViewModels
{
    public class Colour
    {
        public Colour(string rgb, string name)
        {
            this.rgb = rgb;
            this.name = name;
        }
        [Key]
        public string rgb { get; set; }
        [StringLength(200)]
        public string name { get; set; }
    }
}
