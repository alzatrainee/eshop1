﻿using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Business
{
    public class FilterProduct
    {

        /****************************************************
         *            Informace o produktu                  *
         ****************************************************/
        /// <summary>
        /// Id produktu
        /// </summary>
        [Key]
        public int id_pr { get; set; }
        [StringLength(200)]
        public string name { get; set; }
        [StringLength(500)]
        public string description { get; set; }
        public decimal price { get; set; }
        public DateTime date { get; set; }
        public int id_fir { get; set; }
        public string firm { get; set; }
        public string image { get; set; }
        public string category;
        public string sub_category;
        public int number_of_color;
        public int likes { get; set; }
        public List<Prod_col> Prod_col { get; set; } = new List<Prod_col>();
        public int id_si { get; set; }

        /****************************************************
         *                  Filtry                          *
         ****************************************************/
        public decimal minPrice { get; set; }
        public decimal maxPrice { get; set; }
        public double page { get; set; }
        public int CurrentPage { get; set; }
        public int SortHigh { get; set; }
        public int SortLow { get; set; }
        public int ItemsPerPage { get; set; }
        /// <summary>
        /// Pro filtraci podle oblibenosti
        /// </summary>
        public int NumbersLike { get; set; }
        /// <summary>
        /// Kontroluje zmackunti tlacitka radit podle oblibenosti
        /// </summary>
        public bool checkFilter { get; set; }
        /// <summary>
        /// Zajistuje rozsviceni daneho filtru
        /// </summary>
        public string FilterHighOn { get; set; }
        public string FilterLowOn { get; set; }
        public string FilterFavouriteOn { get; set; }

        public string FilterPage9On { get; set; }
        public string FilterPage27On { get; set; }
        public string FilterPage69On { get; set; }


        /// <summary>
        /// Id soucasne kategorie
        /// </summary>
        public int IdCategory { get; set; }

        //public override int GetHashCode() { }
        public List<FilterProduct> ProductFilter { get; set; } = new List<FilterProduct>();
        public List<FilterProduct> LatestOffer { get; set; } = new List<FilterProduct>();
        public List<int> HaveThisProductInWishList { get; set; } = new List<int>(); // detekce pritomnosti prroduktu ve WishListu
        public List<int> HaveLatestProductInWishList { get; set; } = new List<int>(); // detekce pritomnosti latest produktu ve WishListu
        public List<Catalog.Dal.Entities.Colour> Colours { get; set; } = new List<Catalog.Dal.Entities.Colour>();
        public List<Catalog.Dal.Entities.Firm> Firms { get; set; } = new List<Catalog.Dal.Entities.Firm>();
        public List<Catalog.Dal.Entities.Size> Sizes { get; set; } = new List<Catalog.Dal.Entities.Size>();

        public List<Product> ProductList { get; set; } = new List<Product>();
        public List<Product> LatestOfferList { get; set; } = new List<Product>();
       

    }
}
