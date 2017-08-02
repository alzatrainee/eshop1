using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Item
    {
        [Key]
        public int id_it { get; set; }
        [Key]
        [StringLength(6)]
        public string rgb { get; set; }
        [Key]
        public int id_si { get; set; }
        [Key]
        public int id_pr { get; set; }
    }
}
