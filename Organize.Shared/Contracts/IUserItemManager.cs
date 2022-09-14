using OrganizeDotNET6a.Shared.Entities;
using OrganizeDotNET6a.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.Shared.Contracts
{
    public interface IUserItemManager
    {
        Task RetrieveAllUserItemsOfUserAndSetToUserAsync(clsUser user);


        Task<ChildItem> CreateNewChildItemAndAddItToParentItemAsync(ParentItem parent);

        Task<BaseItem> CreateNewUserItemAndAddItToUserAsync(clsUser user, ItemTypeEnum typeEnum);

        Task UpdateAsync<T>(T item) where T : BaseItem;

        Task DeleteAllDoneAsync(clsUser user);
    }
}
