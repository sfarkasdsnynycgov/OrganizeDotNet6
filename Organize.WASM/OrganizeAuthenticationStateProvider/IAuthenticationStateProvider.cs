using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.WASM.OrganizeAuthenticationStateProvider
{
    public interface IAuthenticationStateProvider
    {
        void SetAuthenticatedState(clsUser objUser);
        void UnsetUser();
    }
}
