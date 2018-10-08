using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using XinLuControlContract;
using XinLuControlContract.Entity;
using XinLuSystemManager.Views;

namespace XinLuSystemManager
{
    public class SysMag : IUiModel<Control>
    {
        Dictionary<string, Control> _subModelDic;

        ModelInfo _modelInfo;

        public SysMag()
        {
            _modelInfo = new ModelInfo()
            {
                ModelName="系统管理",
                ModelIntroduction="包括用户管理，登录日志以及系统设置",
                SubModelNames=new List<string>(),
                Icon= new BitmapImage(new Uri( @"pack://siteoforigin:,,,/Resources/xitong.png")),
            };
            _subModelDic = new Dictionary<string, Control>();
            _modelInfo.SubModelNames.Add("用户管理");
            _subModelDic.Add("用户管理", new UserMagView());
        }

        public ModelInfo GetModelInfo()
        {
            return _modelInfo;
        }

        public Control GetSubModelByName(string name)
        {
            return _subModelDic[name];
        }

        public void InitModel(object args)
        {
            
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~SysMag() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
