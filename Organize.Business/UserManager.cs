using OrganizeDotNET6a.Shared.Contracts;
using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.Business
{
    public class cUserManager : IUserManager
    {
        private readonly IUserDataAccess _userDataAccess;

        public cUserManager(IUserDataAccess pUserDataAccess)
        {
            _userDataAccess = pUserDataAccess;
        }

        public async Task InsertUserAsync(clsUser objUser)
        {
            var isUserAlreadyAvailable = await _userDataAccess.IsUserWithNameAvailableAsync(objUser);
            if (isUserAlreadyAvailable)
            {
                throw new Exception("Username already exists");
            }

            await _userDataAccess.InsertUserAsync(objUser);
        }

        async Task<clsUser> IUserManager.TrySignInAndGetUserAsync(clsUser objUser)
        {
            return await _userDataAccess.AuthenticateAndGetUserAsync(objUser);
        }
    }
}
