using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PernicekWeb.Models.PlaygroundViewModels;


namespace PernicekWeb.Models.CommentViewModels
{
    public class Product
    {
        [Key]
        public int id_pr { get; set; }
        [StringLength(200)]
        public string name { get; set; }
        public string[] comment; // obsahuje v sobe informace o Comment
        public string[] nameOfUser; // obsahuje v sobe informace o Comment
        public int[] thumb_up; // obsahuje v sobe informace o Comment
        public int[] thumb_down; // obsahuje v sobe informace o Comment
        public int AmountOfComments;
    }
}
