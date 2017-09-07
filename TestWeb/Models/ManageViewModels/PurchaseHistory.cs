﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.ManageViewModels
{
    public class PurchaseHistory
    {
        /* Product */
        public string nameProduct { get; set; }
        public string image { get; set; }
        public string colour { get; set; }
        public int size { get; set; }
        public string Firm { get; set; }
        public int id_pr { get; set; }
        public int id_si { get; set; }
        public string id_col { get; set; }
        public DateTime? date { get; set; }
        
        public int quantity { get; set; }
        
        /* Payment */
        public string PaymentMethod { get; set; }
        public decimal amount { get; set; }
        public decimal Price { get; set; }

        /* Shipping */
        public decimal ShippingPrice { get; set; }
        public string ShippingName { get; set; }

        /* Address */
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
        public string Country { get; set; }

        public List<PurchaseHistory> PurchaseH { get; set; } = new List<PurchaseHistory>();

       
    }
}
