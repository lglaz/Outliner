using Newtonsoft.Json;
using Outliner.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.ViewModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OutlineViewModel : ObservableObject, IDragable, IDropable
    {
        private string _text;
        private OutlineViewModel _parent;
        private ObservableCollection<OutlineViewModel> _children;
        private bool _isSelected;
        private bool _isExpanded;
        private bool? _isFocused;

        [JsonProperty]
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
        [JsonProperty]
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
            set
            {
                if (value && Children.Count <= 0)
                    return;
                Set(ref _isExpanded, value);
            }
        }

        public bool? IsFocused
        {
            get { return _isFocused; }
            set { Set(ref _isFocused, value); }
        }

        public RelayCommand AddNewSiblingCommand { get; private set; }
        public RelayCommand IndentCommand { get; private set; }
        public RelayCommand OutdentCommand { get; private set; }
        public RelayCommand GoUpCommand { get; private set; }
        public RelayCommand GoDownCommand { get; private set; }
        public virtual bool IsFocusable { get { return true; } }

        public Type DataType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public OutlineViewModel()
        {
            _children = new ObservableCollection<OutlineViewModel>();
            AddNewSiblingCommand = new RelayCommand(AddNewSibling);
            IndentCommand = new RelayCommand(Indent);
            OutdentCommand = new RelayCommand(Outdent);
            GoUpCommand = new RelayCommand(GoUp);
            GoDownCommand = new RelayCommand(GoDown);
        }

        private void AddNewSibling()
        {
            if(Parent != null)
            {
                var idx = GetPosition() + 1;
                if (idx >= Parent.Children.Count)
                {
                    Parent.Add().IsFocused = true; 
                }
                else
                {                    
                    Parent.Insert(idx).IsFocused = true;
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
                Parent.IsExpanded = true;
                IsFocused = true;
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
                Parent.IsExpanded = true;
                IsFocused = true;                
            }
        }

        private void GoUp()
        {
            var idx = GetPosition();
            if(idx > 0)
            {
                Parent.Children[idx - 1].FocusLastVisibleSubtreeElement();
            }
            else
            {
                if(Parent.IsFocusable)
                {
                    Parent.IsFocused = true;
                }
            }
        }

        private void FocusLastVisibleSubtreeElement()
        {
            if(IsExpanded && Children.Count > 0)
            {
                Children.Last().FocusLastVisibleSubtreeElement();
            }
            else
            {
                IsFocused = true;
            }
        }

        private void GoDown()
        {
            if(IsExpanded && Children.Count > 0)
            {
                Children[0].IsFocused = true;
            }
            else
            {
                FocusNextElement();
            }
        }

        private void FocusNextElement()
        {
            if (Parent == null)
                return;
            var idx = GetPosition() + 1;
            if (idx < Parent.Children.Count)
            {
                Parent.Children[idx].IsFocused = true;
            }
            else
            {
                Parent.FocusNextElement();
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

        internal void RefreshParentalReferences()
        {
            foreach(var child in Children)
            {
                child.Parent = this;
                child.RefreshParentalReferences();
            }
        }

        public void Remove(object i)
        {
            
        }

        public void Drop(object data, int index = -1)
        {
            var outline = data as OutlineViewModel;
            if (outline == null || outline == this)
                return;

            //todo: check if not parent
            outline.Parent.Children.Remove(outline);
            Children.Add(outline);
            outline.Parent = this;

        }
    }
}
