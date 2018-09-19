//**********************************************
// 文 件 名：IProduct
// 命名空间：Csrd.MonitorStation.Communication.MsgQuene
// 内    容：
// 功    能：
// 文件关系：
// 作    者：胡明灿
// 小    组：研发中心
// 生成日期：2016/3/14 11:48:07
// 版 本 号：V1.0.0.0
// 修改日志：
// 版权说明：
//**********************************************

namespace KT.Utility.MsgQueue
{
    /// <summary>
    /// 消息生产者
    /// </summary>
    public interface IMsgProduct<T>
    {
        void PushToMsgQueue(T msg);
    }
}
