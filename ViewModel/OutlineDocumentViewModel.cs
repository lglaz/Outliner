using Outliner.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.ViewModel
{
    public class OutlineDocumentViewModel : OutlineViewModel
    {
        public override bool IsFocusable { get { return false; } }

        public OutlineDocumentViewModel()
        {     
        }        
    }
}
