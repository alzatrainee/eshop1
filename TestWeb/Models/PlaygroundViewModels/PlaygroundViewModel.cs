using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PernicekWeb.Models.PlaygroundViewModels;
using Catalog.Dal.Entities;

namespace Pernicek.Models.PlaygroundViewModels
{
    public class PlaygroundViewModel
    {
        public List<Catalog.Dal.Entities.Firm> Firms { get; set; } = new List<Catalog.Dal.Entities.Firm>();

    }
}
