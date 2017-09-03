using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PernicekWeb.Models.ItemViewModels;

namespace Pernicek.Models.ItemViewModels
{
    public class ItemViewModel
    {
        public Product Product { get; set; }

        public Colour Colour { get; set; }

        public Size Size { get; set; }

        public Image Image { get; set; }

        public Category Category { get; set; }

        public Category Sub_category { get; set; }
        
        public Firm Firm { get; set; }

        public Comment Comment { get; set; }
        
        public InterestedIn InterestedIn { get; set; }

        public ParentModel ParentModel { get; set; }
    }
}