using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Module.Business.Dal.Entity
{
    public class Product_Like
    {
        [Required]
        [ForeignKey("Product")]
        public int id_pr { get; set; }

        [Required]
        [ForeignKey("User")]
        public int id_us { get; set; }
    }
}
