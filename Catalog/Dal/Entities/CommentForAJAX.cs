using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Dal.Entities
{ 
    public class CommentForAJAX
    {
        [Required]
        public int id_us { get; set; }

        [Required]
        public int id_pr { get; set; }
        
        [Required]
        public string comment { get; set; }

        public int? parent_com { get; set; }
        
        public DateTime date { get; set; }
    }
}
