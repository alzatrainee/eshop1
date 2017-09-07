using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PernicekWeb.Models.PlaygroundViewModels;
using PernicekWeb.Models.ItemViewModels;

namespace PernicekWeb.Models.ItemViewModels
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
        public string descriptionOfFirm { get; set; }
        public Firm Firm { get; set; } = new Firm();
        //public string[] colour { get; set; } // pole stringu obsahujici nazvy barev tohoto produktu ... P.S. koukni jeste na finkci getRGB, vlastne kvuli ni jsi nemohl vypsat vsechny barvy, jelikoz ti vracela jen jednu prvni ze vseho seznamu        
        public List<Size> sizes { get; set; }
        public List<Colour> colours { get; set; }
        public List<Image> images;
        public string category;
        public string sub_category;
        public List<Comment> comments;
        public List<string> UsersLikes = new List<string>();
        public List<InterestedIn> IntrestedIn;
        public List<int> InterestedInWishList; // odhaduje, jestli je produkt z InterestedIn uz ve  WishListu .... 0 - ne, 1 - ano
        public int AmountOfComments;
        public string comment; // Nove pridany komentar !!!!!!! Strasne dulezite ho tu nechat

        public string Colours { get; set; }
        public int Sizes { get; set; }
        public int Idecko { get; set; }
        public int likes { get; set; }
    }
}
