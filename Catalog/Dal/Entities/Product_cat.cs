using System.ComponentModel.DataAnnotations;

namespace Catalog.Dal.Entities
{
    public class Product_cat
    {
        [Key]
        public int id_pr { get; set; }
        public Product Product { get; set; } = new Product();
        [Key]
        public int id_cs { get; set; }
        public Cat_sub cat_sub { get; set; } = new Cat_sub();

    }
}
