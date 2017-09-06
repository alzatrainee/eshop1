using Module.Business.Dal.Entities;
using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.OrderViewModels
{
    public class OrderProduct
    {
        public int id_car { get; set; }
        public int id_pr { get; set; }
        public decimal amount { get; set; }
        public int id_si { get; set; }
        public string id_col { get; set; }
        public int hodn = 1;
        public int quantity { get; set; }
        public decimal OverallPrice { get; set; }

        public string street { get; set; }
        public int house_number { get; set; }
        public string city { get; set; }
        public int post_code { get; set; }
        public string nameCountry { get; set; }
        public int codeCountry { get; set; }

        public string Payment { get; set; }
        public int PaymentMethodNumber { get; set; }

        public int Shipp { get; set; }

        public string ShippingOption { get; set; }
        public int ShippingOptionNumber { get; set; }

        public decimal Price { get; set; }

        public string nameProduct { get; set; }
        public string image { get; set; }
        public string colour { get; set; }
        public int size { get; set; }
        public string Firm { get; set; }
        public int id_ad { get; set; }

        public List<OrderProduct> OrdProd { get; set; } = new List<OrderProduct>();
        public List<OrderProduct> AddressCheck { get; set; } = new List<OrderProduct>();
        public List<Country> Country { get; set; } = new List<Module.Order.Dal.Entities.Country>();
    }
}
