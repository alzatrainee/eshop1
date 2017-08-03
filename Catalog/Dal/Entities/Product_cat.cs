using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Product_cat
    {
        [Key]
        public int id_pr { get; set; }
        public Product Product { get; set; } = new Product();
        [Key]
        public int id_cs { get; set; }
        public Category Category { get; set; } = new Category();

    }
}
