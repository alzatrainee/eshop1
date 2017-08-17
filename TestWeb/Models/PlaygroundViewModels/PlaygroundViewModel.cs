using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PernicekWeb.Models.PlaygroundViewModels;

namespace Pernicek.Models.PlaygroundViewModels
{
    public class PlaygroundViewModel
    {
        public FilterProduc Product { get; set; }

        public Colour Colour { get; set; }
    }
}
