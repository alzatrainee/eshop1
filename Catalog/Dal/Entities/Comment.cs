using Alza.Module.UserProfile.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Catalog.Dal.Entities
{
    public class Comment
    {
        public Comment() {}

        public Comment(int id_product, int id_user, string comment, DateTime date, int ? parent_com )
        {
            this.id_pr = id_product;
            this.id_us = id_user;
            this.comment = comment;
            this.date = date;
            this.thumb_up = 0;
            this.thumb_down = 0;
            this.parent_com = parent_com;
            
        }
        public Comment(int id_user, int id_product, string comment, DateTime date)
        {
            this.id_pr = id_product;
            this.id_us = id_user;
            this.comment = comment;
            this.date = date;
            this.thumb_up = 0;
            this.thumb_down = 0;
            this.parent_com = null;
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_com { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int id_pr { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        public int id_us { get; set; }

        [Required]
        [StringLength(400)]
        public string comment { get; set; }


        //[Required]
       // public DateTime PostDate { get; set; }

        public int thumb_up { get; set; }
        public int thumb_down { get; set; }

        [ForeignKey("Comment")]
        public int? parent_com { get; set; }

        public DateTime date { get; set; }
        
    }
}
