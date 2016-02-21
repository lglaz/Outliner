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
        public RelayCommand IndentCommand { get; private set; }
        public RelayCommand OutdentCommand { get; private set; }

        public OutlineViewModel()
        {
            _children = new ObservableCollection<OutlineViewModel>();
            AddNewSiblingCommand = new RelayCommand(AddNewSibling);
            IndentCommand = new RelayCommand(Indent);
            OutdentCommand = new RelayCommand(Outdent);
        }

        private void AddNewSibling()
        {
            if(Parent != null)
            {
                var idx = GetPosition() + 1;
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

        private int GetPosition()
        {
            return Parent.Children.IndexOf(this);
        }

        private void Indent()
        {
            var idx = GetPosition();
            if(idx > 0)
            {
                Parent.Children.RemoveAt(idx);
                Parent = Parent.Children[idx - 1];
                Parent.Children.Add(this);
            }
        }

        private void Outdent()
        {
            if(Parent != null && Parent.Parent != null)
            {
                var idx = GetPosition();
                Parent.Children.RemoveAt(idx);
                idx = Parent.GetPosition() + 1;
                Parent = Parent.Parent;
                if (idx >= Parent.Children.Count)
                {
                    Parent.Children.Add(this);
                }
                else
                {
                    Parent.Children.Insert(idx, this);
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
