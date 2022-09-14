using Microsoft.AspNetCore.Components;
using OrganizeDotNET6a.Shared.Contracts;
using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.WASM.Components
{
    public partial class ChildItemEdit : ComponentBase
    {
        [Inject]
        private IUserItemManager UserItemManager { get; set; }
        
        [Parameter]
        public ParentItem ParentItem { get; set; }



        private async Task AddNewChildToParentAsync()
        {
            await UserItemManager.CreateNewChildItemAndAddItToParentItemAsync(ParentItem);
        }

        private async void OnChildItemTitleChanged(ChildItem childItem,ChangeEventArgs eventArgs)
        {
            childItem.Title = eventArgs.Value.ToString();
            await UserItemManager.UpdateAsync(childItem);
        }
    }
}
