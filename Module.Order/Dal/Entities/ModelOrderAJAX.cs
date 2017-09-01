using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Module.Order.Dal.Entities
{
    public class ModelOrderAJAX
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int size { get; set; }
        [Required]
        [StringLength(50)]
        public string colour { get; set; }

    }
}
