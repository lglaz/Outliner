using Newtonsoft.Json;
using Outliner.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.Model
{
    public static class OutlinerWriter
    {
        public static void Write(OutlineDocumentViewModel outline, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(outline));
            outline.SavePath(path);
        }
    }
}
