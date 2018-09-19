using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Comm
{
  public  class EntityesContext
    {
        /// <summary>
        /// 实例化Entities对象
        /// </summary>
        /// <returns>返回Entities对象</returns>
        public static T Instance<T>()
        {
            try
            {
                T t = (T)Activator.CreateInstance(typeof(T));

                return t;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 实例化Entities对象
        /// </summary>
        /// <typeparam name="ConnKey">连接串键</typeparam>
        /// <returns>返回Entities对象</returns>
        public static T Instance<T>(string ConnKey)
        {
            try
            {
                string Conn = "";

                T t = (T)Activator.CreateInstance(typeof(T), new object[] { Conn });

                return t;
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// 实例化Entities对象
        /// </summary>
        /// <typeparam name="Conn">连接串</typeparam>
        /// <returns>返回Entities对象</returns>
        public static T Instance<T>(global::System.Data.EntityClient.EntityConnection Conn)
        {
            try
            {
                T t = (T)Activator.CreateInstance(typeof(T), new object[] { Conn });

                return t;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t">Instance of Entity Data Model Class</param>
        /// <param name="v">新增对象</param>
        public static bool Insert<T, V>(T t, V v)
        {
            Type t_Type = t.GetType();

            Type v_Type = v.GetType();

            bool flag = false;

            try
            {
                //AddObject             
                MethodInfo oMethod = t_Type.GetMethod("AddObject");

                oMethod.Invoke(t, new object[] { v_Type.Name, v });

                //SaveChanges
                oMethod = t_Type.GetMethod("SaveChanges", new Type[] { });

                oMethod.Invoke(t, new object[] { });

                flag = true;
            }
            catch (Exception err)
            {
                flag = false;

                throw err;
            }
            return flag;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t">Instance of Entity Data Model Class</param>
        /// <param name="v">新增对象</param>
        public static bool Insert<T, V>(V v)
        {

            T t = (T)Activator.CreateInstance(typeof(T));

            Type t_Type = t.GetType();

            Type v_Type = v.GetType();

            bool flag = false;

            try
            {
                //AddObject
                //string StrMethod = string.Format("AddTo{0}", v_Type.Name); 

                MethodInfo oMethod = t_Type.GetMethod("AddObject");

                oMethod.Invoke(t, new object[] { v_Type.Name, v });

                //MethodInfo oMethod = t_Type.GetMethod(StrMethod);

                //SaveChanges
                oMethod = t_Type.GetMethod("SaveChanges", new Type[] { });

                oMethod.Invoke(t, new object[] { });

                flag = true;
            }
            catch (Exception err)
            {
                flag = false;

                throw err;
            }
            return flag;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="t">Instance of Entity Data Model Class</param>
        /// <param name="vlist">新增对象集合</param>
        public static bool Insert<T, V>(T t, List<V> vlist)
        {

            Type t_Type = t.GetType();

            Type v_Type = typeof(V);

            bool flag = false;

            try
            {
                //AddObject     
                MethodInfo oMethod = t_Type.GetMethod("AddObject");

                foreach (V v in vlist)
                {
                    oMethod.Invoke(t, new object[] { v_Type.Name, v });
                }

                //SaveChanges
                oMethod = t_Type.GetMethod("SaveChanges", new Type[] { });

                oMethod.Invoke(t, new object[] { });

                flag = true;
            }
            catch (Exception err)
            {
                flag = false;

                throw err;
            }
            return flag;
        }

        /// <summary>
        /// 新增[事务]
        /// </summary>
        /// <param name="t">Instance of Entity Data Model Class</param>
        /// <param name="vlist">新增对象集合</param>
        public static bool InsertByTran<T, V>(List<V> vlist, DbTransaction Tran)
        {

            T t = (T)Activator.CreateInstance(typeof(T));

            Type t_Type = t.GetType();

            Type v_Type = typeof(V);

            bool flag = false;

            try
            {
                //AddObject
                MethodInfo oMethod = t_Type.GetMethod("AddObject");

                foreach (V v in vlist)
                {
                    oMethod.Invoke(t, new object[] { v_Type.Name, v });
                }

                //SaveChanges
                oMethod = t_Type.GetMethod("SaveChanges", new Type[] { });

                oMethod.Invoke(t, new object[] { });

                Tran.Commit();

                flag = true;
            }
            catch (Exception err)
            {
                flag = false;

                throw err;
            }
            return flag;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t">Instance of Entity Data Model Class</param>
        /// <param name="v">修改对象</param>
        public static bool Update<T, V>(T t, IList<V> vList) where T : ObjectContext
        {
            Type t_Type = t.GetType();


            if (vList.Count == 0)
            {
                return false;
            }
            Type v_Type = vList[0].GetType();


            bool flag = false;
            try
            {
                foreach (V v in vList)
                {
                    //GetObjectByKey
                    EntityKey oEntityKey = (EntityKey)v_Type.GetProperty("EntityKey").GetValue(v, null);

                    MethodInfo oMethod = t_Type.GetMethod("GetObjectByKey", new Type[] { oEntityKey.GetType() });

                    oMethod.Invoke(t, new object[] { oEntityKey });


                    //ApplyPropertyChanges
                    oMethod = t_Type.GetMethod("ApplyPropertyChanges");

                    oMethod.Invoke(t, new object[] { v_Type.Name, v });

                    //SaveChanges
                    oMethod = t_Type.GetMethod("SaveChanges", new Type[] { });

                    oMethod.Invoke(t, new object[] { });

                    flag = true;
                }
            }
            catch (Exception err)
            {
                flag = false;
                throw err;
            }
            return flag;
        }

        /// <summary>
        /// 修改[事务]
        /// </summary>
        /// <param name="t">Instance of Entity Data Model Class</param>
        /// <param name="v">修改对象</param>
        public static bool Update<T, V>(T ot, V v) where T : ObjectContext
        {

            T t = ot;
            t.MetadataWorkspace.LoadFromAssembly(ot.GetType().Assembly);
            Type t_Type = ot.GetType();

            Type v_Type = v.GetType();

            bool flag = false;
            try
            {
                //GetObjectByKey
                EntityKey oEntityKey = (EntityKey)v_Type.GetProperty("EntityKey").GetValue(v, null);

                MethodInfo oMethod = t_Type.GetMethod("GetObjectByKey", new Type[] { oEntityKey.GetType() });

                oMethod.Invoke(t, new object[] { oEntityKey });


                //ApplyPropertyChanges
                oMethod = t_Type.GetMethod("ApplyPropertyChanges");

                oMethod.Invoke(t, new object[] { v_Type.Name, v });

                //SaveChanges
                oMethod = t_Type.GetMethod("SaveChanges", new Type[] { });

                oMethod.Invoke(t, new object[] { });

                flag = true;
            }
            catch (Exception err)
            {
                flag = false;
                throw err;
            }
            return flag;
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t">Instance of Entity Data Model Class</param>
        /// <param name="v">删除对象</param>
        /// <returns></returns>
        public static bool Delete<T, V>(T ot, V v) where T : ObjectContext
        {
            T t = ot;
            t.MetadataWorkspace.LoadFromAssembly(ot.GetType().Assembly);
            Type t_Type = t.GetType();
            Type v_Type = v.GetType();

            bool flag = false;
            try
            {
                //GetObjectByKey
                EntityKey oEntityKey = (EntityKey)v_Type.GetProperty("EntityKey").GetValue(v, null);

                MethodInfo oMethod1 = t_Type.GetMethod("GetObjectByKey", new Type[] { oEntityKey.GetType() });

                var v0 = oMethod1.Invoke(t, new object[] { oEntityKey });

                //DeleteObject
                MethodInfo oMethod = t_Type.GetMethod("DeleteObject");

                oMethod.Invoke(t, new object[] { v0 });

                //SaveChanges
                oMethod = t_Type.GetMethod("SaveChanges", new Type[] { });

                oMethod.Invoke(t, new object[] { });

                flag = true;
            }
            catch (Exception err)
            {
                flag = false;

                throw err;
            }
            return flag;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t">Instance of Entity Data Model Class</param>
        /// <param name="v">删除对象</param>
        /// <returns></returns>
        public static bool Delete<T, V>(T t, List<V> vList)
        {

            Type t_Type = t.GetType();

            bool flag = false;
            try
            {
                //DeleteObject
                MethodInfo oMethod = t_Type.GetMethod("DeleteObject");

                foreach (V v in vList)
                {
                    oMethod.Invoke(t, new object[] { v });
                }

                //SaveChanges
                oMethod = t_Type.GetMethod("SaveChanges", new Type[] { });

                oMethod.Invoke(t, new object[] { });

                flag = true;
            }
            catch (Exception err)
            {
                flag = false;

                throw err;
            }
            return flag;
        }


        /// <summary>
        /// 保存[新增/修改]表单
        /// </summary>
        /// <typeparam name="T">Entity Data Model Class</typeparam>
        /// <typeparam name="V">Entity class</typeparam>
        /// <param name="t">Instance of Entity Data Model Class</param>
        /// <param name="v">Instance of Entity class</param>
        /// <param name="priKey">主键</param>
        /// <param name="dict">表单键值映射Dictionary</param>
        /// <returns></returns>
        public static bool SaveForm<T, V>(T t, V v, string priKey, Dictionary<string, string> dict) where T : ObjectContext
        {

            Type v_Type = v.GetType();

            bool flag = false;
            try
            {
                foreach (PropertyInfo pi in v_Type.GetProperties())
                {
                    string key = pi.Name;

                    //if (!dict.ContainsKey(key) || key.ToUpper() == priKey.ToUpper()) continue;
                    if (key.ToUpper() == priKey.ToUpper())
                    {
                        continue;
                    }
                    if (!dict.ContainsKey(key))
                    {
                        pi.SetValue(v, null, null);
                        continue;
                    }

                    if (pi.PropertyType.FullName.Contains(typeof(int).FullName))
                    {
                        if (string.IsNullOrWhiteSpace(dict[key]))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, int.Parse(dict[key]), null);
                        }
                    }
                    else if (pi.PropertyType.FullName.Contains(typeof(DateTime).FullName))
                    {
                        if (string.IsNullOrWhiteSpace(dict[key]))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, DateTime.Parse(dict[key]), null);
                        }
                    }
                    else if (pi.PropertyType.FullName.Contains(typeof(bool).FullName))
                    {
                        if (string.IsNullOrWhiteSpace(dict[key]))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, bool.Parse(dict[key]), null);
                        }
                    }
                    else if (pi.PropertyType.FullName.Contains(typeof(decimal).FullName))
                    {
                        if (string.IsNullOrWhiteSpace(dict[key]))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, decimal.Parse(dict[key]), null);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(dict[key]))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, dict[key], null);
                        }
                    }
                }

                int id = (int)v_Type.GetProperty(priKey).GetValue(v, null);

                if (id > 0)
                {
                    Update(t, v);
                }
                else
                {
                    Insert(t, v);
                }

                flag = true;
            }
            catch (Exception err)
            {
                flag = false;

                throw err;
            }
            return flag;
        }

        /// <summary>
        /// 获取表单对象
        /// </summary>
        /// <typeparam name="V">Entity class</typeparam>
        /// <param name="v">Instance of Entity class</param>
        /// <returns></returns>
        public static V GetObjByForm<V, F>(V v, F f, string priKey)
        {

            //V v = (V)Activator.CreateInstance(typeof(V));

            Type v_Type = v.GetType();

            Type f_Type = f.GetType();

            MethodInfo oMethod = f_Type.GetMethod("Get", new Type[] { typeof(string) });

            string[] FormKeys = (string[])f_Type.GetProperty("AllKeys").GetValue(f, null);

            try
            {
                foreach (string key in FormKeys)
                {
                    PropertyInfo pi = v_Type.GetProperty(key);

                    if (pi == null)
                    {
                        continue;
                    }
                    if (key.ToUpper() == priKey.ToUpper())
                    {
                        continue;
                    }
                    string value = (string)oMethod.Invoke(f, new object[] { key });

                    if (pi.PropertyType.FullName.Contains(typeof(int).FullName))
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, int.Parse(value), null);
                        }
                    }
                    else if (pi.PropertyType.FullName.Contains(typeof(DateTime).FullName))
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, DateTime.Parse(value), null);
                        }
                    }
                    else if (pi.PropertyType.FullName.Contains(typeof(bool).FullName))
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, bool.Parse(value), null);
                        }
                    }
                    else if (pi.PropertyType.FullName.Contains(typeof(decimal).FullName))
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, decimal.Parse(value), null);
                        }
                    }
                    else if (pi.PropertyType.FullName.Contains(typeof(long).FullName))
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, long.Parse(value), null);
                        }
                    }
                    else if (pi.PropertyType.FullName.Contains(typeof(Guid).FullName))
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, new Guid(value), null);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            pi.SetValue(v, null, null);
                        }
                        else
                        {
                            pi.SetValue(v, value, null);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return v;
        }

        /// <summary>
        /// 保存[新增/修改]表单
        /// </summary>
        /// <typeparam name="T">Entity Data Model Class</typeparam>
        /// <typeparam name="V">Entity class</typeparam>
        /// <param name="t">Instance of Entity Data Model Class</param>
        /// <param name="v">Instance of Entity class</param>
        /// <param name="priKey">主键</param>
        /// <param name="dict">表单键值映射Dictionary</param>
        /// <returns></returns>
        public static bool SaveForm<T, V, F>(T t, V v, string priKey, F f) where T : ObjectContext
        {
            bool OptFlag = false;

            Type v_Type = v.GetType();

            Type f_Type = f.GetType();

            MethodInfo oMethod = f_Type.GetMethod("Get", new Type[] { typeof(string) });

            string DataId = (string)oMethod.Invoke(f, new object[] { priKey });

            string id = "0";

            if (DataId != null)
            {
                id = ((DataId ?? "0") == "" ? "0" : DataId);
            }
            string[] form = (string[])f_Type.GetProperty("AllKeys").GetValue(f, null);

            try
            {

                v = GetObjByForm<V, F>(v, f, priKey);

                if (id != "0")
                {
                    //PropertyInfo pi = v_Type.GetProperty(priKey);
                    //string value = (string)oMethod.Invoke(f, new object[] { priKey });
                    //pi.SetValue(v, int.Parse(value), null);
                    Update(t, v);
                }
                else
                {
                    Insert(t, v);
                }

                OptFlag = true;
            }
            catch (Exception err)
            {
                OptFlag = false;

                throw err;
            }

            return OptFlag;
        }

        /// <summary>
        /// 保存[新增/修改]表单
        /// </summary>
        /// <typeparam name="T">Entity Data Model Class</typeparam>
        /// /// <typeparam name="StoreProName">存储过程在Entity Data Model Class中的实例名[格式：HSWebWFP_PortalEntities.Pro_Test12]</typeparam>
        /// <param name="t">Instance of Entity Data Model Class</param>
        /// <param name="dict">参数键值映射Dictionary</param>
        /// <returns></returns>
        public static string ExecStorePro<T>(T t, string StoreProName, Dictionary<string, string> dict)
        {
            Type t_Type = t.GetType();

            DbConnection EntityConn = (DbConnection)t_Type.GetProperty("Connection").GetValue(t, null);

            try
            {
                EntityConn.Open();

                //var tran = EntityConn.BeginTransaction();

                //EntityCommand EntityCmd = new EntityCommand(StoreProName, EntityConn as EntityConnection, tran as EntityTransaction);

                EntityCommand EntityCmd = new EntityCommand(StoreProName, EntityConn as EntityConnection);

                EntityCmd.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, string> item in dict)
                {
                    EntityParameter EntityPram = new EntityParameter();

                    EntityPram.ParameterName = item.Key;

                    EntityPram.Value = item.Value;

                    EntityCmd.Parameters.Add(EntityPram);
                }

                return EntityCmd.ExecuteScalar().ToString();

                //tran.Commit();

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                EntityConn.Close();
            }

        }

        /// <summary>
        /// 带事务提交
        /// </summary>
        /// <param name="t">Entity instance</param>
        /// <returns></returns>
        public static void SaveWithTrans<T>(T t)
        {
            Type t_Type = t.GetType();

            DbConnection Conn = (DbConnection)t_Type.GetProperty("Connection").GetValue(t, null);

            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }
            DbTransaction Tran = Conn.BeginTransaction();

            try
            {
                //SaveChanges
                MethodInfo oMethod = t_Type.GetMethod("SaveChanges", new Type[] { });

                oMethod.Invoke(t, new object[] { });

                Tran.Commit();
            }
            catch (Exception err)
            {
                Tran.Rollback();
                throw err;
            }
            finally
            {
                Conn.Close();

                MethodInfo oMethod = t_Type.GetMethod("Dispose", new Type[] { });

                oMethod.Invoke(t, new object[] { });
            }
        }
        /// <summary>
        /// 执行sql语句返回数据集合
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public static DataTable Query(string sqlText)
        {
            DataTable dt = new DataTable();
            using (EntityCommand command = new EntityCommand())
            {
                command.Connection = new EntityConnection("");
                command.CommandText = sqlText;
                command.Connection.Open();
                EntityDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                try
                {    ///动态添加表的数据列
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        DataColumn myDataColumn = new DataColumn();
                        myDataColumn.DataType = reader.GetFieldType(i);
                        myDataColumn.ColumnName = reader.GetName(i);
                        dt.Columns.Add(myDataColumn);
                    }

                    ///添加表的数据
                    while (reader.Read())
                    {
                        DataRow myDataRow = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            myDataRow[i] = reader[i].ToString();
                        }
                        dt.Rows.Add(myDataRow);
                        myDataRow = null;
                    }
                }
                catch (Exception ex)
                {
                    ///抛出类型转换错误
                    //SystemError.CreateErrorLog(ex.Message);
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    ///关闭数据读取器
                    reader.Close();
                    command.Connection.Close();
                }


            }
            return dt;
        }
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="commandtype"></param>
        /// <returns></returns>
        public static DataTable Query(string sqlText, CommandType commandtype)
        {
            DataTable dt = new DataTable();
            using (EntityCommand command = new EntityCommand())
            {
                command.Connection = new EntityConnection("");
                command.CommandText = sqlText;
                command.CommandType = commandtype;
                command.Connection.Open();
                EntityDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                try
                {    ///动态添加表的数据列
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        DataColumn myDataColumn = new DataColumn();
                        myDataColumn.DataType = reader.GetFieldType(i);
                        myDataColumn.ColumnName = reader.GetName(i);
                        dt.Columns.Add(myDataColumn);
                    }

                    ///添加表的数据
                    while (reader.Read())
                    {
                        DataRow myDataRow = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            myDataRow[i] = reader[i].ToString();
                        }
                        dt.Rows.Add(myDataRow);
                        myDataRow = null;
                    }
                }
                catch (Exception ex)
                {
                    ///抛出类型转换错误
                    //SystemError.CreateErrorLog(ex.Message);
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    ///关闭数据读取器
                    reader.Close();
                    command.Connection.Close();
                }


            }
            return dt;
        }
        /// <summary>
        /// 带参数查询
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="parames"></param>
        /// <returns></returns>
        public static DataTable Query(string sqlText, EntityParameter[] parames)
        {
            DataTable dt = new DataTable();
            using (EntityCommand command = new EntityCommand())
            {
                command.Connection = new EntityConnection("");
                command.CommandText = sqlText;
                if (parames != null && parames.Length > 0)
                {
                    command.Parameters.AddRange(parames);
                }
                command.Connection.Open();
                EntityDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                try
                {    ///动态添加表的数据列
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        DataColumn myDataColumn = new DataColumn();
                        myDataColumn.DataType = reader.GetFieldType(i);
                        myDataColumn.ColumnName = reader.GetName(i);
                        dt.Columns.Add(myDataColumn);
                    }

                    ///添加表的数据
                    while (reader.Read())
                    {
                        DataRow myDataRow = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            myDataRow[i] = reader[i].ToString();
                        }
                        dt.Rows.Add(myDataRow);
                        myDataRow = null;
                    }
                }
                catch (Exception ex)
                {
                    ///抛出类型转换错误
                    //SystemError.CreateErrorLog(ex.Message);
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    ///关闭数据读取器
                    reader.Close();
                    command.Connection.Close();
                }


            }
            return dt;
        }
        /// <summary>
        /// 带参数查询
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="parames"></param>
        /// <param name="commandtype"></param>
        /// <returns></returns>
        public static DataTable Query(string sqlText, EntityParameter[] parames, CommandType commandtype)
        {
            DataTable dt = new DataTable();
            using (EntityCommand command = new EntityCommand())
            {
                command.Connection = new EntityConnection("");
                command.CommandText = sqlText;
                command.CommandType = commandtype;
                if (parames != null && parames.Length > 0)
                {
                    command.Parameters.AddRange(parames);
                }
                command.Connection.Open();
                EntityDataReader reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                try
                {    ///动态添加表的数据列
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        DataColumn myDataColumn = new DataColumn();
                        myDataColumn.DataType = reader.GetFieldType(i);
                        myDataColumn.ColumnName = reader.GetName(i);
                        dt.Columns.Add(myDataColumn);
                    }

                    ///添加表的数据
                    while (reader.Read())
                    {
                        DataRow myDataRow = dt.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            myDataRow[i] = reader[i].ToString();
                        }
                        dt.Rows.Add(myDataRow);
                        myDataRow = null;
                    }
                }
                catch (Exception ex)
                {
                    ///抛出类型转换错误
                    //SystemError.CreateErrorLog(ex.Message);
                    throw new Exception(ex.Message, ex);
                }
                finally
                {
                    ///关闭数据读取器
                    reader.Close();
                    command.Connection.Close();
                }


            }
            return dt;
        }
    }
}
