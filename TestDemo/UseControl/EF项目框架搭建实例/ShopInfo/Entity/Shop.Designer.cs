﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace Entity
{
    #region 上下文
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    public partial class ShopDBEntities : ObjectContext
    {
        #region 构造函数
    
        /// <summary>
        /// 请使用应用程序配置文件的“ShopDBEntities”部分中的连接字符串初始化新 ShopDBEntities 对象。
        /// </summary>
        public ShopDBEntities() : base("name=ShopDBEntities", "ShopDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 ShopDBEntities 对象。
        /// </summary>
        public ShopDBEntities(string connectionString) : base(connectionString, "ShopDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 ShopDBEntities 对象。
        /// </summary>
        public ShopDBEntities(EntityConnection connection) : base(connection, "ShopDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region 分部方法
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet 属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<ShopInfo> ShopInfo
        {
            get
            {
                if ((_ShopInfo == null))
                {
                    _ShopInfo = base.CreateObjectSet<ShopInfo>("ShopInfo");
                }
                return _ShopInfo;
            }
        }
        private ObjectSet<ShopInfo> _ShopInfo;

        #endregion

        #region AddTo 方法
    
        /// <summary>
        /// 用于向 ShopInfo EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddToShopInfo(ShopInfo shopInfo)
        {
            base.AddObject("ShopInfo", shopInfo);
        }

        #endregion

    }

    #endregion

    #region 实体
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ShopDBModel", Name="ShopInfo")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ShopInfo : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 ShopInfo 对象。
        /// </summary>
        /// <param name="id">ID 属性的初始值。</param>
        public static ShopInfo CreateShopInfo(global::System.Int32 id)
        {
            ShopInfo shopInfo = new ShopInfo();
            shopInfo.ID = id;
            return shopInfo;
        }

        #endregion

        #region 简单属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value, "ID");
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, true, "Name");
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> Price
        {
            get
            {
                return _Price;
            }
            set
            {
                OnPriceChanging(value);
                ReportPropertyChanging("Price");
                _Price = StructuralObject.SetValidValue(value, "Price");
                ReportPropertyChanged("Price");
                OnPriceChanged();
            }
        }
        private Nullable<global::System.Decimal> _Price;
        partial void OnPriceChanging(Nullable<global::System.Decimal> value);
        partial void OnPriceChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Adress
        {
            get
            {
                return _Adress;
            }
            set
            {
                OnAdressChanging(value);
                ReportPropertyChanging("Adress");
                _Adress = StructuralObject.SetValidValue(value, true, "Adress");
                ReportPropertyChanged("Adress");
                OnAdressChanged();
            }
        }
        private global::System.String _Adress;
        partial void OnAdressChanging(global::System.String value);
        partial void OnAdressChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Remark
        {
            get
            {
                return _Remark;
            }
            set
            {
                OnRemarkChanging(value);
                ReportPropertyChanging("Remark");
                _Remark = StructuralObject.SetValidValue(value, true, "Remark");
                ReportPropertyChanged("Remark");
                OnRemarkChanged();
            }
        }
        private global::System.String _Remark;
        partial void OnRemarkChanging(global::System.String value);
        partial void OnRemarkChanged();

        #endregion

    }

    #endregion

}
