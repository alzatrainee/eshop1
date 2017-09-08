using Catalog.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using PernicekWeb.Models.ItemViewModels;
using Alza.Module.UserProfile.Business;
using Module.Business.Business;
using Module.Business.Dal.Entities;

namespace PernicekWeb.Controllers
{
    public class WishListController : Controller
    {
        private readonly CatalogService _catalogService;
        private readonly UserProfileService _userService;
        private readonly BusinessService _businessService;


        public WishListController(CatalogService catalogservice, UserProfileService userservice, BusinessService businessService)
        {
            _catalogService = catalogservice;
            _userService = userservice;
            _businessService = businessService;
        }

        [HttpPost]

        public IActionResult AddToWishList([FromBody] WishProduct product)
        {
            if (_businessService.AlreadyHasThisProductInList(product.id_us, product.id_pr))
            {
                return Json(false);
                
            } else
            {
                _businessService.AddProductToWishList(product.id_us, product.id_pr);
                _catalogService.AddLikeToProduct(product.id_pr);
                return Json(true);
            }
            
        }

        [HttpPost]


        public IActionResult RemoveFromWishList([FromBody] WishProduct product)
        {
            _businessService.RemoveProductFromeWishList(product.id_us, product.id_pr);
            _catalogService.RemoveLikeFromProduct(product.id_pr);
            return Json(false);
            
        }
    }
}
