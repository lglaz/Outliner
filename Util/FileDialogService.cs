using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.Util
{
    public class FileDialogService : IFileDialogService
    {
        public string GetOpenFilePath(string filter = "", string initialDirectory = "")
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filter;
            dialog.InitialDirectory = initialDirectory;
            if(dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return "";
        }

        public string GetSaveFilePath(string defaultExtension = "", string filter = "", string initialDirectory = "")
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = filter;
            dialog.InitialDirectory = initialDirectory;
            dialog.DefaultExt = defaultExtension;
            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return "";
        }
    }
}
