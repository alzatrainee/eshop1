using Alza.Core.Module.Http;
using Catalog.Dal.Entities;
using Module.Order.Dal.Entities;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;


namespace Module.Order.Business
{
    public class OrderService
    {

        private ICartRepository _cartRepo;
        private IShippingRepository _shippingRepo;
        public OrderService(
            IShippingRepository shippingRepo,
            ICartRepository cartRepo
             )
        {
            _shippingRepo = shippingRepo;
            _cartRepo = cartRepo;
        }

        public AlzaAdminDTO AddCart(Cart item)
        {
            try
            {
                _cartRepo.AddCart(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO GetCart(int id_user)
        {
            var result = _cartRepo.GetCart(id_user);
            return AlzaAdminDTO.Data(result);
        }

        public Shipping GetPriceShipping(int id_ship)
        {
            
                var result = _shippingRepo.GetPrice(id_ship);
                return (result);
           
        }
    }
}
