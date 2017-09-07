using Catalog.Configuration;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.Repository.Abstraction;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Module.Business.Dal.Context;
using Module.Business.Dal.Entities;
using Module.Business.Dal.Entity;
using Module.Business.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Business.Dal.Repository.Implementation
{
    public class Product_LikeRepository : IProduct_LikeRepository
    {
        private readonly BusinessDbContext _context;

        public Product_LikeRepository(BusinessDbContext context)
        {
            _context = context;
        }

        public bool ThisUserHasTheProductInHisWishList(int id_us, int id_pr)
        {
            var AllProductsInUsersWishList= _context.Product_Like.Where(c => c.id_us == id_us).ToList();
            var HaveThisProduct = AllProductsInUsersWishList.Where(c => c.id_pr == id_pr).FirstOrDefault();
            if (HaveThisProduct != null)
                return true;
            else
                return false;
        }

        public Product_Like AddProductToWishList(Product_Like NewProductInWishList)
        {
            _context.Product_Like.Add(NewProductInWishList);
            _context.SaveChanges();
            return NewProductInWishList;
        }

        public List<Product_Like> GetAllWishProductsFromThisUser(int id_us)
        {
            var result = _context.Product_Like.Where(p => p.id_us == id_us).ToList();
            return result;
        }

        public void RemoveFromList(Product_Like temp)
        {
            _context.Product_Like.Remove(temp);
            _context.SaveChanges();
        }

    }
}
