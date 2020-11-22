using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Organize.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Organize.WASM.OrganizeAuthenticationStateProvider;
using GeneralUI.BusyOverlay;

namespace Organize.WASM.Shared
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        private DotNetObjectReference<MainLayout> _dotNetReference;

        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        [Inject]
        private BusyOverlayService BusyOverlayService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Inject]
        private IAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private IUserItemManager UserItemManager { get; set; }

        private bool IsAuthenticated { get; set; } = false;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        public bool UserShortNavText { get; set; }

        protected void SignOut()
        {
            AuthenticationStateProvider.UnsetUser();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            var authState = await AuthenticationStateTask;
            IsAuthenticated = authState.User.Identity.IsAuthenticated;

            if (!IsAuthenticated || CurrentUserService.CurrentUser.IsUserItemsPropertyLoaded)
            {
                return;
            }

            try
            {
                BusyOverlayService.SetBusyState(BusyEnum.Busy);
                await UserItemManager.RetrieveAllUserItemsOfUserAndSetToUserAsync(CurrentUserService.CurrentUser);
            }
            finally
            {
                BusyOverlayService.SetBusyState(BusyEnum.NotBusy);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var width = await JSRuntime.InvokeAsync<int>("blazorDimension.getWidth");
            CheckUserShortNavText(width);

            _dotNetReference = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("blazorResize.registerReferenceForResizeEvent",
                nameof(MainLayout),
                _dotNetReference);
        }

        [JSInvokable]
        public static void OnResize()
        {

        }

        [JSInvokable]
        public void HandleResize(int width, int height)
        {
            CheckUserShortNavText(width);
        }

        private void CheckUserShortNavText(int width)
        {
            var oldValue = UserShortNavText;
            UserShortNavText = width < 700;
            if (oldValue != UserShortNavText)
            {
                StateHasChanged();
            }
        }

        public async void Dispose()
        {
            await JSRuntime.InvokeVoidAsync("blazorResize.unRegister", nameof(MainLayout));
            _dotNetReference?.Dispose();
        }
    }
}
