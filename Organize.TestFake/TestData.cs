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

        public static void CreateTestUser()
        {
            var user = new User();
            user.Id = 1;
            user.UserName = "huynguyen";
            user.FirstName = "Huy";
            user.LastName = "Nguyen";
            user.Password = "giahuy";
            user.GenderType = GenderTypeEnum.Male;
            user.UserItems = new ObservableCollection<BaseItem>();

            var textItem = new TextItem();
            textItem.ParentId = user.Id;
            user.UserItems.Add(textItem);
            textItem.Id = 1;
            textItem.Title = "Buy Apples";
            textItem.SubTitle = "Red | 5";
            textItem.Detail = "From New Zealand preferred";
            textItem.ItemTypeEnum = ItemTypeEnum.Text;
            textItem.Position = 1;

            var urlItem = new UrlItem();
            urlItem.ParentId = user.Id;
            user.UserItems.Add(urlItem);
            urlItem.Id = 2;
            urlItem.Title = "Buy Sunflowers";
            urlItem.Url = "https://www.google.com";
            urlItem.ItemTypeEnum = ItemTypeEnum.Url;
            urlItem.Position = 2;

            var parentItem = new ParentItem();
            parentItem.ParentId = user.Id;
            user.UserItems.Add(parentItem);
            parentItem.Id = 3;
            parentItem.Title = "Make Birthday Present";
            parentItem.ItemTypeEnum = ItemTypeEnum.Parent;
            parentItem.Position = 3;
            parentItem.ChildItems = new ObservableCollection<ChildItem>();

            var childItem = new ChildItem();
            childItem.ParentId = parentItem.Id;
            parentItem.ChildItems.Add(childItem);
            childItem.Id = 4;
            childItem.Title = "Cut";
            childItem.Position = 4;

            TestUser = user;
        }
    }
}
