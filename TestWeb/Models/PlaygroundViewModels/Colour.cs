using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace PernicekWeb.Models.PlaygroundViewModels
{
    public class Colour
    {
        public List<Catalog.Dal.Entities.Colour> Colours { get; set; } = new List<Catalog.Dal.Entities.Colour>();
        public List<Size> Size { get; set; }
    }
}
