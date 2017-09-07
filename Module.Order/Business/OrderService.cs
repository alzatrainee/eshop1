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
        private ICountryRepository _countryRepo;
        private IMethodRepository _methodRepo;

        public OrderService(
            IShippingRepository shippingRepo,
            ICartRepository cartRepo,
            IOrderRepository orderRepo,
            IPaymentRepository paymentRepo,
            IAddressRepository addressRepo,
            ICountryRepository countryRepo,
            IMethodRepository methodRepo
             )
        {
            _shippingRepo = shippingRepo;
            _cartRepo = cartRepo;
            _orderRepo = orderRepo;
            _paymentRepo = paymentRepo;
            _addressRepo = addressRepo;
            _countryRepo = countryRepo;
            _methodRepo = methodRepo;
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

        /// <summary>
        /// Vrati tabulku Shipping, kde si muzu vybrat jmeno nebo price, podle Id shipping
        /// </summary>
        /// <param name="id_ship"></param>
        /// <returns></returns>
        public Shipping GetPriceShipping(int id_ship)
        {
                var result = _shippingRepo.GetPrice(id_ship);
                return (result);
        }

        /// <summary>
        /// Vraci posledni provedenou objednavku
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        public NewOrder GetNewOrder(int id_user)
        {
            var result = _orderRepo.GetNewOrder(id_user);
            return (result);
        }

        /// <summary>
        /// Vraci list vsech objednavek
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        public List<NewOrder> GetNewOrderList(int id_user)
        {
            var result = _orderRepo.GetNewOrderList(id_user);
            return (result);
        }

        /// <summary>
        /// Najde adresu podle Id adresy
        /// </summary>
        /// <param name="id_ad"></param>
        /// <returns></returns>
        public Address FindSpecificAddress(int id_ad)
        {
            var result = _addressRepo.FindSpecificAddress(id_ad);
            return (result);
        }

        /// <summary>
        /// Najde adresy podle id usera
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        public Address FindAddresByIdUser(int id_user)
        {
            var result = _addressRepo.FindAddresByIdUser(id_user);
            return (result);
        }

        /// <summary>
        /// Najde informace o payment podle Id payment
        /// </summary>
        /// <param name="id_pay"></param>
        /// <returns></returns>
        public Payment GetPayment(int id_pay)
        {
            var result = _paymentRepo.GetPayment(id_pay);
            return (result);
        }

        /// <summary>
        /// Ziska informace o metode platby
        /// </summary>
        /// <param name="id_meth"></param>
        /// <returns></returns>
        public Method GetPaymentMethod(int id_meth)
        {
            var result = _methodRepo.GetPaymentMethod(id_meth);
            return (result);
        }

        /// <summary>
        /// Vrati prvni objednavku
        /// </summary>
        /// <param name="id_ord"></param>
        /// <returns></returns>
        public NewOrder GetSpecificOrder(int id_ord)
        {
            var result = _orderRepo.GetSpecificOrder(id_ord);
            return (result);
        }

        /// <summary>
        /// Zjisti jmeno zeme podle jeho kodu
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Country GetState (int code)
        {
            var tmp = _countryRepo.GetState(code);
            return tmp;
        }

        public List<Country> GetAllCountries()
        {
            var tmp = _countryRepo.GetAllCountries();
            return tmp;
        }



        /* Pridani jednotlivych casti do databaze */
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
