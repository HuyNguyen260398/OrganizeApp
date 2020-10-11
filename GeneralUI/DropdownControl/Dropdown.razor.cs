using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralUI.DropdownControl
{
    public partial class Dropdown<TValue> : ComponentBase
    {
        [Parameter]
        public IList<DropdownItem<TValue>> SelectableItems { get; set; }

        [Parameter]
        public DropdownItem<TValue> SelectedItem { get; set; }

        [Parameter]
        public EventCallback<DropdownItem<TValue>> SelectedItemChanged { get; set; } // 2-way binding

        public async void OnItemClicked(DropdownItem<TValue> item)
        {
            SelectedItem = item;
            StateHasChanged(); // re-render the UI when there a state changed
            await SelectedItemChanged.InvokeAsync(item);
        }
    }
}
