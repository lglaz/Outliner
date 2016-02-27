using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.Util
{
    public interface IFileDialogService
    {
        string GetOpenFilePath(string filter = "", string initialDirectory = "");
        string GetSaveFilePath(string defaultExtension = "", string filter = "", string initialDirectory = "");
    }
}
