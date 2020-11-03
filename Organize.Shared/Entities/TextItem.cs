using System;
using System.Collections.Generic;
using System.Text;

namespace Organize.Shared.Entities
{
    public class TextItem : BaseItem
    {
        private string _subTitle;

        public string SubTitle
        {
            get => _subTitle;
            set => SetProperty(ref _subTitle, value);
        }

        private string _detail;

        public string Detail
        {
            get => _detail;
            set => SetProperty(ref _detail, value);
        }
    }
}
