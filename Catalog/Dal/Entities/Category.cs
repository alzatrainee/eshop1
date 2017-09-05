using System.ComponentModel.DataAnnotations;

namespace Catalog.Dal.Entities
{
    public class Category 
    {
        [Key]
        public int id_cat { get; set; }
        [StringLength(200)]
        public string name { get; set; }
        public string description { get; set; }
        


        //VAZBY
        //public int? ParentId { get; set; }

        //NAVIGATION
        //public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }


    
}
