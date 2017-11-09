using Suzsoft.Smart.EntityCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace Suzsoft.Smart.EntityCore
{
    public static class EntityExtends
    {
        public static DataRow ConvertRow(this EntityBase item, DataRow row)
        {
            //PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(item.GetType());
            //foreach (PropertyDescriptor prop in properties)
            //{
            //    try
            //    {
            //        if (row.Table.Columns.Contains(prop.Name))
            //        {
            //            //row[prop.Name] = item.GetData(prop.Name) ?? DBNull.Value;
            //            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        row[prop.Name] = DBNull.Value;
            //    }
            //}
            //return row;

            foreach (DataColumn col in row.Table.Columns)
            {
                try
                {
                    row[col.ColumnName] = item.GetData(col.ColumnName) ?? DBNull.Value;
                }
                catch (Exception ex)
                {
                    row[col.ColumnName] = DBNull.Value;
                }
            }
            return row;
        }
    }
}
