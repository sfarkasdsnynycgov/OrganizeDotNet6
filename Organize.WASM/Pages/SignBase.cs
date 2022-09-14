using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.WASM.Pages
{
    public class SignBase : ComponentBase
    {
        protected clsUser objUser { get; set; } = new clsUser();

        protected EditContext EditContext { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            EditContext = new EditContext(objUser);
        }

        public string GetError(Expression<Func<object>> fu)
        {
            if (EditContext == null)
            {
                return null;
            }
            return EditContext.GetValidationMessages(fu).FirstOrDefault();
        }
    }
}
