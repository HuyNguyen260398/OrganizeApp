using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organize.WASM.Controls
{
    public class ValidationInputBase : ComponentBase
    {
        //[Parameter]
        //public EventCallback<string> ValueChangedCallBack { get; set; }

        /// 2 way data bindging using @bind-property
        /// the EventCallBack for that property must be named "propertyChanged"
        /// and the generic type of the call back and the property must be same
        
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public string Error { get; set; }

        // Attribute splatting - add HTML attribute to razor component
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> InputAttributes { get; set; }

        protected async void HandleInputChanged(ChangeEventArgs eventArgs)
        {
            //await ValueChangedCallBack.InvokeAsync(eventArgs.Value.ToString());
            await ValueChanged.InvokeAsync(eventArgs.Value.ToString());
        }
    }
}
