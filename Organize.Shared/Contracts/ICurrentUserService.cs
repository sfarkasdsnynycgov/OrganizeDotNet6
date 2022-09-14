using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.Shared.Contracts
{
    public interface ICurrentUserService
    {
        clsUser CurrentUser { get; set; }

    }
}
