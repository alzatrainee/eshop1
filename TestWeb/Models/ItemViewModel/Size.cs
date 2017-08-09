using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PernicekWeb.Models.ItemViewModels
{
    public class Size
    {
        [Key]
        public int id_si { get; set; }
        [Required]
        [StringLength(5)]
        public string euro { get; set; }
        [Required]
        public int uk { get; set; }
        [Required]
        public int us_wo { get; set; }
    }
}
