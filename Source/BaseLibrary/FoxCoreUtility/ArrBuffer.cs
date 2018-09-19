using System;

namespace FoxCoreUpdateServer
{
    /// <summary>
    /// 环形缓存
    /// </summary>
    public class ArrBuffer
    {
        private readonly int _bufferSize;
        private int _wIndex = 0;
        private int _rIndex = 0;
        private readonly byte[] _buffer;

        /// <summary>
        /// 获取缓存可用大小
        /// </summary>
        /// <returns></returns>
        public int GetNumsBetweenWAndRIndex()
        {
            if (_wIndex < _rIndex)
            {
                return _bufferSize - _rIndex + _wIndex;
            }
            if (_wIndex > _rIndex)
            {
                return _wIndex - _rIndex;
            }
            return 0;
        }

        /// <summary>
        /// 构造函数，此类不同步读写
        /// </summary>
        /// <param name="bufSize">缓存大小</param>
        public ArrBuffer(int bufSize)
        {
            _bufferSize = bufSize;
            _buffer = new byte[_bufferSize];
        }

        /// <summary>
        /// 读指针
        /// </summary>
        public int RIndex
        {
            get { return _rIndex; }
        }

        /// <summary>
        /// 向缓存中写入数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataLen"></param>
        /// <returns></returns>
        public bool WriteBuffer(byte[] data, int dataLen)
        {
            bool ret = true;
            //Console.WriteLine("写WriteBuffer前：_wIndex={0},_rIndex={1},bufferSize={2}.", _wIndex, _rIndex, bufferSize);
            if (_wIndex >= _rIndex)
            {
                if (_wIndex + dataLen <= _bufferSize - 1)
                {
                    Buffer.BlockCopy(data, 0, _buffer, _wIndex, dataLen);
                    _wIndex += dataLen;
                }
                else if (_wIndex + dataLen > _bufferSize - 1 && _wIndex + dataLen - _bufferSize < _rIndex)
                {
                    Buffer.BlockCopy(data, 0, _buffer, _wIndex, _bufferSize - _wIndex);
                    Buffer.BlockCopy(data, _bufferSize - _wIndex, _buffer, 0, dataLen - (_bufferSize - _wIndex));
                    _wIndex = dataLen - (_bufferSize - _wIndex);
                }
                else if (_wIndex + dataLen > _bufferSize - 1 && _wIndex + dataLen - _bufferSize >= _rIndex)
                {
                    ret = false;
                    Console.WriteLine("写WriteBuffer后：_wIndex={0},_rIndex={1},bufferSize={2}.", _wIndex, _rIndex, _bufferSize);
                }
                else
                {
                    ret = false;
                    Console.WriteLine("写WriteBuffer后：_wIndex={0},_rIndex={1},bufferSize={2}.", _wIndex, _rIndex, _bufferSize);
                }
            }
            else if (_wIndex < _rIndex)
            {
                if (_wIndex + dataLen < _rIndex)
                {
                    Buffer.BlockCopy(data, 0, _buffer, _wIndex, dataLen);
                    _wIndex += dataLen;
                }
            }
            else //wIndex = rIndex
            {
                ret = false;
                Console.WriteLine("写WriteBuffer后：_wIndex={0},_rIndex={1},bufferSize={2}.", _wIndex, _rIndex, _bufferSize);
            }
            //Console.WriteLine("写WriteBuffer后：_wIndex={0},_rIndex={1},bufferSize={2}.", _wIndex, _rIndex, bufferSize);
            return ret;
        }

        /// <summary>
        /// 读一个byte(不pop)
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ReadByte(int offset, out byte data) //读取数据，rIndex不变化
        {
            data = 0;
            if (_rIndex == _wIndex) //只准写不准读
            {
                return false;
            }
            else if (_rIndex < _wIndex)
            {
                if (_rIndex + offset < _wIndex)
                {
                    data = _buffer[_rIndex + offset];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else // if(rIndex>wIndex)
            {
                if (_rIndex + offset <= _bufferSize - 1)
                {
                    data = _buffer[_rIndex + offset];
                    return true;
                }
                else if (_rIndex + offset > _bufferSize - 1 && _rIndex + offset - _bufferSize < _wIndex)
                {
                    data = _buffer[_rIndex + offset - _bufferSize];
                    return true;

                }
                else //>=wIndex
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 推出数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataLen"></param>
        /// <returns></returns>
        public bool PopBytes(out byte[] data, ushort dataLen)
        {
            //data = null;
            data = new byte[dataLen];
            if (_rIndex == _wIndex) //只准写，不准读
            {
                return false;
            }
            else if (_rIndex > _wIndex)
            {
                if (_rIndex + dataLen - 1 <= _bufferSize - 1)
                {
                    Buffer.BlockCopy(_buffer, _rIndex, data, 0, dataLen);
                    _rIndex += dataLen;
                    if (_rIndex > _bufferSize)
                    {
                        _rIndex = 0;
                    }
                    return true;
                }
                else if (_rIndex + dataLen > _bufferSize && _rIndex + dataLen - _bufferSize - 1 < _wIndex)
                {
                    Buffer.BlockCopy(_buffer, _rIndex, data, 0, _bufferSize - _rIndex);
                    Buffer.BlockCopy(_buffer, 0, data, _bufferSize - _rIndex, dataLen - (_bufferSize - _rIndex));
                    _rIndex = dataLen + _rIndex - _bufferSize;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else //rIndex<wIndex
            {
                if (_rIndex + dataLen <= _wIndex)
                {
                    Buffer.BlockCopy(_buffer, _rIndex, data, 0, dataLen);
                    _rIndex += dataLen;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
