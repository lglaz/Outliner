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
            var o1 = _document.Add("Outline 1");
            _document.Add("Outline 2");
            _document.Add("Outline 3");

            o1.Add("Outline 1.1");
            o1.Add("Outline 1.2");
            o1.Add("Outline 1.3");
        }
    }
}
