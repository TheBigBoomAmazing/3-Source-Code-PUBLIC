using System;
using System.Collections.Generic;
using System.Text;

namespace Suzsoft.Smart.EntityCore
{
    /// <summary>
    /// SQL参数集合
    /// </summary>
    public class DataAccessParameterCollection : Dictionary<string, DataAccessParameter>
    {
        public DataAccessParameterCollection()
        {
        }

        /// <summary>
        /// 添加SQL参数列表-输入参数
        /// </summary>
        /// <param name="ParamName"></param>
        /// <param name="ParamValue"></param>
        public virtual void AddWithValue(ColumnInfo columnInfo, object parameterValue)
        {
            ColumnInfo newColumnInfo = new ColumnInfo(columnInfo.ColumnName, columnInfo.ColumnCaption, columnInfo.IsPrimaryKey, columnInfo.DataType);
            if (columnInfo.ColumnName.IndexOf("@") != 0)
            {
                newColumnInfo.ColumnName = "@" + columnInfo.ColumnName;
            }
            //if (parameterName.IndexOf(":") != 0)
            //{
            //    parameterName = ":" + parameterName;
            //}
            AddWithValue(newColumnInfo, parameterValue, System.Data.ParameterDirection.Input);
        }


        /// <summary>
        /// 添加SQL参数列表
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="direction"></param>
        public virtual void AddWithValue(ColumnInfo columnInfo,object parameterValue, System.Data.ParameterDirection direction)
        {
            string parameterName = columnInfo.ColumnName;
            DataAccessParameter parameter = new DataAccessParameter(parameterName, parameterValue, direction);
            this[parameterName] = parameter;
            if (columnInfo.DataType == typeof(int))
            {
                parameter.DbType = System.Data.DbType.Int32;
            }
            if (columnInfo.DataType == typeof(bool))
            {
                parameter.DbType = System.Data.DbType.Boolean;
            }
            if (columnInfo.DataType == typeof(DateTime))
            {
                parameter.DbType = System.Data.DbType.DateTime;
            }
            if (columnInfo.DataType == typeof(Int64))
            {
                parameter.DbType = System.Data.DbType.Int64;
            }
            if (columnInfo.DataType == typeof(Int16))
            {
                parameter.DbType = System.Data.DbType.Int16;
            }
            if (columnInfo.DataType == typeof(decimal))
            {
                parameter.DbType = System.Data.DbType.Decimal;
            }
            if (columnInfo.DataType == typeof(double))
            {
                parameter.DbType = System.Data.DbType.Double;
            }
            if (columnInfo.DataType == typeof(float))
            {
                parameter.DbType = System.Data.DbType.Single;
            }
        }

        /// <summary>
        /// 添加SQL参数列表
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="direction"></param>
        public virtual void AddWithValue(ColumnInfo columnInfo, object parameterValue,
            System.Data.ParameterDirection direction, int size)
        {
            string parameterName = columnInfo.ColumnName;
            DataAccessParameter parameter = new DataAccessParameter(parameterName, parameterValue, direction);
            parameter.Size = size;
            this[parameterName] = parameter;
            if (columnInfo.DataType == typeof(int))
            {
                parameter.DbType = System.Data.DbType.Int32;
            }
            if (columnInfo.DataType == typeof(bool))
            {
                parameter.DbType = System.Data.DbType.Boolean;
            }
            if (columnInfo.DataType == typeof(DateTime))
            {
                parameter.DbType = System.Data.DbType.DateTime;
            }
            if (columnInfo.DataType == typeof(Int64))
            {
                parameter.DbType = System.Data.DbType.Int64;
            }
            if (columnInfo.DataType == typeof(Int16))
            {
                parameter.DbType = System.Data.DbType.Int16;
            }
            if (columnInfo.DataType == typeof(decimal))
            {
                parameter.DbType = System.Data.DbType.Decimal;
            }
            if (columnInfo.DataType == typeof(double))
            {
                parameter.DbType = System.Data.DbType.Double;
            }
            if (columnInfo.DataType == typeof(float))
            {
                parameter.DbType = System.Data.DbType.Single;
            }
        }
    }
}
