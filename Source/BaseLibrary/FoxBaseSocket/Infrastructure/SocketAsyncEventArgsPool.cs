using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace FoxBaseSocket.Infrastructure
{
    /// <summary>
    /// socket�첽�¼�����ʵ��� 
    /// </summary>
    internal sealed class SocketAsyncEventArgsPool
    {
        /// <summary>
        /// Pool of SocketAsyncEventArgs.
        /// </summary>
        readonly Stack<SocketAsyncEventArgs> _argsPool;

        /// <summary>
        /// ��ʼ��ʵ���
        /// </summary>
        /// <param name="capacity">ʵ��ص��������</param>
        internal SocketAsyncEventArgsPool(Int32 capacity)
        {
            _argsPool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        /// <summary>
        /// �Ӳ���ʵ������Ƴ�һ��
        /// </summary>
        /// <returns>�Ƴ����Ǹ�����ʵ��s</returns>
        internal SocketAsyncEventArgs Pop()
        {
            lock (this._argsPool)
            {
                if (this._argsPool.Count > 0)
                {
                    return this._argsPool.Pop();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// ������ʵ��������һ��ʵ��
        /// </summary>
        /// <param name="item"></param>
        internal void Push(SocketAsyncEventArgs item)
        {
            if (item == null) 
            { 
                throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null"); 
            }
            lock (this._argsPool)
            {
                this._argsPool.Push(item);
            }
        }
        /// <summary>
        /// ����ʵ�������ʵ�������
        /// </summary>
        public int Count
        {
            get { return _argsPool.Count; }
        }
    }
}
