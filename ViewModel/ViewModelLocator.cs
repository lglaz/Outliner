using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.ViewModel
{
    public class ViewModelLocator
    {
        private OutlineDocumentViewModel _document;

        public OutlineDocumentViewModel Document
        {
            get { return _document; }            
        }

        public ViewModelLocator ()
        {
            _document = new OutlineDocumentViewModel();
            var o1 = new OutlineViewModel() { Text = "Outline 1" };
            _document.Outlines.Add(o1);
            _document.Outlines.Add(new OutlineViewModel() { Text = "Outline 2" });
            _document.Outlines.Add(new OutlineViewModel() { Text = "Outline 3" });

            o1.Children.Add(new OutlineViewModel() { Text = "Outline 1.1" });
            o1.Children.Add(new OutlineViewModel() { Text = "Outline 1.2" });
            o1.Children.Add(new OutlineViewModel() { Text = "Outline 1.3" });
        }
    }
}
