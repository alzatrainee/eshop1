using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.SearchViewModels
{
    public class Category
    {
        [Key]
        public int id_cat;
        [StringLength(50)]
        [Required]
        public string name;
        [StringLength(200)]
        public string description;
    }
}
