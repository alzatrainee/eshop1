using Module.Order.Dal.Context;
using Module.Order.Dal.Entities;
using Module.Order.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Order.Dal.Repository.Implementation
{
    public class CountryRepository : ICountryRepository
    {
        private readonly OrderDbContext _context;
        public CountryRepository(OrderDbContext context)
        {
            _context = context;
        }

        public Country GetState(int code)
        {
            var result = _context.Country.Where(p => p.code == code).FirstOrDefault();
            return result;
        }

        public List<Country> GetAllCountries()
        {
            var result = _context.Country.OrderBy(p => p.name).ToList();
            return result;
        }

        
    }
}
