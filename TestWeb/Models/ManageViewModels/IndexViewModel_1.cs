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

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "User")]
        public string user { get; set; }
        public int Id { get; set; }

        [Display(Name = "First name")]
        public string name { get; set; }

        [Display(Name = "Last name")]
        public string sec_name { get; set; }

        [Display(Name = "Phone")]
        [Phone]
        public string mobile { get; set; }

        public int id_user { get; set; }

        public int id_car { get; set; }

        /* Adress */
        public string Address { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public int HouseNumber { get; set; }
        public string Country { get; set; }
        public int id_ad { get; set; }

        /* Purchase history */
        public string nameProduct { get; set; }
        public string image { get; set; }
        public string colour { get; set; }
        public int size { get; set; }
        public string Firm { get; set; }

        public int id_ord { get; set; } 
        public int id_pr { get; set; }
        public decimal amount { get; set; }
        public int quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? date { get; set; }
        public int tmpCount { get; set; }

        public List<IndexViewModel_1> OrdProd { get; set; } = new List<IndexViewModel_1>();
        public List<IndexViewModel_1> AddressCheck { get; set; } = new List<IndexViewModel_1>();
        public List<IndexViewModel_1> OrderDetails { get; set; } = new List<IndexViewModel_1>();
        public List<Country> Countries { get; set; } = new List<Country>();

        /* WishList */
        public List<WishProduct> WishList { get; set; } = new List<WishProduct>();

    }
}
