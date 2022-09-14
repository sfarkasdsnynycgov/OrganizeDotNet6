using OrganizeDotNET6a.Shared.Entities;
using OrganizeDotNET6a.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.Shared.Contracts
{
    public interface IUserManager
    {
        Task<clsUser> TrySignInAndGetUserAsync(clsUser objUser);

        Task InsertUserAsync(clsUser objUser);
    }
}
