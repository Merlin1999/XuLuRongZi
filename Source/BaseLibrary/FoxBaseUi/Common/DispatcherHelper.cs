using System;
using System.Windows.Threading;

namespace FoxBaseUi.Common
{
    public static class DispatcherHelper
    {
        public static Dispatcher UiDispatcher { get; private set; }

        public static void CheckBeginInvokeonUi(Action action)
        {
            if (action == null)
            {
                return;
            }
            if (UiDispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                UiDispatcher.BeginInvoke(action);
            }
        }

        public static void Initialize()
        {
            if (UiDispatcher != null)
            {
                return;
            }
#if SILVERLIGHT
            UIDispatcher = Deployment.Current.Dispather;
#else
            UiDispatcher = Dispatcher.CurrentDispatcher;
#endif
        }

    }
}
