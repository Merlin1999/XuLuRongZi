using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinLuControlContract.Entity
{
    public class XinLuEventArgs:EventArgs
    {
        public DllEventType EventType { get; set; }
    }

    public enum DllEventType
    {
        ShowDialog,

    }
}
