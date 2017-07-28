using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Colour : Entity
    {
        [StringLength(6)]
        public string rgb;
        [StringLength(200)]
        public string name;
        
    }
}
