using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Dal.Entities
{
    public class Size
    {
        [Key]
        public int id_si { get; set; }
        [StringLength(5)]
        public string euro { get; set; }
        public int uk { get; set; }
        public int us_wo { get; set; }

        [NotMapped]
        public bool checkboxAnswer { get; set; }
    }
}
