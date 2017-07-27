using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    class ProductCategory
    {
        [Key]
        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();

        [Key]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = new Category();
    }
}
