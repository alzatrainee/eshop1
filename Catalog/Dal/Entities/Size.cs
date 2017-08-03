using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Size : Entity
    {
        [Key]
        public int id_si { get; set; }
        [StringLength(5)]
        public string euro { get; set; }
        public int uk { get; set; }
        public decimal us_wo { get; set; }
    }
}
