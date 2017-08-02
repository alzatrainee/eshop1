using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Cat_sub
    {
        [Key]
        public int id_cs { get; set; }
        [Key]
        public int id_cat { get; set; }
        [Key]
        public int id_sub { get; set; }
    }
}
