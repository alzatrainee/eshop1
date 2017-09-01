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
        private IOrderRepository _orderRepo;
        private IPaymentRepository _paymentRepo;
        private IAddressRepository _addressRepo;
        public OrderService(
            IShippingRepository shippingRepo,
            ICartRepository cartRepo,
            IOrderRepository orderRepo,
            IPaymentRepository paymentRepo,
            IAddressRepository addressRepo
             )
        {
            _shippingRepo = shippingRepo;
            _cartRepo = cartRepo;
            _orderRepo = orderRepo;
            _paymentRepo = paymentRepo;
            _addressRepo = addressRepo;
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

        public NewOrder FindAddress(int id_user)
        {

            var result = _orderRepo.FindAddress(id_user);
            return (result);

        }

        public Address FindSpecificAddress(int id_ad)
        {

            var result = _addressRepo.FindSpecificAddress(id_ad);
            return (result);

        }

        /* Pridani jednotlivych casti do databze */
        public AlzaAdminDTO AddNewOrder(NewOrder item)
        {
            try
            {
                _orderRepo.AddNewOrder(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO AddPayment(Payment item)
        {
            try
            {
                _paymentRepo.AddPayment(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO AddAddress(Address item)
        {
            try
            {
                _addressRepo.AddAddress(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO UpdateNewOrder(NewOrder item)
        {
            try
            {
                _orderRepo.UpdateNewOrder(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO UpdatePayment(Payment item)
        {
            try
            {
                _paymentRepo.UpdatePayment(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

        public AlzaAdminDTO UpdateAddress(Address item)
        {
            try
            {
                _addressRepo.UpdateAddress(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {
                return AlzaAdminDTO.Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }

    }
}
