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
    public static class OutlinerReader
    {
        public static OutlineDocumentViewModel Read(string path)
        {
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                OutlineDocumentViewModel doc = (OutlineDocumentViewModel)serializer.Deserialize(file, typeof(OutlineDocumentViewModel));
                doc.RefreshParentalReferences();
                return doc;
            }

            
        }
    }
}
