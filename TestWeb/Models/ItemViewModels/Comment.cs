using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.ItemViewModels
{
    public class Comment {
        public Comment(int id_com, string UserName, string comment, int thumb_up, int thumb_down, int ? parent_com, DateTime date)
        {
            this.id_com = id_com;
            this.UsersName = UserName;
            this.comment = comment;
            this.thumb_up = thumb_up;
            this.thumb_down = thumb_down;
            this.parent_com = parent_com;
            this.date = date;
        }
        public Comment(int id_com, string UserName, string comment, int thumb_up, int thumb_down, DateTime date)
        {
            this.id_com = id_com;
            this.UsersName = UserName;
            this.comment = comment;
            this.thumb_up = thumb_up;
            this.thumb_down = thumb_down;
            this.parent_com = null;
            this.date = date;
        }

        [Key]
        public int id_com { get; set; }
      
        //[Required]
        //public DateTime PostDate { get; set; }
        
        public int ? id_us { get; set; }

        [Required]
        public string UsersName { get; set; }

        [Required]
        [StringLength(400)]
        public string comment { get; set; }
        public int thumb_up { get; set; }
        public int thumb_down { get; set; }

        [ForeignKey("Comment")]
        public int? parent_com { get; set; }

        public DateTime date { get; set; }
        
    }
    
}
