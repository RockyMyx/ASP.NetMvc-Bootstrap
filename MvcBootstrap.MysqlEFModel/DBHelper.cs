using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using MySql.Data.MySqlClient;

public class DBHelper
{
    #region 字段
    /// <summary>
    /// 数据库对象工厂
    /// </summary>
    DbProviderFactory _provider;
    /// <summary>
    /// 数据库连接对象
    /// </summary>
    DbConnection _connection;
    /// <summary>
    /// 数据库命令
    /// </summary>
    DbCommand _command;
    /// <summary>
    /// 阅读器
    /// </summary>
    DbDataReader _reader;
    /// <summary>
    /// 适配器
    /// </summary>
    DbDataAdapter _adapter;
    /// <summary>
    /// 参数
    /// </summary>
    DbParameter _parameter;
    /// <summary>
    /// 数据集
    /// </summary>
    DataSet _ds;
    /// <summary>
    /// 数据库类型
    /// </summary>
    string _sqlType;
    /// <summary>
    /// 数据库参数前缀
    /// Oracle:    select count(*) from gnetwork_ais_file where file_name = :file_name
    ///            db.AddParameter("file_name", "xxx")
    /// SQLServer: select count(*) from gnetwork_ais_file where file_name = @file_name
    ///            db.AddParameter("@file_name", "xxx")
    /// MySQL:     select count(*) from gnetwork_ais_file where file_name = ?file_name
    ///            db.AddParameter("file_name", "xxx")
    /// </summary>
    string _paraPrefix;
    /// <summary>
    /// 标志SQL操作是否执行成功
    /// </summary>
    bool isSuccess = false;
    #endregion

    #region 初始化数据库对象工厂
    /// <summary>
    /// 初始化数据库对象工厂
    /// </summary>
    DbProviderFactory GetDbProvider(string providerName)
    {
        return DbProviderFactories.GetFactory(providerName);
    }
    #endregion

    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    public DBHelper(string providerName, string connectionString, string sqlType)
    {
        this._sqlType = sqlType;
        if (string.Compare(sqlType, "Mysql", true) == 0)
        {
            _connection = new MySqlConnection();
            _command = new MySqlCommand();
            this._paraPrefix = "?";
        }
        else
        {
            _provider = GetDbProvider(providerName);
            _connection = _provider.CreateConnection();
            _command = _provider.CreateCommand();
            if (string.Compare(sqlType, "Oracle", true) == 0)
            {
                this._paraPrefix = ":";
            }
            else if (string.Compare(sqlType, "SQLServer", true) == 0)
            {
                this._paraPrefix = "@";
            }
        }

        _connection.ConnectionString = connectionString;
        _command.Connection = _connection;
    }

    /// <summary>
    /// 配置web.config文件，数据库连接字符串名为ConnectionString
    /// </summary>
    public DBHelper(string sqlType)
        : this(ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName,
               ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, sqlType) { }
    #endregion

    #region 清空SQL参数
    /// <summary>
    /// 清空SQL参数
    /// </summary>
    public void ClearParameters()
    {
        _command.Parameters.Clear();
    }
    #endregion

    #region 打开数据库连接
    /// <summary>
    /// 打开数据库连接
    /// </summary>
    void Open()
    {
        try
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        catch (Exception e)
        {
            LogHelper.Append(e.ToString());
        }
    }
    #endregion

    #region 关闭数据库连接
    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    void Close()
    {
        if (_connection.State == ConnectionState.Open)
        {
            _connection.Close();
        }
    }
    #endregion

    #region 无参SQL语句操作

    #region ExecuteNonQuery
    /// <summary>
    /// 执行无参SQL语句
    /// </summary>
    public bool ExecuteNonQuery(string sqlStr)
    {
        try
        {
            Open();
            _command.CommandText = sqlStr;
            _command.ExecuteNonQuery();
            isSuccess = true;
        }
        catch (Exception e)
        {
            LogHelper.Append(e.ToString());
        }
        finally
        {
            Close();
        }

        return isSuccess;
    }
    #endregion

    #region ExecuteSqlTran
    /// <summary>
    /// 批量执行多条无参SQL语句（实现数据库事务）
    /// </summary>
    public bool ExecuteSqlTran(List<string> sqlStrList)
    {
        try
        {
            Open();
            using (DbTransaction tran = _connection.BeginTransaction())
            {
                _command.Transaction = tran;
                foreach (string s in sqlStrList)
                {
                    _command.CommandText = s;
                    _command.ExecuteNonQuery();
                }
                tran.Commit();
                isSuccess = true;
            }
        }
        catch (Exception e)
        {
            LogHelper.Append(e.ToString());
        }
        finally
        {
            Close();
        }

        return isSuccess;
    }
    #endregion

