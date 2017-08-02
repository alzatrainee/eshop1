using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Firm
    {
        [Key]
        public int id_fir { get; set; }
        [StringLength(50)]
        public string name { get; set; }
        public string logo { get; set; }
    }
}
