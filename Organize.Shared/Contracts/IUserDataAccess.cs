using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.Shared.Contracts
{
    public interface IUserDataAccess
    {
        Task<clsUser> AuthenticateAndGetUserAsync(clsUser objUser);
        Task InsertUserAsync(clsUser objUser);
        Task<bool> IsUserWithNameAvailableAsync(clsUser objUser);
    }
}
