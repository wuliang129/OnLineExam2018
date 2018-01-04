using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

    /// <summary>
    ///DBHelper 的摘要说明 数据库操作类
    /// </summary>
    public class DBHelper
    {
        //protected static string conString = "data source=127.0.0.1;Database=Students;user id=sa;password=123";
        protected string conString = ConfigurationManager.ConnectionStrings["NewsConnectionString"].ToString();

        #region 构造 DBHelper 对象
        /// <summary>
        /// 构造 DBHelper 对象
        /// </summary>
        public DBHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 构造 DBHelper 对象
        /// </summary>
        /// <param name="strCon"></param>
        public DBHelper(string strCon)
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
            conString = strCon;
        }
        #endregion

        #region  执行查询 返回查询结果


        /// <summary>
        /// 执行SQL语句，返回查询结果 的个数
        /// </summary>
        /// <param name="StrSql">查询sql命令</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>
        /// 查询结果 第一行第一列 int 整形
        /// </returns>
        public int ExecuteSelect(string StrSql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    connection.Open();
                    PrepareCommand(cmd, connection, StrSql, cmdParms);
                    int rows;
                    try
                    {
                        rows =Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                    cmd.Parameters.Clear();
                    return rows;
                }
            }
        }

        #endregion

        #region  执行数据库命令  返回影响的记录数
        


        /// <summary>
        /// 执行SQL语句，返回影响的记录数 
        /// </summary>
        /// <param name="StrSql">SQL语句</param> 
        /// <returns>影响的记录数</returns>  
        public int ExecuteSql(string StrSql)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(StrSql, connection))
                {
                    connection.Open();
                    int rows;
                    try
                    {
                        rows = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                    return rows;
                }
            }
        }


        /// <summary>
        /// 执行SQL语句，返回影响的记录数 
        /// </summary>
        /// <param name="StrSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSql(string StrSql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    connection.Open();
                    PrepareCommand(cmd, connection, StrSql, cmdParms);
                    int rows;
                    try
                    {
                        rows = cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                    cmd.Parameters.Clear();
                    return rows;
                }
            }
        }
        #endregion


        #region  执行数据库查询语句 返回 object类型
       
        ///<summary>  
        ///执行一条计算查询结果语句，返回查询结果（object）  
        ///</summary>  
        ///<param name="StrSql">计算查询结果语句</param>  
        ///<returns>查询结果（object）</returns>  
        public object ExecuteScalar(string StrSql)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(StrSql, connection))
                {
                    connection.Open();
                    object obj;
                    try
                    {
                        obj = cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
            }
        }


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）,返回首行首列的值;
        /// </summary>
        /// <param name="StrSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns>影响的记录数</returns>
        public object ExecuteScalar(string StrSql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    connection.Open();
                    PrepareCommand(cmd, connection, StrSql, cmdParms);
                    object obj;
                    try
                    {
                        obj = cmd.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                        cmd.Parameters.Clear();
                    }
                    return obj;
                }
            }
        }
        #endregion

        #region 利用 ExecuteReader 执行查询语句，返回 链表
        
        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public ArrayList ExecuteReaderBackArray(string strSQL)
        {
            ArrayList ar = new ArrayList();
            SqlConnection connection = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
               
                while (myReader.Read())
                {
                    ar.Add(myReader[0].ToString());
                }
                return ar;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public ArrayList ExecuteReaderBackArray(string SQLString, params SqlParameter[] cmdParms)
        {
            ArrayList ar = new ArrayList();
            SqlConnection connection = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            connection.Open();
            try
            {
                PrepareCommand(cmd, connection, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();

                while (myReader.Read())
                {
                    ar.Add(myReader[0].ToString());
                }
                return ar;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        #endregion

        #region 执行查询语句，返回SqlDataReader
        
        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string strSQL)
        {
            SqlConnection connection = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            connection.Open();
            try
            {
                PrepareCommand(cmd, connection, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        #endregion

        #region 执行查询语句，返回DataSet
        
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string SQLString)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(SQLString, connection);
                    adapter.Fill(ds, "ds");
                    connection.Close();
                    return ds;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand();
                try
                {
                    connection.Open();
                    PrepareCommand(cmd, connection, SQLString, cmdParms);
                    
                    
                    //SqlDataAdapter adapter = new SqlDataAdapter(SQLString, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds, "ds");
                    
                    connection.Close();
                    return ds;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        #endregion

        

        #region 准备数据
        /// <summary>
        /// 准备数据库操作数据
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="connection"></param>
        /// <param name="StrSql"></param>
        /// <param name="cmdParms"></param>
        protected void PrepareCommand(SqlCommand cmd, SqlConnection connection, string StrSql, params SqlParameter[] cmdParms)
        {
            cmd.Connection = connection;
            cmd.CommandText = StrSql;
            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (SqlParameter param in cmdParms)
                    cmd.Parameters.Add(param);
            }
        }

        /// <summary>
        /// 准备数据
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, params SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }


        /// <summary>
        /// 公有方法，实例化一个用于调用存储过程的输入参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Value">值</param>
        /// <returns></returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }	

        /// <summary>
        /// 公有方法，实例化一个用于调用存储过程的参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">参数名称</param>
        /// <param name="Size">参数大小</param>
        /// <param name="Direction">传递方向</param>
        /// <param name="Value">值</param>
        /// <returns></returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter Param;

            if (Size > 0)
                Param = new SqlParameter(ParamName, DbType, Size);
            else Param = new SqlParameter(ParamName, DbType);

            Param.Direction = Direction;

            if (Value != null)
                Param.Value = Value;

            return Param;
        }

        #endregion


        #region 执行多条SQL语句，实现数据库事务

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>
        /// <returns>
        /// 执行成功返回 true 否则 false
        /// </returns>
        public bool ExecuteSqlTran(ArrayList SQLStringList)
        {
            bool bResult = true;
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    bResult = false;
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return bResult;
        }




        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        conn.Close();
                        throw new Exception(e.Message);
                    }
                }
                conn.Close();
            }
        }
        #endregion


        #region 存储过程操作
        
        /// <summary>
        /// 执行存储过程;
        /// </summary>
        /// <param name="storeProcName">存储过程名</param>
        /// <param name="parameters">所需要的参数</param>
        /// <returns>返回受影响的行数</returns>
        public int RunProcedureExecuteSql(string storeProcName, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = BuildQueryCommand(connection, storeProcName, parameters);
                int rows = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                connection.Close();
                return rows;
            }
        }
       
        /// <summary>
        /// 执行存储过程,返回首行首列的值
        /// </summary>
        /// <param name="storeProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>返回首行首列的值</returns>
        public Object RunProcedureGetSingle(string storeProcName, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                try
                {
                    SqlCommand cmd = BuildQueryCommand(connection, storeProcName, parameters);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
       
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader RunProcedureGetDataReader(string storedProcName, params SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(conString);
            SqlDataReader returnReader;
            SqlCommand cmd = BuildQueryCommand(connection, storedProcName, parameters);
            cmd.CommandType = CommandType.StoredProcedure;
            returnReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return returnReader;
        }
        
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedureGetDataSet(string storedProcName, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet);
                connection.Close();
                sqlDA.SelectCommand.Parameters.Clear();
                sqlDA.Dispose();
                return dataSet;
            }
        }

        /// <summary>
        /// 执行多个存储过程，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">存储过程的哈希表（key是该语句的params SqlParameter[],value为存储过程语句）</param>
        public bool RunProcedureTran(Hashtable SQLStringList)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();
                using (SqlTransaction trans = connection.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            cmd.Connection = connection;
                            string storeName = myDE.Value.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Key;

                            cmd.Transaction = trans;
                            cmd.CommandText = storeName;
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (cmdParms != null)
                            {
                                foreach (SqlParameter parameter in cmdParms)
                                    cmd.Parameters.Add(parameter);
                            }
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                        throw;
                    }
                }
            }
        }
        
        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, params SqlParameter[] parameters)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }
        #endregion


    }
