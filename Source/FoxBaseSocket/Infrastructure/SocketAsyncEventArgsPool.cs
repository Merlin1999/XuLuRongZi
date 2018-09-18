using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace FoxBaseSocket.Infrastructure
{
    /// <summary>
    /// socket异步事件参数实体池 
    /// </summary>
    internal sealed class SocketAsyncEventArgsPool
    {
        /// <summary>
        /// Pool of SocketAsyncEventArgs.
        /// </summary>
        readonly Stack<SocketAsyncEventArgs> _argsPool;

        /// <summary>
        /// 初始化实体池
        /// </summary>
        /// <param name="capacity">实体池的最大容量</param>
        internal SocketAsyncEventArgsPool(Int32 capacity)
        {
            _argsPool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        /// <summary>
        /// 从参数实体池中移除一个
        /// </summary>
        /// <returns>移除的那个参数实体s</returns>
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
        /// 往参数实体池中添加一个实体
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
        /// 参数实体池已有实体的数量
        /// </summary>
        public int Count
        {
            get { return _argsPool.Count; }
        }
    }
}
