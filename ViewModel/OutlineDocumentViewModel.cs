using Outliner.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.ViewModel
{
    public class OutlineDocumentViewModel : ObservableObject
    {
        private ObservableCollection<OutlineViewModel> _outlines;

        public ObservableCollection<OutlineViewModel> Outlines
        {
            get { return _outlines;  }
            set { Set(ref _outlines, value); }
        }

        public OutlineDocumentViewModel()
        {
            _outlines = new ObservableCollection<OutlineViewModel>();
        }
    }
}
