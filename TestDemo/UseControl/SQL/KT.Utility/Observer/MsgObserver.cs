
namespace KT.Utility.Observer
{
    public class MsgObserver<T>
    {
        private static readonly MsgObserver<T> Observer = new MsgObserver<T>();

        private event ObsRecvDataEvent<T> ObserverEvents;
        private MsgObserver(){}
        public static void PushMsgToObserver(T dataPack)
        {
            if (Observer.ObserverEvents != null)
                Observer.ObserverEvents.Invoke(dataPack);
        }

        public static void RegisterObserver(ObsRecvDataEvent<T> observer)
        {
            if (observer != null)
                Observer.ObserverEvents += observer;
        }

        public static void LogoutObserver(ObsRecvDataEvent<T> observer)
        {
            try
            {
                if (observer != null)
                    Observer.ObserverEvents -= observer;
            }
            catch (System.Exception ex)
            {
            	
            }

        }
    }
}
