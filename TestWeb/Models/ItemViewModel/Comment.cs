using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.ItemViewModels
{
    public class Comment {
    [Key]
     public int id_com { get; set; }

    [Required]
    [ForeignKey("Product")]
     public int id_pr { get; set; }
     public virtual Product Product { get; set; }
     

    [Required]
    public int id_us { get; set; }

    [Required]
    [StringLength(400)]
     public string comment { get; set; }
     public int thumb_up { get; set; }
     public int thumb_down { get; set; }
}
    
}
