using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Dal.Repository.Abstraction
{
    public interface IAddressRepository
    {
        Address AddAddress(Address address);
        Address FindSpecificAddress(int id_ad);
        Address UpdateAddress(Address update);
        Address FindAddresByIdUser(int id_user);
    }
}
