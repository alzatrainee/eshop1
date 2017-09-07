using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.ManageViewModels
{
    public class PurchaseHistory
    {
        /****************************************************
         *                  Produkt                         *
         ****************************************************/
        public string nameProduct { get; set; }
        public string image { get; set; }
        public string colour { get; set; }
        public int size { get; set; }
        public string Firm { get; set; }

        /// <summary>
        /// Id produktu
        /// </summary>
        public int id_pr { get; set; }

        /// <summary>
        /// Datum vyhotoveni objednavky
        /// </summary>
        public DateTime? date { get; set; }

        /// <summary>
        /// Mnozstvi zakoupeneho produktu
        /// </summary>
        public int quantity { get; set; }

        /****************************************************
         *                  Payment                         *
         ****************************************************/
        public string PaymentMethod { get; set; }
        /// <summary>
        /// Cena produktu * mnozstvi
        /// </summary>
        public decimal amount { get; set; }
        public decimal Price { get; set; }
        public int PaymentOption { get; set; }

        /****************************************************
         *                  Shipping                        *
         ****************************************************/
        public decimal ShippingPrice { get; set; }
        public string ShippingName { get; set; }
        public int ShippingOption { get; set; }

        /****************************************************
         *                  Address                         *
         ****************************************************/
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
        public string Country { get; set; }

        /// <summary>
        /// Pro iterovani ve View
        /// </summary>
        public List<PurchaseHistory> PurchaseH { get; set; } = new List<PurchaseHistory>();

       
    }
}
