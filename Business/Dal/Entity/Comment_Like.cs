using Alza.Module.UserProfile.Dal.Entities;
using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Module.Business.Dal.Entities
{
    public class Comment_Like
    {
        [Required]
        [ForeignKey("User")]
        public int id_us { get; set; }

        [Required]
        [ForeignKey("Comment")]
        public int id_com { get; set; }

        [Required]
        [StringLength(7)]
        public string type { get; set;}
    }
}
