using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Prod_si
    {
        [Key]
        public int id_pr { get; set; }
        [Key]
        public int id_si { get; set; }
    }
}
