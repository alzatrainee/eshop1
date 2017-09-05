using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Dal.Entities
{
    public class Product //: Entity
    {
        [Key]
        public int id_pr { get; set; }
        [Required]
        [StringLength(200)]
        public string name { get; set; }
        [StringLength(500)]
        public string description { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public int id_fir { get; set; }

        public int id_im { get; set; }
        public int likes { get; set; }

    }
}
