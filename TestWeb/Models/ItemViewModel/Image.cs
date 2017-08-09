using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.ItemViewModels
{
    public class Image
    {
        [Key]
        public int id_im { get; set; }
        [Required]
        [StringLength(200)]
        public string link { get; set; }
        [Required]
        public int id_pr { get; set; }
        public Product Product { get; set; } = new Product();
        [Required]
        [StringLength(6)]
        public string rgb { get; set; }
        public Colour Colour { get; set; } = new Colour();

    }
}
