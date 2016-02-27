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
        private string _path;

        public override bool IsFocusable { get { return false; } }

        public string Path
        {
            get { return _path; }
            private set { Set(ref _path, value); }
        }

        public OutlineDocumentViewModel()
        {     
        }

        internal void SavePath(string path)
        {
            Path = path;
            Text = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
}
