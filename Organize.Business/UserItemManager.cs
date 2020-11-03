using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Organize.Business
{
    public class UserItemManager : IUserItemManager
    {
        public async Task<ChildItem> CreateNewChildItemAndAddItToParentItemAsync(ParentItem parent)
        {
            var childItem = new ChildItem();
            childItem.ParentId = parent.Id;
            childItem.ItemTypeEnum = ItemTypeEnum.Child;

            parent.ChildItems.Add(childItem);
            return await Task.FromResult(childItem);
        }
    }
}
