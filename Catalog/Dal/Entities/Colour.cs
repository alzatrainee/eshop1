using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Dal.Entities
{
    public class Colour// : Entity
    {
        //[StringLength(6)]
        // public string rgb;
        [Key]
        [StringLength(6)]
        public string rgb { get; set; }
        [StringLength(50)]
        public string name { get; set; }

        [NotMapped]
        public bool checkboxAnswer { get; set; }

    }
}
