using Alza.Core.Module.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Dal.Entities
{
    public class Product //: Entity
    {
        [Key]
        public int id_pr { get; set; }
        [Required]
        [StringLength(200)]
        public string name { get; set; }
        [StringLength(500)]
        public string description { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public int id_fir { get; set; }
        public Firm Firm { get; set; } = new Firm();
        public int id_im { get; set; }

        public Image Image { get; set; }

        /*
        //NAVIGATION
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public List<ProductMedia> ProductMedia { get; set; } = new List<Entities.ProductMedia>();

        
        public List<Media> getVideos()
        {
            List<Media> result = new List<Media>();

            foreach (var item in ProductMedia)
            {
                if (item.Media.MediaType.Value == "Video")
                {
                    result.Add(item.Media);
                }
            }

            return result;
        }

        public List<Media> getImages()
        {
            List<Media> result = new List<Media>();

            foreach (var item in ProductMedia)
            {
                if (item.Media.MediaType.Value == "Image")
                {
                    result.Add(item.Media);
                }
            }

            return result;
        }

        public List<Media> getGames()
        {
            List<Media> result = new List<Media>();

            foreach (var item in ProductMedia)
            {
                if (item.Media.MediaType.Value == "Game")
                {
                    result.Add(item.Media);
                }
            }

            return result;
        }

        public List<Category> getCategories()
        {
            List<Category> result = new List<Category>();

            foreach (var item in ProductCategories)
            {
                result.Add(item.Category);
            }

            return result;
        }
        */
    }
}
