using System;

namespace KT.UpdateServer.DB.Dto.Base
{
    /// <summary>
    ///     数据传输对象抽象类
    /// </summary>
    public abstract class Dto
    {
        public string Id { set; get; }
    }
}