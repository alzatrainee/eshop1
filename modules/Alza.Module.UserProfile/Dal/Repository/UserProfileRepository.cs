using Alza.Module.UserProfile.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Alza.Module.UserProfile.Dal.Entities;
using Alza.Module.UserProfile.Dal.Repository.Abstraction;
using Alza.Module.UserProfile.Dal.Context;


namespace Alza.Module.UserProfile.Dal.Repository
{
    public class UserProfileRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        //private readonly AlzaUserProfileOptions _options;
        //private ILogger<UserProfileRepository> _logger;

        public UserProfileRepository(UserDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> GetAll()
        {
            var temp = _context.User.AsQueryable();
            return temp;
        }

        public User AddUserProfile(User profile)
        {
            _context.User.Add(profile);
            _context.SaveChanges();

            return profile;
        }

        public User GetUser(int Id)
        {
            var temp = _context.User.Where(p => p.id_user == Id).FirstOrDefault();
            return temp;
        }
        public IQueryable<Entities.User> Query()
        {
            throw new NotImplementedException();
        }

        public void Update (User user)
        {
          //  _context.User.Update(user);
            _context.SaveChanges();
        }





        /*********************************************/
        /*               HELPERS                     */
        /*********************************************/

        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="something"></param>
        /// <returns></returns>
        public Object ToParameter(object something)
        {
            if (something == null)
            {
                return DBNull.Value;
            }

            return something;
        }


    }
}
