using Alza.Core.Module.Abstraction;
using Alza.Core.Module.Http;
using Alza.Module.UserProfile.Dal.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alza.Module.UserProfile.Dal.Repository.Abstraction;
namespace Alza.Module.UserProfile.Business
{
    public class UserProfileService
    {
        private ILogger<UserProfileService> _logger;
        private IUserRepository _userProfileRepo;

        public UserProfileService(IUserRepository userProfileRepo,
                              ILogger<UserProfileService> logger)
        {
            _userProfileRepo = userProfileRepo;
            _logger = logger;
        }

        /**********************************************/
        /*              GET ITEM                      */
        /**********************************************/
        public User GetUserProfile(int Id)
        {

                var result = _userProfileRepo.GetUser(Id);
                AlzaAdminDTO.Data(result);
                return (result);
        }

        /**********************************************/
        /*              ADD ITEM                      */
        /**********************************************/
        public AlzaAdminDTO AddUserProfile(User item)
        {
            try
            {
                _userProfileRepo.AddUserProfile(item);
                return AlzaAdminDTO.Data(item);
            }
            catch (Exception e)
            {

                return Error(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
        
        /**********************************************/
        /*              UPDATE ITEM                      */
        /**********************************************/
          public AlzaAdminDTO UpdateUserProfile(Dal.Entities.User item)
          {
              try
              {
                _userProfileRepo.Update(item);
                return AlzaAdminDTO.Data(item);
              }
              catch (Exception e)
              {

                  return Error(e.Message + Environment.NewLine + e.StackTrace);
              }
          }

        
        /// <summary>
        /// HELPER return and log error
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public AlzaAdminDTO Error(string text)
        {
            Guid errNo = Guid.NewGuid();
            _logger.LogCritical(errNo + " - " + text);
            return AlzaAdminDTO.Error(errNo, "SomeText");
        }

        /**********************************************/
        /*              USER'S NAME                      */
        /**********************************************/

        public User FindNameOfUser(int id_us)
        {
            var result = _userProfileRepo.FindNameOfUser(id_us);
            return result;
        }


    }
}
