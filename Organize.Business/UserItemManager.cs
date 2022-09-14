using OrganizeDotNET6a.Shared.Contracts;
using OrganizeDotNET6a.Shared.Entities;
using OrganizeDotNET6a.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.Business
{
    public class UserItemManager : IUserItemManager
    {
        private IForItemDataAccess _itemDataAccess;

        public UserItemManager(IForItemDataAccess itemDataAccess)
        { // constructor
            _itemDataAccess = itemDataAccess; // injection by client
        }

        public async Task RetrieveAllUserItemsOfUserAndSetToUserAsync(clsUser user)
        {
            var allItems = new List<BaseItem>();

            var textItems = await _itemDataAccess.GetItemsOfUserAsync<TextItem>(user.Id);
            var urlItems = await _itemDataAccess.GetItemsOfUserAsync<UrlItem>(user.Id);
            var parentItems = await _itemDataAccess.GetItemsOfUserAsync<ParentItem>(user.Id);
            var parentItemsList = parentItems.ToList();
            foreach (var parentItem in parentItemsList)
            {
                var childItems = await _itemDataAccess.GetItemsOfUserAsync<ChildItem>(parentItem.Id);
                parentItem.ChildItems = new ObservableCollection<ChildItem>(childItems.OrderBy(c => c.Position));
            }

            allItems.AddRange(textItems);
            allItems.AddRange(urlItems);
            allItems.AddRange(parentItemsList);

            user.IsUserItemsPropertyLoaded = true;
            user.UserItems = new ObservableCollection<BaseItem>(allItems.OrderBy(i => i.Position));
        }

        public async Task<ChildItem> CreateNewChildItemAndAddItToParentItemAsync(ParentItem parent)
        {
            var newChildItem = new ChildItem();
            newChildItem.ParentId = parent.Id;
            newChildItem.ItemTypeEnum = ItemTypeEnum.Child; // so I can tell the difference between a parent-child and a text or url type at the same level as parent

            //parent.ChildItems.Add(childItem);
            //return await Task.FromResult(childItem); // need an await command in every async method

            // instead of the above via the persistence service ...
            // **************
            await _itemDataAccess.InsertItemAsync<ChildItem>(newChildItem);

            parent.ChildItems.Add(newChildItem);
            return newChildItem;
            // **************
        }

        public async Task<BaseItem> CreateNewUserItemAndAddItToUserAsync(clsUser objUser, ItemTypeEnum typeTextUrlParent)
        {
            BaseItem anItem = null;
            switch (typeTextUrlParent)
            {
                case ItemTypeEnum.Text:
                    anItem = await CreateAndInsertItemAsync<TextItem>(objUser, typeTextUrlParent);
                    break;
                case ItemTypeEnum.Url:
                    anItem = await CreateAndInsertItemAsync<UrlItem>(objUser, typeTextUrlParent);
                    break;
                case ItemTypeEnum.Parent:
                    var parent = await CreateAndInsertItemAsync<ParentItem>(objUser, typeTextUrlParent);
                    parent.ChildItems = new ObservableCollection<ChildItem>();
                    anItem = parent;
                    break;
            }

            objUser.UserItems.Add(anItem);
            return anItem;
        }

        private async Task<T> CreateAndInsertItemAsync<T>(
            clsUser objUser,
            ItemTypeEnum typeTextUrlParent) where T : BaseItem, new()
        {
            // need a public method to invoke CreateAdnInsertItemAsync 

            var item = new T();
            item.ItemTypeEnum = typeTextUrlParent;
            item.Position = objUser.UserItems.Count + 1;
            item.ParentId = objUser.Id;

            // return await Task.FromResult(item);

            // use persistence instead ...
            await _itemDataAccess.InsertItemAsync(item);

            return item;
        }

        public async Task UpdateAsync<T>(T item) where T : BaseItem
        {
            switch (item)
            {
                case TextItem textItem:
                    await _itemDataAccess.UpdateItemAsync(textItem);
                    break;
                case UrlItem urlItem:
                    await _itemDataAccess.UpdateItemAsync(urlItem);
                    break;
                case ParentItem parentItem:
                    await _itemDataAccess.UpdateItemAsync(parentItem);
                    break;
                case ChildItem childItem:
                    await _itemDataAccess.UpdateItemAsync(childItem); 
                    break;
            }

        }

        public async Task DeleteAllDoneAsync(clsUser user)
        {
            var doneItems = user.UserItems.Where(i => i.IsDone).ToList();
            Console.WriteLine(doneItems.Count);

            var doneParentItem = doneItems.OfType<ParentItem>().ToList();
            var allChildItems = doneParentItem.SelectMany(i => i.ChildItems).ToList();

            await _itemDataAccess.DeleteItemsAsync(allChildItems);
            await _itemDataAccess.DeleteItemsAsync(doneParentItem);
            await _itemDataAccess.DeleteItemsAsync(doneItems.OfType<TextItem>());
            await _itemDataAccess.DeleteItemsAsync(doneItems.OfType<UrlItem>());

            foreach (var doneItem in doneItems)
            {
                user.UserItems.Remove(doneItem);
            }

            var sortedByPosition = user.UserItems.OrderBy(i => i.Position);
            var position = 1;
            foreach (var item in sortedByPosition)
            {
                item.Position = position;
                position++;
                await UpdateAsync(item);
            }
        }
    }
}
