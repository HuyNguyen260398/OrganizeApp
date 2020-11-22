using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralUI.BusyOverlay
{
    public class BusyChangedEventArgs : EventArgs
    {
        public BusyEnum BusyState { get; set; }
    }
}
