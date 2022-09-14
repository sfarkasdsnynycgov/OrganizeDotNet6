using Blazored.Modal;
using Blazored.Modal.Services;
using GeneralUi.BusyOverlay;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Organize.Business;
using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using Organize.WASM.Components;
using Organize.WASM.OrganizeAuthenticationStateProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Organize.WASM.Pages
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
                FirstName = "no",
                LastName = "one",
                PhoneNumber = "2125551212"
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
                CurrentUserService.CurrentUser.UserName = objUser.UserName;
                CurrentUserService.CurrentUser.FirstName = objUser.FirstName;
                CurrentUserService.CurrentUser.LastName = objUser.LastName;
                CurrentUserService.CurrentUser.GenderType = GenderTypeEnum.Neutral;

                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                var foundUser = await UserManager.TrySignInAndGetUserAsync(objUser);

                if (foundUser != null)
                {
                    AuthenticationStateProvider.SetAuthenticatedState(foundUser);
                    CurrentUserService.CurrentUser = foundUser;
                    NavigationManager.NavigateTo("items");
                }
                else
                {
                    var parameters = new ModalParameters();
                    parameters.Add(nameof(ModalMessage.Message), "User ID " + objUser.UserName + " not found, or an unrecognized password entered.");
                    ModalService.Show<ModalMessage>("Information", parameters);
                }
            }
            catch (Exception e)
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
