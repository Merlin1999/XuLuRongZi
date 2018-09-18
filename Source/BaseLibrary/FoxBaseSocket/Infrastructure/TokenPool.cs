using System;
using System.Collections;
using System.Collections.Generic;

namespace FoxBaseSocket.Infrastructure
{
    /// <summary>
    /// 保存Token的字典  
    /// </summary>
    internal sealed class TokenDictionary
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDictionary<string, Token> _tokens;

        /// <summary>
        /// Initializes the object Dictionary to the specified size.
        /// </summary>
        /// <param name="capacity">Maximum number the Dictionary can hold.</param>
        internal TokenDictionary(Int32 capacity)
        {
            _tokens = new Dictionary<string, Token>(capacity);
        }

        internal IEnumerable<KeyValuePair<string, Token>> GetEnumerable()
        {
            return _tokens;
        }

        /// <summary>
        /// Add a Token to the pool.
        /// </summary>
        /// <param name="ipAndPort">EndPoint.ToString()作为Key</param>
        /// <param name="t"></param>
        internal void Add(string ipAndPort, Token t)
        {
            if (string.IsNullOrEmpty(ipAndPort))
            {
                throw new ArgumentNullException("ipAndPort added to a Token cannot be null");
            }
            lock (this._tokens)
            {
                _tokens[ipAndPort] = t;
            }
        }

        /// <summary>
        /// Removes a Token from the pool. 
        /// </summary>
        /// <param name="ipAndPort"></param>
        internal void Remove(string ipAndPort)
        {
            if (string.IsNullOrEmpty(ipAndPort)) 
            {
                throw new ArgumentNullException("ipAndPort Remove to a Token cannot be null"); 
            }
            lock (this._tokens)
            {
                if (_tokens.ContainsKey(ipAndPort)) _tokens.Remove(ipAndPort);
            }
        }


        internal Token GetToken(string ipAndPort)
        {
            lock (this._tokens)
            {
                if (_tokens.ContainsKey(ipAndPort)) return _tokens[ipAndPort];
                return null;
            }
        }

        internal void CloseAll()
        {
            lock (this._tokens)
            {
                foreach (var s in _tokens.Values)
                {
                    if (s.Connection.Connected)
                    s.Connection.Close();
                }
            }
        }
    }
}
