using System;
using System.Runtime.Serialization;

namespace KT.UpdateServer.Contracts.DbBase
{
    /// <summary>
    /// 调用服务或业务逻辑的返回标记
    /// </summary>
    [DataContract]
    public enum ResultSign
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [EnumMember]
        Successful = 0,
        
        /// <summary>
        /// 警告
        /// </summary>
        [EnumMember]
        Warning = 1,
        
        /// <summary>
        /// 出现错误
        /// </summary>
        [EnumMember]
        Error = 2
    }

    /// <summary>
    /// 调用服务或业务逻辑的操作状态
    /// </summary>
    [DataContract]
    public class OperateStatus
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OperateStatus()
        {
            ResultSign = ResultSign.Successful;
            Message = String.Empty;
        }
        /// <summary>
        /// 从操作状态构造
        /// </summary>
        public OperateStatus(OperateStatus status)
        {
            ResultSign = status.ResultSign;
            Message = status.Message;
            FormatParams = status.FormatParams;
        }

        /// <summary>
        /// 从操作状态复制
        /// </summary>
        /// <param name="status">其他操作状态</param>
        public void CopyFromStatus(OperateStatus status)
        {
            ResultSign = status.ResultSign;
            Message = status.Message;
            FormatParams = status.FormatParams;
        }

        /// <summary>
        /// 获取一个值，表示返回标记是否成功
        /// </summary>
        public bool IsSuccessful
        {
            get { return ResultSign == ResultSign.Successful; }
        }

        /// <summary>
        /// 获取一个值，表示返回标记是否不成功
        /// </summary>
        public bool IsNotSuccessful
        {
            get { return ResultSign != ResultSign.Successful; }
        }

        /// <summary>
        /// 返回标记
        /// </summary>
        [DataMember]
        public ResultSign ResultSign { get; set; }

        /// <summary>
        /// 消息字符串key
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// 消息的参数
        /// </summary>
        [DataMember]
        public string[] FormatParams { get; set; }
    }
}
