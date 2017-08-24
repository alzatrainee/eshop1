using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PernicekWeb.Models.ItemViewModels
{
    public class Size
    {
        public Size(int id_si, int uk)
        {
            this.id_si = id_si;
            this.uk = uk;
        }

        [Key]
        public int id_si { get; set; }
              
        [Required]
        public int uk { get; set; }
       
    }
}
