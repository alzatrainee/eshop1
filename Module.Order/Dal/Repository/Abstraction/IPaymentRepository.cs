using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Dal.Repository.Abstraction
{
    public interface IPaymentRepository
    {
        Payment AddPayment(Payment payment);
        Payment UpdatePayment(Payment update);

        /// <summary>
        /// Najde payment informace podle Id payement 
        /// </summary>
        /// <param name="id_pay"></param>
        /// <returns></returns>
        Payment GetPayment(int id_pay);
    }
}
