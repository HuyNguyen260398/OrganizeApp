using Organize.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organize.Shared.Entities
{
    public class BaseItem : BaseEntity
    {
        private int _parentId;

        public int ParentId
        {
            get => _parentId;
            set => SetProperty(ref _parentId, value);
        }

        private ItemTypeEnum _itemTypeEnum;

        public ItemTypeEnum ItemTypeEnum
        {
            get => _itemTypeEnum;
            set => SetProperty(ref _itemTypeEnum, value);
        }

        private int _position;

        public int Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private bool _isDone;

        public bool IsDone
        {
            get => _isDone;
            set => SetProperty(ref _isDone, value);
        }

        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
