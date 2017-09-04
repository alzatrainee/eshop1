using System.ComponentModel.DataAnnotations;

namespace Catalog.Dal.Entities
{
    public class Image
    {
        [Key]
        public int id_im { get; set; }
        public string link { get; set; }
        [Key]
        public int id_pr { get; set; }
        public Product Product { get; set; } = new Product();
        [Key]
        [StringLength(6)]
        public string rgb { get; set; }
        public Colour Colour { get; set; } = new Colour();

    }
}
