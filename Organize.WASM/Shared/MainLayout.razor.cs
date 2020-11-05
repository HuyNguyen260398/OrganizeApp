using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Organize.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Shared
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        private DotNetObjectReference<MainLayout> _dotNetReference;

        [Inject]
        private ICurrentUserService CurrentUserService { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        public bool UserShortNavText { get; set; }

        protected void SignOut()
        {

        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var width = await JSRuntime.InvokeAsync<int>("blazorDimension.getWidth");
            CheckUserShortNavText(width);

            _dotNetReference = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("blazorResize.registerReferenceForResizeEvent", _dotNetReference);
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

        public void Dispose()
        {
            _dotNetReference?.Dispose();
        }
    }
}
