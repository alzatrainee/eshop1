﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.SearchViewModel
{
    public class Product
    {
        [Key]
        public int id_pr { get; set; }
        [StringLength(200)]
        public string name { get; set; }
        [StringLength(500)]
        public string description { get; set; }
        public decimal price { get; set; }
        public DateTime date { get; set; }
        public string firm { get; set; }
        public Catalog.Dal.Entities.Firm Firm { get; set; } = new Catalog.Dal.Entities.Firm();
        public string[] colour { get; set; } // pole stringu obsahujici nazvy barev tohoto produktu ... P.S. koukni jeste na finkci getRGB, vlastne kvuli ni jsi nemohl vypsat vsechny barvy, jelikoz ti vracela jen jednu prvni ze vseho seznamu        
        public int[] size { get; set; }
        public string image { get; set; }
        public string category;
        public string sub_category;
        public int number_of_color;
        public bool isChecked { get; set; }
    }
}
