using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Colour// : Entity
    {
        //[StringLength(6)]
       // public string rgb;
       [Key]
       [StringLength(6)]
       public string rgb { get; set; }
       [StringLength(50)]
       public string name { get; set; }
        
    }
}
