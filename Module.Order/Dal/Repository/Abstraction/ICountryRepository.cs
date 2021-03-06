﻿using Module.Order.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Order.Dal.Repository.Abstraction
{
    public interface ICountryRepository
    {
        /// <summary>
        /// Zjisti jmeno statu podle jeho kodu
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Country GetState(int code);
        List<Country> GetAllCountries();
    }
}
