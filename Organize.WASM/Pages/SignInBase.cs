using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Organize.Business;
using Organize.Shared.Contracts;
using Organize.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Organize.WASM.Pages
{
    public class SignInBase : SignBase
    {
        //protected string Username { get; set; } = "Username";

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IUserManager UserManager { get; set; }

        protected string Day { get; } = DateTime.Now.DayOfWeek.ToString();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            User = new User
            {
                FirstName = "Huy",
                LastName = "Nguyen",
                PhoneNumber = "123"
            };

            EditContext = new EditContext(User);
        }

        protected async void OnSubmit()
        {
            if (!EditContext.Validate())
            {
                return;
            }

            var foundUser = await UserManager.TrySignInAndGetUserAsync(User);

            if (foundUser != null)
            {
                NavigationManager.NavigateTo("items");
            }
        }

        //protected User User { get; set; } = new User();

        //protected EditContext EditContext { get; set; }

        //protected override void OnInitialized()
        //{
        //    base.OnInitialized();
        //    EditContext = new EditContext(User);
        //}

        //protected void HandleUserNameChanged(ChangeEventArgs eventArgs)
        //{
        //    Username = eventArgs.Value.ToString();
        //}

        //protected void HandleUserNameValueChanged(string value)
        //{
        //    Username = value;
        //}

        //public string GetError(Expression<Func<Object>> fu)
        //{
        //    if (EditContext == null)
        //    {
        //        return null;
        //    }
        //    return EditContext.GetValidationMessages(fu).FirstOrDefault();
        //}
    }
}
