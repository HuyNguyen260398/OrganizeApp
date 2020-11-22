using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using Organize.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Organize.TestFake
{
    public class TestData
    {
        public static User TestUser { get; set; }

        public static void CreateTestUser(
            IUserItemManager userItemManager = null,
            IUserManager userManager = null)
        {
            var user = new User();
            user.Id = 1;
            user.UserName = "huynguyen";
            user.FirstName = "Huy";
            user.LastName = "Nguyen";
            user.Password = "giahuy";
            user.GenderType = GenderTypeEnum.Male;
            user.UserItems = new ObservableCollection<BaseItem>();

            if (userManager != null)
            {
                userManager.InserUserAsync(user);
            }

            TextItem textItem;
            if (userItemManager != null)
            {
                textItem = (TextItem)userItemManager.CreateNewUserItemAndAddItToUserAsync(user, ItemTypeEnum.Text).Result;
            }
            else
            {
                textItem = new TextItem();
                user.UserItems.Add(textItem);
            }

            textItem.ParentId = user.Id;
            textItem.Id = 1;
            textItem.Title = "Buy Apples";
            textItem.SubTitle = "Red | 5";
            textItem.Detail = "From New Zealand preferred";
            textItem.ItemTypeEnum = ItemTypeEnum.Text;
            textItem.Position = 1;

            UrlItem urlItem;
            if (userItemManager != null)
            {
                urlItem = (UrlItem)userItemManager.CreateNewUserItemAndAddItToUserAsync(user, ItemTypeEnum.Url).Result;
            }
            else
            {
                urlItem = new UrlItem();
                user.UserItems.Add(urlItem);
            }

            urlItem.ParentId = user.Id;
            urlItem.Id = 2;
            urlItem.Title = "Buy Sunflowers";
            urlItem.Url = "https://www.google.com";
            urlItem.ItemTypeEnum = ItemTypeEnum.Url;
            urlItem.Position = 2;

            ParentItem parentItem;
            if (userItemManager != null)
            {
                parentItem = (ParentItem)userItemManager.CreateNewUserItemAndAddItToUserAsync(user, ItemTypeEnum.Parent).Result;
            }
            else
            {
                parentItem = new ParentItem();
                user.UserItems.Add(parentItem);
            }

            parentItem.ParentId = user.Id;
            parentItem.Id = 3;
            parentItem.Title = "Make Birthday Present";
            parentItem.ItemTypeEnum = ItemTypeEnum.Parent;
            parentItem.Position = 3;
            parentItem.ChildItems = new ObservableCollection<ChildItem>();

            ChildItem childItem;
            if (userItemManager != null)
            {
                childItem = (ChildItem)userItemManager.CreateNewChildItemAndAddItToParentItemAsync(parentItem).Result;

                user.UserItems.Clear();
            }
            else
            {
                childItem = new ChildItem();
                parentItem.ChildItems.Add(childItem);
            }

            childItem.ParentId = parentItem.Id;
            childItem.Id = 4;
            childItem.ItemTypeEnum = ItemTypeEnum.Child;
            childItem.Title = "Cut";
            childItem.Position = 4;

            TestUser = user;
        }
    }
}
