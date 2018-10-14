using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace FoxBaseUi.Interface
{
    public interface IDialogControl
    {
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="CloseCallBack"></param>
        void Show(Action<bool> CloseCallBack);

        /// <summary>
        /// 获取对话框内容控件
        /// </summary>
        /// <returns></returns>
        Control GetView();

        /// <summary>
        /// 对话框容器接收对话框关闭消息
        /// </summary>
        event Action RaiseClosed;

        /// <summary>
        ///  对话框容器关闭对话框
        /// </summary>
        void CloseDialog();

    }
}
