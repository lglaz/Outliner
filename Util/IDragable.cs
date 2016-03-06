using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outliner.Util
{
    public interface IDragable
    {
        Type DataType { get; }
        void Remove(object i);       
    }
}
