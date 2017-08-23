using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PernicekWeb.Models.PlaygroundViewModels;

namespace Pernicek.Models.PlaygroundViewModels
{
    public class PlaygroundViewModel
    {
        public string street { get; set; }
        public int house_number { get; set; }
        public string city { get; set; }
        public decimal post_code { get; set; }



        public int Payment { get; set; }
    }
}
