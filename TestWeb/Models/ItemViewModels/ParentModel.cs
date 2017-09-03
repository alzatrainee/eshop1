using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.ItemViewModels
{
    public class ParentModel
    {
        [Required]
        public int id_parent { get; set; }
    }
}
