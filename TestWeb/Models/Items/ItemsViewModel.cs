using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PernicekWeb.Models.PlaygroundViewModels;

namespace PernicekWeb.Models
{
    public class ItemsViewModel
    {    
        [StringLength(200)]
        public string name { get; set; }
        public decimal price { get; set; }
    }
}
