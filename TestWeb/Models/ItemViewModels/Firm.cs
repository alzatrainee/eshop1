using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PernicekWeb.Models.ItemViewModels
{
    public class Firm
    {

        [Key]
        public int id_fir { get; set; }
        [Required]
        [StringLength(50)]
        public string name { get; set; }
        [StringLength(200)]
        public string logo { get; set; }
        [StringLength(200)]
        public string information { get; set; }
    }
}
