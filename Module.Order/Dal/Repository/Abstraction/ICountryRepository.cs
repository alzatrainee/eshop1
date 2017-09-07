using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Dal.Repository.Abstraction
{
    public interface ICountryRepository
    {
        Country GetState(int code);
        List<Country> GetAllCountries();
    }
}
