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
        private OutlineViewModel _parent;
        private ObservableCollection<OutlineViewModel> _children;
        private bool _isSelected;
        private bool _isExpanded;

        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        public OutlineViewModel Parent
        {
            get { return _parent; }
            set { Set(ref _parent, value); }
        }

        public ObservableCollection<OutlineViewModel> Children
        {
            get { return _children; }
            private set { Set(ref _children, value); }
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

        public RelayCommand AddNewSiblingCommand { get; private set; }

        public OutlineViewModel()
        {
            _children = new ObservableCollection<OutlineViewModel>();

            AddNewSiblingCommand = new RelayCommand(AddNewSibling);
        }

        private void AddNewSibling()
        {
            if(Parent != null)
            {
                var idx = Parent.Children.IndexOf(this) + 1;
                if (idx >= Parent.Children.Count)
                {
                    Parent.Add();
                }
                else
                {                    
                    Parent.Insert(idx);
                }
            }
        }

        public OutlineViewModel Add(string text = "")
        {
            var outline = new OutlineViewModel() { Parent = this, Text = text };
            Children.Add(outline);
            return outline;
        }

        public OutlineViewModel Insert(int index, string text = "")
        {
            var outline = new OutlineViewModel() { Parent = this, Text = text };
            Children.Insert(index, outline);
            return outline;
        }
    }
}
