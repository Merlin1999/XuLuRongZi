using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinLuSystemManager
{
    internal class StaticGlobal
    {
        /// <summary>
        /// 通知外部模块的事件
        /// </summary>
        public static XinLuControlContract.DllEventHandler ExecuteDllEvent;

        /// <summary>
        /// 执行通知外部模块的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void RunExecuteDllEvent(object sender, XinLuControlContract.Entity.XinLuEventArgs args)
        {
            if (ExecuteDllEvent != null)
                ExecuteDllEvent.Invoke(sender, args);
        }
    }
}
