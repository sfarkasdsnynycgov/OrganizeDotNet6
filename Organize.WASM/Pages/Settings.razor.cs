using GeneralUi.BusyOverlay;
using Microsoft.AspNetCore.Components;
using OrganizeDotNET6a.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.WASM.Pages
{
    public partial class Settings : ComponentBase
    {
        [Inject]
        private IUserItemManager UserItemManager { get; set; }

        [Inject]
        private BusyOverlayService BusyOverlayService { get; set; }

        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        private async void DeleteAllDone()
        {
            try
            {
                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                await UserItemManager.DeleteAllDoneAsync(CurrentUserService.CurrentUser);
            }
            finally
            {
                BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
            }
        }
    }
}
