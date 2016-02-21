using Outliner.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.ViewModel
{
    public class OutlineViewModel : ObservableObject
    {
        private string _text;
        private ObservableCollection<OutlineViewModel> _children;
        private bool _isSelected;
        private bool _isExpanded;

        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        public ObservableCollection<OutlineViewModel> Children
        {
            get { return _children; }
            set { Set(ref _children, value); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { Set(ref _isSelected, value); }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { Set(ref _isExpanded, value); }
        }

        public OutlineViewModel()
        {
            _children = new ObservableCollection<OutlineViewModel>();
        }
    }
}
