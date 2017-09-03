using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Catalog.Dal.Entities
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
        [StringLength(500)]
        public string information { get; set; }

        [NotMapped]
        public bool checkboxAnswer { get; set; }
    }
}
