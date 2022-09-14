using Blazored.Modal;
using Blazored.Modal.Services;
using GeneralUi.BusyOverlay;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OrganizeDotNET6a.Business;
using OrganizeDotNET6a.Shared.Contracts;
using OrganizeDotNET6a.Shared.Entities;
using OrganizeDotNET6a.WASM.Components;
using OrganizeDotNET6a.WASM.OrganizeAuthenticationStateProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.WASM.Pages
{
    public class SignInBase : SignBase
    {
        protected string Day { get; } = DateTime.Now.DayOfWeek.ToString();

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUserManager UserManager { get; set; }


        [Inject]
        private IModalService ModalService { get; set; }


        [Inject]
        private BusyOverlayService BusyOverlayService { get; set; }

        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        [Inject]
        private IAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public bool ShowPassword { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            objUser = new clsUser
            {
                FirstName = "X",
                LastName = "X",
                PhoneNumber = "123"
            };

            EditContext = new EditContext(objUser);
        }

        protected async void OnSubmit()
        {
            if (!EditContext.Validate())
            {
                return;
            }

            try
            {
                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                var foundUser = await UserManager.TrySignInAndGetUserAsync(objUser);

                if (foundUser != null)
                {
                    AuthenticationStateProvider.SetAuthenticatedState(foundUser);
                    CurrentUserService.CurrentUser = foundUser;
                    NavigationManager.NavigateTo("items");
                } else
                {
                    var parameters = new ModalParameters();
                    parameters.Add(nameof(ModalMessage.Message), "User not found");
                    ModalService.Show<ModalMessage>("Error", parameters);
                }
            } 
            catch(Exception e)
            {
                var parameters = new ModalParameters();
                parameters.Add(nameof(ModalMessage.Message), e.Message);
                ModalService.Show<ModalMessage>("Error", parameters);
            }
            finally
            {
                BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
            }
        }

        //protected string Username { get; set; } = "Ben";

        //protected void HandleUserNameChanged(ChangeEventArgs eventArgs)
        //{
        //    Username = eventArgs.Value.ToString();
        //}

        //protected void HandleUserNameValueChanged(string value)
        //{
        //    Username = value;
        //}
    }
}
