
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace MySQLiteHelper
{
    public class SQLiteHelper
    {
        #region 字段
        
        /// <summary>
        /// 事务的基类
        /// </summary>
        private DbTransaction DBtrans;
        /// <summary>
        /// 使用静态变量字典解决多线程实例本类，实现一个数据库对应一个clslock
        /// </summary>
        private static readonly Dictionary<string, ClsLock> RWL = new Dictionary<string, ClsLock>();
        /// <summary>
        /// 数据库地址
        /// </summary>
        private readonly string mdataFile;
        /// <summary>
        /// 数据库密码
        /// </summary>
        private readonly string mPassWord;
        private readonly string LockName = null;
        /// <summary>
        /// 数据库连接定义
        /// </summary>
        private SQLiteConnection mConn;

        #endregion

        #region 构造函数

        /// <summary>
        /// 根据数据库地址初始化
        /// </summary>
        /// <param name="dataFile">数据库地址</param>
        public SQLiteHelper(string dataFile)
        {
            this.mdataFile = dataFile ?? throw new ArgumentNullException("dataFile=null");
            this.mdataFile = AppDomain.CurrentDomain.BaseDirectory + dataFile;
            //this.mdataFile = dataFile;
            RWL.Clear();
            if (!RWL.ContainsKey(dataFile))
            {
                LockName = dataFile;
                RWL.Add(dataFile, new ClsLock());
            }

        }

        /// <summary>
        /// 使用密码打开数据库
        /// </summary>
        /// <param name="dataFile">数据库地址</param>
        /// <param name="PassWord">数据库密码</param>
        public SQLiteHelper(string dataFile, string PassWord)
        {
            this.mdataFile = dataFile ?? throw new ArgumentNullException("dataFile is null");
            this.mPassWord = PassWord ?? throw new ArgumentNullException("PassWord is null");
            this.mdataFile = AppDomain.CurrentDomain.BaseDirectory + dataFile;
            if (!RWL.ContainsKey(dataFile))
            {
                LockName = dataFile;
                RWL.Add(dataFile, new ClsLock());
            }
        }

        #endregion

        #region 打开/关闭 数据库

        /// <summary>  
        /// 打开 SQLiteManager 使用的数据库连接  
        /// </summary>  
        public void Open()
        {
            if (string.IsNullOrWhiteSpace(mPassWord))
            {
                mConn = OpenConnection(this.mdataFile);
            }
            else
            {
                mConn = OpenConnection(this.mdataFile, mPassWord);
            }
            Console.WriteLine("打开数据库成功");
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (this.mConn != null)
            {
                try
                {
                    this.mConn.Close();
                    if (RWL.ContainsKey(LockName))
                    {
                        RWL.Remove(LockName);
                    }
                }
                catch
                {
                    Console.WriteLine("关闭失败");
                }
            }
            Console.WriteLine("关闭数据库成功");
        }

        #endregion

        #region 事务

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTrain()
        {
            EnsureConnection();
            DBtrans = mConn.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void DBCommit()
        {
            try
            {
                DBtrans.Commit();
            }
            catch (Exception)
            {
                DBtrans.Rollback();
            }
        }

        #endregion

        #region 工具

        /// <summary>  
        /// 打开一个SQLite数据库文件，如果文件不存在，则创建（无密码）
        /// </summary>  
        /// <param name="dataFile"></param>  
        /// <returns>SQLiteConnection 类</returns>  
        private SQLiteConnection OpenConnection(string dataFile)
        {
            if (dataFile == null)
            {
                throw new ArgumentNullException("dataFiledataFile=null");
            }
            if (!File.Exists(dataFile))
            {
                SQLiteConnection.CreateFile(dataFile);
            }
            SQLiteConnection conn = new SQLiteConnection();
            SQLiteConnectionStringBuilder conStr = new SQLiteConnectionStringBuilder
            {
                DataSource = dataFile
            };
            conn.ConnectionString = conStr.ToString();
            conn.Open();
            return conn;
        }

        /// <summary>  
        /// 打开一个SQLite数据库文件，如果文件不存在，则创建（有密码）
        /// </summary>  
        /// <param name="dataFile"></param>  
        /// <param name="Password"></param>
        /// <returns>SQLiteConnection 类</returns>  
        private SQLiteConnection OpenConnection(string dataFile, string Password)
        {
            if (dataFile == null)
            {
                throw new ArgumentNullException("dataFile=null");
            }
            if (!File.Exists(Convert.ToString(dataFile)))
            {
                SQLiteConnection.CreateFile(dataFile);
            }
            try
            {
                SQLiteConnection conn = new SQLiteConnection();
                SQLiteConnectionStringBuilder conStr = new SQLiteConnectionStringBuilder
                {
                    DataSource = dataFile,
                    Password = Password
                };
                conn.ConnectionString = conStr.ToString();
                conn.Open();
                return conn;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>  
        /// 读取 或 设置 SQLiteManager 使用的数据库连接  
        /// </summary>  
        public SQLiteConnection Connection
        {
            get
            {
                return mConn;
            }
            private set
            {
                mConn = value ?? throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// 确保数据库是连接状态
        /// </summary>
        /// <exception cref="Exception"></exception>
        protected void EnsureConnection()
        {
            if (this.mConn == null)
            {
                throw new Exception("SQLiteManager.Connection=null");
            }
            if (mConn.State != ConnectionState.Open)
            {
                mConn.Open();
            }
        }

        /// <summary>
        /// 获取数据库文件的路径
        /// </summary>
        /// <returns></returns>
        public string GetDataFile()
        {
            return this.mdataFile;
        }

        /// <summary>  
        /// 判断表 table 是否存在  
        /// </summary>  
        /// <param name="table"></param>  
        /// <returns>存在返回true，否则返回false</returns>  
        public bool TableExists(string table)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table=null");
            }
            EnsureConnection();
            SQLiteDataReader reader = ExecuteReader("SELECT count(*) as c FROM sqlite_master WHERE type='table' AND name=@tableName ", new SQLiteParameter[] { new SQLiteParameter("tableName", table) });
            if (reader == null)
            {
                return false;
            }
            reader.Read();
            int c = reader.GetInt32(0);
            reader.Close();
            reader.Dispose();
            //return false;  
            return c == 1;
        }

        /// <summary>
        /// VACUUM 命令（通过复制主数据库中的内容到一个临时数据库文件，然后清空主数据库，并从副本中重新载入原始的数据库文件）
        /// </summary>
        /// <returns></returns>
        public bool Vacuum()
        {
            try
            {
                using (SQLiteCommand Command = new SQLiteCommand("VACUUM", Connection))
                {
                    Command.ExecuteNonQuery();
                }
                return true;
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                return false;
            }
        } 

        #endregion

        #region 执行SQL

        /// <summary>
        /// 执行SQL, 并返回 SQLiteDataReader 对象结果 
        /// </summary>  
        /// <param name="sql"></param>
        /// <param name="paramArr">null 表示无参数</param>
        /// <returns></returns>  
        public SQLiteDataReader ExecuteReader(string sql, SQLiteParameter[] paramArr)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql=null");
            }
            EnsureConnection();
            using (RWL[LockName].Read())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, Connection))
                {
                    if (paramArr != null)
                    {
                        cmd.Parameters.AddRange(paramArr);
                    }
                    try
                    {
                        SQLiteDataReader reader = cmd.ExecuteReader();
                        cmd.Parameters.Clear();
                        return reader;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询，并返回dataset对象
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="paramArr">参数数组</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sql, SQLiteParameter[] paramArr)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql=null");
            }
            this.EnsureConnection();
            using (RWL[LockName].Read())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, this.Connection))
                {
                    if (paramArr != null)
                    {
                        cmd.Parameters.AddRange(paramArr);
                    }
                    try
                    {
                        SQLiteDataAdapter da = new SQLiteDataAdapter();
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        cmd.Parameters.Clear();
                        da.Dispose();
                        return ds;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }
        public DataSet ExecuteDataSet(string sql)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql=null");
            }
            this.EnsureConnection();
            using (RWL[LockName].Read())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, this.Connection))
                {
                    try
                    {
                        SQLiteDataAdapter da = new SQLiteDataAdapter();
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        da.Dispose();
                        return ds;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }


            /// <summary>
            /// 执行SQL查询，并返回dataset对象。
            /// </summary>
            /// <param name="strTable">映射源表的名称</param>
            /// <param name="sql">SQL语句</param>
            /// <param name="paramArr">SQL参数数组</param>
            /// <returns></returns>
            public DataSet ExecuteDataSet(string strTable, string sql, SQLiteParameter[] paramArr)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql=null");
            }
            this.EnsureConnection();
            using (RWL[LockName].Read())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, this.Connection))
                {
                    if (paramArr != null)
                    {
                        cmd.Parameters.AddRange(paramArr);
                    }
                    try
                    {
                        SQLiteDataAdapter da = new SQLiteDataAdapter();
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds, strTable);
                        cmd.Parameters.Clear();
                        da.Dispose();
                        return ds;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>  
        /// 执行SQL，返回受影响的行数，可用于执行表创建语句，paramArr == null 表示无参数
        /// </summary>  
        /// <param name="sql"></param>  
        /// <returns></returns>  
        public int ExecuteNonQuery(string sql, SQLiteParameter[] paramArr)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql=null");
            }
            this.EnsureConnection();
            using (RWL[LockName].Read())
            {
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, Connection))
                    {
                        if (paramArr != null)
                        {
                            foreach (SQLiteParameter p in paramArr)
                            {
                                cmd.Parameters.Add(p);
                            }
                        }
                        int c = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return c;
                    }
                }
                catch (SQLiteException)
                {
                    return 0;
                }
            }
        }

        /// <summary>  
        /// 执行SQL，返回结果集第一行，如果结果集为空，那么返回空 List(List.Count=0)， 
        /// rowWrapper = null 时，使用 WrapRowToDictionary  
        /// </summary>  
        /// <param name="sql"></param>  
        /// <param name="paramArr"></param>  
        /// <returns></returns>  
        public object ExecuteScalar(string sql, SQLiteParameter[] paramArr)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql=null");
            }
            this.EnsureConnection();
            using (RWL[LockName].Read())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, Connection))
                {
                    if (paramArr != null)
                    {
                        cmd.Parameters.AddRange(paramArr);
                    }
                    try
                    {
                        object reader = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        cmd.Dispose();
                        return reader;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>  
        /// 查询一行记录，无结果时返回 null，conditionCol = null 时将忽略条件，直接执行 select * from table   
        /// </summary>  
        /// <param name="table">表名</param>  
        /// <param name="conditionCol"></param>  
        /// <param name="conditionVal"></param>  
        /// <returns></returns>  
        public object QueryOne(string table, string conditionCol, object conditionVal)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table=null");
            }
            this.EnsureConnection();
            string sql = "select * from " + table;
            if (conditionCol != null)
            {
                sql += " where " + conditionCol + "=@" + conditionCol;
            }
            object result = ExecuteScalar(sql, new SQLiteParameter[] { new SQLiteParameter(conditionCol, conditionVal) });
            return result;
        }

        #endregion

        #region 增 删 改

        /// <summary>  
        /// 执行 insert into 语句 
        /// </summary>  
        /// <param name="table"></param>  
        /// <param name="entity"></param>  
        /// <returns></returns>  
        public int InsertData(string table, Dictionary<string, object> entity)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table=null");
            }
            this.EnsureConnection();
            string sql = BuildInsert(table, entity);
            return this.ExecuteNonQuery(sql, BuildParamArray(entity));
        }

        /// <summary>  
        /// 执行 update 语句，注意：如果 where = null，那么 whereParams 也为 null，
        /// </summary>  
        /// <param name="table">表名</param>  
        /// <param name="entity">要修改的列名和列名的值</param>  
        /// <param name="where">查找符合条件的列</param>  
        /// <param name="whereParams">where条件中参数的值</param>  
        /// <returns></returns>  
        public int Update(string table, Dictionary<string, object> entity, string where, SQLiteParameter[] whereParams)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table=null");
            }
            this.EnsureConnection();
            string sql = BuildUpdate(table, entity);
            SQLiteParameter[] parameter = BuildParamArray(entity);
            if (where != null)
            {
                sql += " where " + where;
                if (whereParams != null)
                {
                    SQLiteParameter[] newArr = new SQLiteParameter[(parameter.Length + whereParams.Length)];
                    Array.Copy(parameter, newArr, parameter.Length);
                    Array.Copy(whereParams, 0, newArr, parameter.Length, whereParams.Length);
                    parameter = newArr;
                }
            }
            return this.ExecuteNonQuery(sql, parameter);
        }

        /// <summary>  
        /// 执行 delete from table 语句，where不必包含'where'关键字，where = null 时将忽略 whereParams  
        /// </summary>  
        /// <param name="table"></param>  
        /// <param name="where"></param>  
        /// <param name="whereParams"></param>  
        /// <returns></returns>  
        public int Delete(string table, string where, SQLiteParameter[] whereParams)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table=null");
            }
            this.EnsureConnection();
            string sql = "delete from " + table + " ";
            if (where != null)
            {
                sql += "where " + where;
            }
            return ExecuteNonQuery(sql, whereParams);
        }

        /// <summary>
        /// 将 Dictionary 类型数据 转换为 SQLiteParameter[] 类型
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private SQLiteParameter[] BuildParamArray(Dictionary<string, object> entity)
        {
            List<SQLiteParameter> list = new List<SQLiteParameter>();
            foreach (string key in entity.Keys)
            {
                list.Add(new SQLiteParameter(key, entity[key]));
            }
            if (list.Count == 0)
            {
                return null;
            }
            return list.ToArray();
        }

        /// <summary>
        /// 将 Dictionary 类型数据 转换为 插入数据 的 SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="entity">字典</param>
        /// <returns></returns>
        private string BuildInsert(string table, Dictionary<string, object> entity)
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("insert into ").Append(table);
            buf.Append(" (");
            foreach (string key in entity.Keys)
            {
                buf.Append(key).Append(",");
            }
            buf.Remove(buf.Length - 1, 1); // 移除最后一个,
            buf.Append(") ");
            buf.Append("values(");
            foreach (string key in entity.Keys)
            {
                buf.Append("@").Append(key).Append(","); // 创建一个参数
            }
            buf.Remove(buf.Length - 1, 1);
            buf.Append(") ");

            return buf.ToString();
        }

        /// <summary>
        /// 将 Dictionary 类型数据 转换为 修改数据 的 SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="entity">字典</param>
        /// <returns></returns>
        private string BuildUpdate(string table, Dictionary<string, object> entity)
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("update ").Append(table).Append(" set ");
            foreach (string key in entity.Keys)
            {
                buf.Append(key).Append("=").Append("@").Append(key).Append(",");
            }
            buf.Remove(buf.Length - 1, 1);
            buf.Append(" ");
            return buf.ToString();
        }


       
        
        #endregion
    }
}
