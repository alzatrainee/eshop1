using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Dal.Repository.Abstraction
{
    public interface IMethodRepository
    {
        /// <summary>
        /// Ziska informace o metode platby
        /// </summary>
        /// <param name="id_meth"></param>
        /// <returns></returns>
        Method GetPaymentMethod(int id_meth);
    }
}
