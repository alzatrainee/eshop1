using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Catalog.Dal.Entities
{
    public class InterestedIn
    {
        [Key]
        public int id_pr { get; set; }
        [Required]
        [StringLength(300)]
        public string name { get; set; }
        [Required]
        [StringLength(50)]
        public string description { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        [StringLength(200)]
        public string obrazek { get; set; }
    }
}
