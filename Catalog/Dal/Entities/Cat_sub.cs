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
        [Required]
        public int id_cat { get; set; }
        public Category Category = new Category();
        public int id_sub { get; set; }
        public Category Subcategory = new Category();

    }
}
