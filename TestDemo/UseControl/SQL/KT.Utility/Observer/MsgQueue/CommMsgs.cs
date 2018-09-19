//**********************************************
// 文 件 名：CommMsgs
// 命名空间：Csrd.MonitorStation.Communication.MsgQuene
// 内    容：
// 功    能：
// 文件关系：
// 作    者：胡明灿
// 小    组：研发中心
// 生成日期：2016/3/14 11:39:32
// 版 本 号：V1.0.0.0
// 修改日志：
// 版权说明：
//**********************************************

using System.Collections.Generic;
using System.IO;
using KT.Utility.Files.Serializer;

namespace KT.Utility.MsgQueue
{
    internal class CommMsgs<T> : IMsgConsumer<T>, IMsgProduct<T>
    {
        private readonly Queue<T> _absDataQueue;
        private readonly object _absDataLock = new object();
        private readonly SerializerXml<Queue<T>> _serializer;
        private readonly string _pathFile;

        public CommMsgs()
        {
            this._pathFile = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "MsgQueue_"+typeof(T).FullName+".temp";
            this._serializer = new SerializerXml<Queue<T>>();
            this._absDataQueue = new Queue<T>();
            if (File.Exists(this._pathFile))
            {
                var temp = this._serializer.Load(this._pathFile);
                File.Delete(this._pathFile);
                this._absDataQueue = temp ?? new Queue<T>();
            }
            else
            {
                this._absDataQueue = new Queue<T>();
            }           
        }

        ~CommMsgs()
        {
            this._serializer.Store(this._pathFile, this._absDataQueue);
        }

        public bool PullFromMsgQueue(ref T absData)
        {
            if (this._absDataQueue.Count <= 0)
            {
                return false;
            }
            lock (_absDataLock)
            {
                if (this._absDataQueue.Count > 0)
                {
                    absData = this._absDataQueue.Dequeue();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void PushToMsgQueue(T msg)
        {
            lock (_absDataLock)
            {
                this._absDataQueue.Enqueue(msg);
                if (this._absDataQueue.Count > 20)
                    this._absDataQueue.Dequeue();
            }
        }
    }
}
