using OrganizeDotNET6a.Shared.Contracts;
using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.DataAccess
{
    public class UserDataAccess : IUserDataAccess
    {
        private IPersistenceService _persistenceService;

        public UserDataAccess(IPersistenceService persistenceService)
        {
            _persistenceService = persistenceService;
        }

        public async Task<bool> IsUserWithNameAvailableAsync(clsUser objUser)
        {
            var users = await _persistenceService.GetAsync<clsUser>(u => u.UserName == objUser.UserName);
            return users.FirstOrDefault() != null;
        }

        public async Task InsertUserAsync(clsUser objUser)
        {
            await _persistenceService.InsertAsync(objUser);
        }

        public async Task<clsUser> AuthenticateAndGetUserAsync(clsUser objUser)
        {
            var users = await _persistenceService
                .GetAsync<clsUser>(u => u.UserName == objUser.UserName && objUser.Password == u.Password);
            return users.FirstOrDefault();
        }
    }
}
