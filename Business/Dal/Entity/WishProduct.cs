﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Dal.Entities;

namespace Module.Business.Dal.Entities
{
    public class WishProduct
    {
        public WishProduct() { }

        public WishProduct(int id_pr, int id_us, string name, decimal price, string firm, string logo)
        {
            this.id_pr = id_pr;
            this.id_us = id_us;
            this.name = name;
            this.price = price;
            this.firm = firm;            
            this.logo = logo;
        }

        [Required]
        [ForeignKey("Product")]
        public int id_pr { get; set; }

        [Required]
        [ForeignKey("User")]
        public int id_us { get; set; }
        
        [Required]
        [StringLength(200)]
        public string name { get; set; }

        [Required]
        [StringLength(100)]
        public string firm { get; set; }

        [Required]
        public decimal price { get; set; }

        [Required]
        [StringLength(300)]
        public string logo { get; set; }
    }
}
