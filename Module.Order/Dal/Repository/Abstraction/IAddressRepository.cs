using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Dal.Repository.Abstraction
{
    public interface IAddressRepository
    {
        Address AddAddress(Address address);
        Address UpdateAddress(Address update);

        /// <summary>
        /// Najde prvni adresu podle Id adresy
        /// </summary>
        /// <param name="id_ad"></param>
        /// <returns></returns>
        Address FindSpecificAddress(int id_ad);
        
        /// <summary>
        /// Najde adresu podle Id uzivatele
        /// </summary>
        /// <param name="id_user"></param>
        /// <returns></returns>
        Address FindAddresByIdUser(int id_user);
    }
}
