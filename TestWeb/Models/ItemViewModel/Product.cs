using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PernicekWeb.Models.PlaygroundViewModels;


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
        public Firm Firm { get; set; } = new Firm();
        public string[] colour { get; set; } // pole stringu obsahujici nazvy barev tohoto produktu ... P.S. koukni jeste na finkci getRGB, vlastne kvuli ni jsi nemohl vypsat vsechny barvy, jelikoz ti vracela jen jednu prvni ze vseho seznamu        
        public int[] size { get; set; }
        public string[] image { get; set; }
        public string category;
        public string sub_category;
        public List<Comment> comments;

        //public string[] comment; // obsahuje v sobe informace o Comment
        //public string[] nameOfUser; // obsahuje v sobe informace o Comment
        //public int[] thumb_up; // obsahuje v sobe informace o Comment
        //public int[] thumb_down; // obsahuje v sobe informace o Comment
        public int AmountOfComments;
    }
}
