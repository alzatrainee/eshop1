﻿using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Dal.Entities
{
    public class Category : Entity
    {
        [StringLength(200)]
        public string name { get; set; }
        public string description { get; set; }
        public int subcat { get; set; }


        //VAZBY
        public int? ParentId { get; set; }

        //NAVIGATION
        //public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }


    
}