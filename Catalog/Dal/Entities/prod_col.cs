using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class prod_col
    {
        [Key]
        public int id_pr { get; set; }
        [Key]
        public string rgb { get; set; }
    }
}
