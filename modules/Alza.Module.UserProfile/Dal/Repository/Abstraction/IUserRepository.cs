using System;
using System.Collections.Generic;
using System.Text;
using Alza.Core.Module.Abstraction;
using Alza.Module.UserProfile.Dal.Entities;
using System.Linq;

namespace Alza.Module.UserProfile.Dal.Repository.Abstraction
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();
        User AddUserProfile(User profile);

        User GetUser(int Id);
        //User GetByCode(string code);
    }
}