    #region ExecuteScalar
    /// <summary>
    /// 执行一条无参SQL语句，返回首行首列的值
    /// </summary>
    public object ExecuteScalar(string sqlStr)
    {
        object result = null;
        try
        {
            Open();
            _command.CommandText = sqlStr;
            result = _command.ExecuteScalar();
            if ((Object.Equals(result, null)) || (Object.Equals(result, System.DBNull.Value)))
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        catch (Exception e)
        {
            LogHelper.Append(e.ToString());
            return null;
        }
        finally
        {
            Close();
        }
    }
    #endregion

    #region ExecuteReader
    /// <summary>
    /// 执行一条无参SQL语句，获取DataReader对象阅读器
    /// </summary>
    public DbDataReader ExecuteReader(string sqlStr)
    {
        try
        {
            Open();
            _command.CommandText = sqlStr;
            _reader = _command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (Exception e)
        {
            LogHelper.Append(e.ToString());
        }

        return _reader;
    }
    #endregion

    #region GetDataSet
    /// <summary>
    /// 执行一条无参SQL语句，返回DataSet
    /// </summary>
    /// <param name="sqlStr">SQL语句</param>
    /// <param name="dsName">数据集名</param>
    public DataSet GetDataSet(string sqlStr, string dsName)
    {
        try
        {
            Open();
            _ds = new DataSet();
            _command.CommandText = sqlStr;
            if (string.Compare(_sqlType, "Mysql", true) == 0)
            {
                _adapter = new MySqlDataAdapter();
            }
            else
            {
                _adapter = _provider.CreateDataAdapter();
            }
            _adapter.SelectCommand = _command;
            if (string.IsNullOrEmpty(dsName))
            {
                _adapter.Fill(_ds);
            }
            else
            {
                _adapter.Fill(_ds, dsName);
            }
        }
        catch (DbException)
        {
            throw;
        }
        finally
        {
            Close();
        }

        return _ds;
    }

    /// <summary>
    /// 执行一条无参SQL语句，返回无名称的DataSet
    /// </summary>
    public DataSet GetDataSet(string sqlStr)
    {
        return GetDataSet(sqlStr, "");
    }
    #endregion

    #region GetDataTable
    /// <summary>
    /// 执行一条无参SQL语句，返回DataTable
    /// </summary>
    /// <param name="sqlStr">SQL语句</param>
    /// <param name="tableName">数据表名</param>
    /// <returns>DataTable对象</returns>
    public DataTable GetDataTable(string sqlStr, string tableName)
    {
        return GetDataSet(sqlStr, tableName).Tables[tableName];
    }

    /// <summary>
    /// 执行一条无参SQL语句，返回无名称的DataTable
    /// </summary>
    public DataTable GetDataTable(string sqlStr)
    {
        return GetDataSet(sqlStr).Tables[0];
    }
    #endregion

    #endregion

    #region 添加参数
    /// <summary>
    /// 添加一个SQL参数
    /// </summary>
    public DbParameter AddParameter(string parameterName, object value)
    {
        if (string.Compare(_sqlType, "Mysql", true) == 0)
        {
            _parameter = new MySqlParameter();
            _parameter.ParameterName = this._paraPrefix + parameterName;
        }
        else
        {
            _parameter = _provider.CreateParameter();
            if (string.Compare(_sqlType, "Oracle", true) == 0)
            {
                _parameter.ParameterName = parameterName;
            }
            else if (string.Compare(_sqlType, "SQLServer", true) == 0)
            {
                _parameter.ParameterName = this._paraPrefix + parameterName;
            }
        }

        _parameter.Value = value;
        _command.Parameters.Add(_parameter);
        return _parameter;
    }
    #endregion

    #region SQL参数化操作

    #region 构造带参数的 DbCommand 对象
    /// <summary>
    /// 构造带多个参数的 DbCommand
    /// </summary>
    private void BuildTextCommand(string sqlStr)
    {
        Open();
        _command.CommandType = CommandType.Text;
        _command.CommandText = string.Format(sqlStr, this._paraPrefix);
    }
    #endregion

    #region ExecuteNonQuery
    /// <summary>
    /// 执行一条有多个参数的SQL语句
    /// </summary>
    public bool ExecuteNonQueryWithParams(string sqlStr)
    {
        try
        {
            BuildTextCommand(sqlStr);
            _command.ExecuteNonQuery();
            isSuccess = true;
        }
        catch (DbException)
        {
            throw;
        }
        finally
        {
            Close();
        }

        return isSuccess;
    }
    #endregion

    #region ExecuteScalar
    /// <summary>
    /// 执行一条带多个参数的SQL语句，返回首行首列的值
    /// </summary>
    public object ExecuteScalarWithParams(string sqlStr)
    {
        object result = null;
        try
        {
            BuildTextCommand(sqlStr);
            result = _command.ExecuteScalar();
            if ((Object.Equals(result, null)) || (Object.Equals(result, System.DBNull.Value)))
                return null;
            else
                return result;
        }
        catch (DbException)
        {
            throw;
        }
        finally
        {
            Close();
        }
    }
    #endregion

    #region ExecuteReader
    /// <summary>
    /// 执行一条带多个参数的SQL语句，获取DataReader对象阅读器
    /// </summary>
    public DbDataReader ExecuteReaderWithParams(string sqlStr)
    {
        try
        {
            BuildTextCommand(sqlStr);
            _reader = _command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (DbException)
        {
            throw;
        }

        return _reader;
    }
    #endregion

    #region GetDataSet
    /// <summary>
    /// 执行一条带多个参数的SQL语句，返回DataSet
    /// </summary>
    public DataSet GetDataSetWithParams(string sqlStr, string dsName)
    {
        _ds = new DataSet();
        try
        {
            BuildTextCommand(sqlStr);
            if (string.Compare(_sqlType, "Mysql", true) == 0)
            {
                _adapter = new MySqlDataAdapter();
            }
            else
            {
                _adapter = _provider.CreateDataAdapter();
            }

            _adapter.SelectCommand = _command;
            if (string.IsNullOrEmpty(dsName))
            {
                _adapter.Fill(_ds);
            }
            else
            {
                _adapter.Fill(_ds, dsName);
            }
        }
        catch (DbException)
        {
            throw;
        }
        finally
        {
            Close();
        }

        return _ds;
    }

    /// <summary>
    /// 执行一条带多个参数的SQL语句，返回无名称的DataSet
    /// </summary>
    public DataSet GetDataSetWithParams(string sqlStr)
    {
        return GetDataSetWithParams(sqlStr, "");
    }
    #endregion

    #region GetDataTable
    /// <summary>
    /// 执行一条带多个参数的SQL语句，返回DataTable
    /// </summary>
    public DataTable GetDataTableWithParams(string sqlStr, string tableName)
    {
        return GetDataSetWithParams(sqlStr, tableName).Tables[tableName];
    }

    /// <summary>
    /// 执行一条带多个参数的SQL语句，返回无名称的DataTable
    /// </summary>
    public DataTable GetDataTableWithParams(string sqlStr)
    {
        return GetDataSetWithParams(sqlStr).Tables[0];
    }
    #endregion

    #endregion

    #region SQL存储过程操作

    #region 构建带存储过程名的 DbCommand 对象
    /// <summary>
    /// 构建有多个参数的存储过程
    /// </summary>
    private void BuildProcCommand(string storedProcName)
    {
        Open();
        _command.CommandType = CommandType.StoredProcedure;
        _command.CommandText = storedProcName;
    }
    #endregion

    #region ExecuteNonQuery
    /// <summary>
    /// 执行有多个参数的存储过程语句
    /// </summary>
    public bool ExecuteNonQueryByProcedure(string storeProcName)
    {
        try
        {
            BuildProcCommand(storeProcName);
            _command.ExecuteNonQuery();
            isSuccess = true;
        }
        catch (DbException)
        {
            throw;
        }
        finally
        {
            Close();
        }

        return isSuccess;
    }
    #endregion

    #region ExecuteScalar
    /// <summary>
    /// 执行有多个参数的存储过程语句，返回首行首列的值
    /// </summary>
    public object ExecuteScalarByProcedure(string storeProcName)
    {
        object result = null;
        try
        {
            BuildProcCommand(storeProcName);
            result = _command.ExecuteScalar();
            if ((Object.Equals(result, null)) || (Object.Equals(result, System.DBNull.Value)))
            {
                return null;
            }
            else
            {
                return result;
            }
        }
        catch (DbException)
        {
            throw;
        }
        finally
        {
            Close();
        }
    }
    #endregion

    #region ExecuteReader
    /// <summary>
    /// 执行有多个参数的存储过程语句，得到DataReader阅读器对象
    /// </summary>
    public DbDataReader ExecuteReaderByProcedure(string storedProcName)
    {
        try
        {
            BuildProcCommand(storedProcName);
            _reader = _command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (DbException)
        {
            throw;
        }

        return _reader;
    }
    #endregion

    #region GetDataSet
    /// <summary>
    /// 执行有多个参数的存储过程语句，返回DataSet
    /// </summary>
    public DataSet GetDataSetByProcedure(string storedProcName, string dsName)
    {
        _ds = new DataSet();
        try
        {
            BuildProcCommand(storedProcName);
            if (string.Compare(_sqlType, "Mysql", true) == 0)
            {
                _adapter = new MySqlDataAdapter();
            }
            else
            {
                _adapter = _provider.CreateDataAdapter();
            }

            _adapter.SelectCommand = _command;
            if (string.IsNullOrEmpty(dsName))
                _adapter.Fill(_ds);
            else
                _adapter.Fill(_ds, dsName);
        }
        catch (DbException)
        {
            throw;
        }
        finally
        {
            Close();
        }

        return _ds;
    }

    /// <summary>
    /// 执行有多个参数的存储过程语句，返回无名称的DataSet
    /// </summary>
    public DataSet GetDataSetByProcedure(string storedProcName)
    {
        return GetDataSetByProcedure(storedProcName, "");
    }
    #endregion

    #region GetDataTable
    /// <summary>
    /// 执行有多个参数的存储过程语句，返回DataTable
    /// </summary>
    public DataTable GetDataTableByProcedure(string storedProcName, string tableName)
    {
        return GetDataSetByProcedure(storedProcName, tableName).Tables[tableName];
    }

    /// <summary>
    /// 执行有多个参数的存储过程语句，返回无名称的DataTable
    /// </summary>
    public DataTable GetDataTableByProcedure(string storedProcName)
    {
        return GetDataSetByProcedure(storedProcName).Tables[0];
    }
    #endregion

    #endregion

    #region 利用程序生成SQL语句

    #region InsertSQL
    /// <summary>
    /// 通过表名与SQL参数集合生成插入的SQL语句
    /// </summary>
    public string GetInsertSQL(string tableName)
    {
        StringBuilder strBuilder = new StringBuilder("INSERT INTO ");
        strBuilder.AppendFormat("{0}(", tableName);
        StringBuilder strBuilderValue = new StringBuilder();
        foreach (DbParameter sp in _command.Parameters)
        {
            strBuilder.AppendFormat("[{0}],", sp.ParameterName.Remove(0, 1));
            strBuilderValue.AppendFormat("{0},", sp.ParameterName);
        }
        strBuilder.Remove(strBuilder.Length - 1, 1);
        strBuilderValue.Remove(strBuilderValue.Length - 1, 1);
        strBuilder.AppendFormat(") values({0});select @@identity as id;", strBuilderValue.ToString());
        return strBuilder.ToString();
    }
    #endregion

    #region DeleteSQL
    /// <summary>
    /// 返回带参数的删除语句
    /// </summary>
    public string GetDeleteSQL(string table, string condition)
    {
        condition = !string.IsNullOrEmpty(condition) ? " WHERE " + condition : "";
        string sqlStr = string.Format("DELETE FROM {0} {1}", table, condition);
        return sqlStr;
    }
    #endregion

    #region UpdateSQL
    /// <summary>
    /// 通过表名与SQL参数集合生成更新的SQL语句
    /// </summary>
    public string GetUpdateSQL(string tableName, string condition)
    {
        if (!string.IsNullOrEmpty(condition))
        {
            condition = " WHERE " + condition;
        }
        StringBuilder sb = new StringBuilder("UPDATE ");
        sb.AppendFormat("{0} SET ", tableName);
        foreach (DbParameter sp in _command.Parameters)
        {
            sb.AppendFormat("[{0}]=@{0},", sp.ParameterName.Remove(0, 1));
        }
        sb.Remove(sb.Length - 1, 1);    //去掉最后一个逗号
        sb.AppendFormat(" {0}", condition);
        return sb.ToString();
    }
    #endregion

    #region SelectSQL

    /// <summary>
    /// 通过表名、数量、字段名等生成相关查询语句
    /// </summary>
    public string GetSelectSQL(string table, string fields, string condition)
    {
        fields = (!string.IsNullOrEmpty(fields)) ? fields : "*";
        condition = (!string.IsNullOrEmpty(condition)) ? " where " + condition : "";
        string sqlStr = "SELECT {0} FROM {1} {2}";
        return string.Format(sqlStr, fields, table, condition);
    }
    #endregion

    #endregion
}