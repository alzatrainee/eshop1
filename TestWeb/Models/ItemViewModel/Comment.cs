using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PernicekWeb.Models.ItemViewModels
{
    public class Comment {
        public Comment(int id_com, string UserName, string comment, int thumb_up, int thumb_down)
        {
            this.id_com = id_com;
            this.UsersName = UserName;
            this.comment = comment;
            this.thumb_up = thumb_up;
            this.thumb_down = thumb_down;
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
}
    
}
