using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Module.Order.Dal.Entities;
using Module.Business.Dal.Entities;

namespace Pernicek.Models.ManageViewModels
{
    public class IndexViewModel_1
    {
        public bool HasPassword { get; set; }
        /****************************************************
         *             Uzivatelske udaje                    *
         ****************************************************/
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "User")]
        public string user { get; set; }

        [Display(Name = "First name")]
        public string name { get; set; }

        [Display(Name = "Last name")]
        public string sec_name { get; set; }

        [Phone]
        [Display(Name = "Phone")]
        public string mobile { get; set; }


        /****************************************************
         *                  Address                         *
         ****************************************************/
        /// <summary>
        /// Street
        /// </summary>
        [RegularExpression("^[a-zA-Z ]*$")]
        public string Address { get; set; }

        [RegularExpression("^[a-zA-Z ]*$")]
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
        public string Country { get; set; }
        
        /// <summary>
        /// Id objednavky
        /// </summary>
        public int id_ord { get; set; } 
        /// <summary>
        /// Id produktu
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Datum vytvoreni objednavky
        /// </summary>
        public DateTime? date { get; set; }
        public int CountryCode { get; set; }
        /// <summary>
        /// Potrebujeme pro iteraci ve View
        /// </summary>
        public int tmpCount { get; set; }

        /// <summary>
        /// Informace o provedene objednavce
        /// </summary>
        public List<IndexViewModel_1> OrderDetails { get; set; } = new List<IndexViewModel_1>();
        public List<Country> Countries { get; set; } = new List<Country>();

        /****************************************************
         *                  WishList                        *
         ****************************************************/
        public List<WishProduct> WishList { get; set; } = new List<WishProduct>();

    }
}
