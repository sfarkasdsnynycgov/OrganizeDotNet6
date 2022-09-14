using OrganizeDotNET6a.Shared.Contracts;
using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OrganizeDotNET6a.Business
{
    public class CurrentUserService : ICurrentUserService
    {
        public clsUser CurrentUser {
            get { return CurrentUser; }
            set { CurrentUser = value; }
        }
    }
}
