//**********************************************
// 文 件 名：MsgQueue
// 命名空间：Csrd.MonitorStation.Communication.MsgQuene
// 内    容：
// 功    能：
// 文件关系：
// 作    者：胡明灿
// 小    组：研发中心
// 生成日期：2016/3/14 14:27:21
// 版 本 号：V1.0.0.0
// 修改日志：
// 版权说明：
//**********************************************

namespace FoxCoreUtility.MsgQueue
{
    public class MsgQueueFactory<T>
    {
        private static readonly CommMsgs<T> CommMsgQueue=new CommMsgs<T>();

        private MsgQueueFactory(){}

        public static IMsgProduct<T> GetProduct()
        {
            return CommMsgQueue;
        }

        public static IMsgConsumer<T> GetConsumer()
        {
            return CommMsgQueue;
        }
    }
}
