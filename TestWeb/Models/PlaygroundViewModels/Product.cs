using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PernicekWeb.Models.PlaygroundViewModels;


namespace PernicekWeb.Models.PlaygroundViewModels
{
    public class Product
    {
        [Key]
        public int id_pr { get; set; }
        [StringLength(200)]
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public DateTime date { get; set; }
        public string colour { get; set; }
    }
}
