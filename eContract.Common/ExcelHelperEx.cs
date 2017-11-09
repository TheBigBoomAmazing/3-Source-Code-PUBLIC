using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using XLS = NPOI.HSSF.UserModel;
using NPOI.XSSF;
using NPOI.SS.Util;
using NPOI.HSSF.UserModel;
using System.Text;

namespace eContract.Common
{
    public class ExcelHelperEx 
    {
        /// <summary>
        /// Excel样式
        /// </summary>
        public static class ExcelCellStyle
        {
            public const string Style1 = "Style1";
            public const string Style2 = "Style2";
            public const string Style3 = "Style3";
            public const string Style4 = "Style4";
            public const string Style5 = "Style5";
            public const string Style6 = "Style6";
            public const string Style7 = "Style7";
            public const string Style8 = "Style8";
            public const string Style9 = "Style9";
            public const string Style10 = "Style10";
            public const string Style11 = "Style11";
            public const string Style12 = "Style12";
        }

        /// <summary>
        /// 当前样式
        /// </summary>
        public static Dictionary<string, ICellStyle> CurrentWorkbookCellStyles
        {
            get;
            private set;
        }

        public class ExcelContext
        {
            public string HeaderBackground { get; set; }
            public ColumnList Columns { get; set; }
            public DataTable Data { get; set; }
            public string FileName { get; set; }
            public string Title { get; set; }
            public string Condition { get; set; }
            public bool ShowTitle { get; set; }

            public ExcelContext()
            {
                FileName = string.Empty;
                Title = string.Empty;
                Condition = string.Empty;
                ShowTitle = true;
                Columns = new ColumnList();
            }

            public void AddColumns(ColumnList columns)
            {
                if (columns != null)
                {
                    foreach (var item in columns)
                    {
                        Columns.Add((ExcelColumn)item);
                    }
                }
            }
        }

        public class ColumnList : List<ExcelColumn>
        {
            public ExcelColumn this[string key]
            {
                get
                {
                    foreach (var item in this)
                    {
                        if (item.Key == key)
                            return item;
                    }
                    return null;
                }
            }

            public void Add(ColumnList list)
            {
                foreach (var item in list)
                {
                    this.Add(item);
                }
            }

            public void Add(string key, string name)
            {
                ExcelColumn col = new ExcelColumn();
                col.Key = key;
                col.Name = name;
                Add(col);
            }

            public bool ContainsKey(string key)
            {
                foreach (var item in this)
                {
                    if (item.Key == key)
                    {
                        return true;
                    }
                }
                return false;
            }
        }


        public static string ToHtml(ExcelContext excelContext)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<style>.xlsText{mso-number-format:""@"";}</style>")
                .Append(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />")
                .Append(@"<table border=""1"" align=""center"" cellspacing=""1"" cellpadding=""1"" width=""100%"">")
            .Append(GetHtmlHeader(excelContext))
            .Append(GetHtmlBody(excelContext))
            .Append("</table>");
            return sb.ToString();
        }


        static StringBuilder GetHtmlHeader(ExcelContext excelContext)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<thead>");
            if (!string.IsNullOrEmpty(excelContext.Title))
            {
                sb.Append(@"<tr><th align=""center"" colspan=""")
                    .Append(excelContext.Columns.Count)
                    .Append(@"""><font size=""+1"" ");
                if (!string.IsNullOrEmpty(excelContext.HeaderBackground))
                {
                    sb.Append(" color=\"").Append(excelContext.HeaderBackground).Append("\"");
                }
                sb.Append(">").Append(excelContext.Title)
                    .Append(@"</font></th></tr>");
            }
            if (!string.IsNullOrEmpty(excelContext.Condition))
            {
                sb.Append(@"<tr><td colspan=""")
                    .Append(excelContext.Columns.Count)
                    .Append(@""" ><font size=""+1"">统计条件:</font>")
                    .Append(excelContext.Condition).Append(@"</td></tr>");
            };
            sb.Append(@"</thead>");
            sb.Append("<tr>");
            foreach (var item in excelContext.Columns)
            {
                sb.Append(@"<th style=""text-align:center; background:#F3C458;"">")
                  .Append(item.Name)
                  .Append("</th>");
            }
            sb.Append("</tr>");
            return sb;
        }

        /// <summary>
        /// 创建HTML主体
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        static StringBuilder GetHtmlBody(ExcelContext excelContext)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in excelContext.Data.Rows)
            {
                sb.Append("<tr>");
                foreach (var column in excelContext.Columns)
                {
                    sb.Append(@"<td style=""text-align:center;"" width=""20%"" class=""xlsText"">");
                    sb.Append(ValueFormat(dr[column.Key], column.Format));
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            return sb;
        }

        /// <summary>
        /// 格式化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        static object ValueFormat(object obj, string format)
        {
            if (obj == DBNull.Value) return string.Empty;
            if (obj is string)
            {
                if (!string.IsNullOrEmpty(format))
                {
                    return string.Format(format, obj);
                }
                else
                {
                    return (string)obj;
                }
            }
            else if (obj is decimal)
            {
                if (!string.IsNullOrEmpty(format))
                {
                    return ((decimal)obj).ToString(format);
                }
                else
                {
                    return (decimal)obj;
                }
            }
            else if (obj is int)
            {
                if (!string.IsNullOrEmpty(format))
                {
                    return ((int)obj).ToString(format);
                }
                else
                {
                    return (int)obj;
                }
            }
            else if (obj is long)
            {
                if (!string.IsNullOrEmpty(format))
                {
                    return ((long)obj).ToString(format);
                }
                else
                {
                    return (long)obj;
                }
            }
            else if (obj is float)
            {
                if (!string.IsNullOrEmpty(format))
                {
                    return ((float)obj).ToString(format);
                }
                else
                {
                    return (float)obj;
                }
            }
            else if (obj is DateTime)
            {
                if (!string.IsNullOrEmpty(format))
                {
                    return ((DateTime)obj).ToString(format);
                }
                else
                {
                    return (DateTime)obj;
                }
            }
            else { return obj.ToString(); }
        }

        public class ExcelColumn
        {
            public string Key { get; set; }
            public string Name { get; set; }
            public string Format { get; set; }
        }

        /// <summary>
        /// Excel版本
        /// </summary>
        public enum ExcelVersion
        {
            /// <summary>
            /// Excel2003(xls格式)
            /// </summary>
            xls,

            /// <summary>
            /// Excel2007(xlsx格式)
            /// </summary>
            xlsx,

            /// <summary>
            /// 未知版本
            /// </summary>
            unknow,
        }

        /// <summary>
        /// 样式模板
        /// </summary>
        public static Dictionary<string, ICellStyle> CellStyles
        {
            get;
            private set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExcelHelperEx()
        {
            CellStyles = new Dictionary<string, ICellStyle>();
            var styleFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\ExcelStyle.xlsx";
            LoadExcelStyle(styleFilePath);
        }

        /// <summary>
        /// 获取Excel版本
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static ExcelVersion GetExcelVersion(string file)
        {
            var res = ExcelVersion.unknow;
            FileInfo fi = new FileInfo(file);
            switch (fi.Extension.ToLower())
            {
                case ".xls":
                    res = ExcelVersion.xls;
                    break;
                case ".xlsx":
                    res = ExcelVersion.xlsx;
                    break;
            }
            return res;
        }

        /// <summary>
        /// 加载Excel模板样式
        /// </summary>
        /// <param name="file"></param>
        private static void LoadExcelStyle(string file)
        {
            var version = GetExcelVersion(file);
            if (version == ExcelVersion.xls)
            {
                LoadExcelStyleForXLS(file);
            }
            else if (version == ExcelVersion.xlsx)
            {
                LoadExcelStyleForXLSX(file);
            }
        }

        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTable(string file)
        {
            var version = GetExcelVersion(file);
            if (version == ExcelVersion.xls)
                return ExcelToTableForXLS(file);
            else if (version == ExcelVersion.xlsx)
                return ExcelToTableForXLSX(file);
            return null;
        }

        public static ISheet GetExcelSheet(string file, string sheetName, ref string fileVersion)
        {
            var version = GetExcelVersion(file);
            fileVersion = version.ToString();
            if (version == ExcelVersion.xls)
                return GetExcelSheetForXLS(file, sheetName);
            else if (version == ExcelVersion.xlsx)
                return GetExcelSheetForXLSX(file, sheetName);
            return null;
        }

        public static object GetCellValueFromSheet(string fileVersion, ISheet sheet, int row, int col)
        {
            if (fileVersion == ExcelVersion.xls.ToString())
            {
                return GetValueTypeForXLS(sheet.GetRow(row).GetCell(col) as XLS.HSSFCell);
            }
            else if (fileVersion == ExcelVersion.xlsx.ToString())
            {
                return GetValueTypeForXLSX(sheet.GetRow(row).GetCell(col) as XSSFCell);
            }
            return null;
        }

        public static ISheet GetExcelSheetForXLS(string file, string sheetName)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XLS.HSSFWorkbook hssfworkbook = new XLS.HSSFWorkbook(fs);
                ISheet sheet = hssfworkbook.GetSheet(sheetName);
                return sheet;
            }
        }

        public static ISheet GetExcelSheetForXLSX(string file, string sheetName)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                ISheet sheet = xssfworkbook.GetSheet(sheetName);
                return sheet;
            }
        }




        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTable(string file, int startRowIndex)
        {
            var version = GetExcelVersion(file);
            if (version == ExcelVersion.xls)
                return ExcelToTableForXLS(file, startRowIndex);
            else if (version == ExcelVersion.xlsx)
                return ExcelToTableForXLSX(file, startRowIndex);
            return null;
        }


        /// <summary>
        /// 将DataTable数据导出到Excel文件中(在导出时根据目标文件的格式应用相应格式的模板样式)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file"></param>
        public static byte[] TableToExcel(DataTable dt, string file)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string styleFile = string.Empty;
            CellStyles.Clear();

            var version = GetExcelVersion(file);
            if (version == ExcelVersion.xls)
            {
                styleFile = Path.Combine(path, "ExcelStyle.xls");
                LoadExcelStyleForXLS(styleFile);
                return TableToExcelForXLS(dt, file);
            }
            else if (version == ExcelVersion.xlsx)
            {
                styleFile = Path.Combine(path, "ExcelStyle.xlsx");
                //LoadExcelStyleForXLSX(styleFile);
                return TableToExcelForXLSX(dt, file);
            }
            return null;

        }

        #region Excel2003
        /// <summary>
        /// 加载Excel模板样式(xls)
        /// </summary>
        /// <param name="file"></param>
        private static void LoadExcelStyleForXLS(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XLS.HSSFWorkbook hssfworkbook = new XLS.HSSFWorkbook(fs);
                ISheet s = hssfworkbook.GetSheetAt(0);
                for (int i = s.FirstRowNum; i <= s.LastRowNum; i++)
                {
                    ICell c0 = s.GetRow(i).GetCell(0);
                    ICell c1 = s.GetRow(i).GetCell(1);
                    CellStyles.Add(c0.StringCellValue, c1.CellStyle);
                    if (c0.StringCellValue == "DateTime")
                    {
                        IDataFormat format = hssfworkbook.CreateDataFormat();
                        CellStyles["DateTime"].DataFormat = format.GetFormat(c1.StringCellValue);
                    }
                }
            }
        }



        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中(xls)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableForXLS(string file)
        {
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XLS.HSSFWorkbook hssfworkbook = new XLS.HSSFWorkbook(fs);
                ISheet sheet = hssfworkbook.GetSheetAt(0);

                //表头
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueTypeForXLS(header.GetCell(i) as XLS.HSSFCell);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        //continue;
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据
                for (int i = sheet.FirstRowNum +1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        try
                        {
                            dr[j] = GetValueTypeForXLS(sheet.GetRow(i).GetCell(j) as XLS.HSSFCell);
                            if (dr[j] != null && dr[j].ToString() != string.Empty)
                            {
                                hasValue = true;
                            }
                        }
                        catch { }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }


        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中(xls)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetRowCellData(string file, int rowIndex, int cellIndex)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XLS.HSSFWorkbook hssfworkbook = new XLS.HSSFWorkbook(fs);
                ISheet sheet = hssfworkbook.GetSheetAt(0);

                var cell = sheet.GetRow(rowIndex).GetCell(cellIndex);
                return cell.StringCellValue;
            }
        }

        public static void ExportExcelForXLSml(DataTable dt, string filePath, string fileOut)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();

            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            //ISheet sheet = hssfworkbook.GetSheet("WareHouseProportion");
            ISheet sheet = hssfworkbook.GetSheetAt(0);

            ////表头
            //IRow row = sheet.CreateRow(0);
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    ICell cell = row.CreateCell(i);
            //    cell.SetCellValue(dt.Columns[i].ColumnName);
            //    cell.SetCellValue(dt.Columns[i].ColumnName); 
            //}
            //数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    double value = 0;
                    if (double.TryParse(dt.Rows[i][j].ToString(), out value))
                    {
                        cell.SetCellValue(Convert.ToDouble(dt.Rows[i][j]));
                    }
                    else
                    {
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
            }

            //转为字节数组
            MemoryStream stream = new MemoryStream();
            hssfworkbook.Write(stream);
            var buf = stream.ToArray();
            //return buf;
            //保存为Excel文件
            using (FileStream fs = new FileStream(fileOut, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }
        }

        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中(xls)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableForXLS(string file, int startRowIndex)
        {
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XLS.HSSFWorkbook hssfworkbook = new XLS.HSSFWorkbook(fs);
                ISheet sheet = hssfworkbook.GetSheetAt(0);

                //表头
                IRow header = sheet.GetRow(startRowIndex);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueTypeForXLS(header.GetCell(i) as XLS.HSSFCell);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        //continue;
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据
                for (int i = startRowIndex + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueTypeForXLS(sheet.GetRow(i).GetCell(j) as XLS.HSSFCell);
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 将DataTable数据导出到Excel文件中(xls)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file"></param>
        public static byte[] TableToExcelForXLS(DataTable dt, string file)
        {
            XLS.HSSFWorkbook hssfworkbook = new XLS.HSSFWorkbook();
            ISheet sheet = hssfworkbook.CreateSheet("Data");

            ICellStyle style = hssfworkbook.CreateCellStyle();
            style.CloneStyleFrom(CellStyles[ExcelCellStyle.Style1]);

            //表头
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
                cell.CellStyle = style;
            }

            //数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组
            MemoryStream stream = new MemoryStream();
            hssfworkbook.Write(stream);
            var buf = stream.ToArray();
            return buf;
            ////保存为Excel文件
            //using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            //{
            //    fs.Write(buf, 0, buf.Length);
            //    fs.Flush();
            //}
        }

        /// <summary>
        /// 获取单元格类型(xls)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueTypeForXLS(XLS.HSSFCell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.BLANK: //BLANK:
                    return null;
                case CellType.BOOLEAN: //BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.NUMERIC: //NUMERIC:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue.ToString("yyyy-MM-dd");
                    }
                    return cell.NumericCellValue;
                case CellType.STRING: //STRING:
                    return cell.StringCellValue.Trim();
                case CellType.ERROR: //ERROR:
                    return cell.ErrorCellValue;
                case CellType.FORMULA: //FORMULA:
                default:
                    return cell.NumericCellValue;
            }
        }
        #endregion

        #region Excel2007
        /// <summary>
        /// 加载Excel模板样式(xlsx)
        /// </summary>
        /// <param name="file"></param>
        private static void LoadExcelStyleForXLSX(string file)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                ISheet s = xssfworkbook.GetSheetAt(0);
                for (int i = s.FirstRowNum; i <= s.LastRowNum; i++)
                {
                    ICell c0 = s.GetRow(i).GetCell(0);
                    ICell c1 = s.GetRow(i).GetCell(1);
                    CellStyles.Add(c0.StringCellValue, c1.CellStyle);
                    if (c0.StringCellValue == "DateTime")
                    {
                        IDataFormat format = xssfworkbook.CreateDataFormat();
                        CellStyles["DateTime"].DataFormat = format.GetFormat(c1.StringCellValue);
                    }
                }
            }
        }

        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中(xlsx)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableForXLSX(string file)
        {
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                ISheet sheet = xssfworkbook.GetSheetAt(0);

                //表头
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        //continue;
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        try
                        {
                            IRow dataRow = sheet.GetRow(i);
                            if (dataRow != null)
                            {
                                dr[j] = GetValueTypeForXLSX(dataRow.GetCell(j) as XSSFCell);
                                if (dr[j] != null && dr[j].ToString() != string.Empty)
                                {
                                    hasValue = true;
                                }
                            }
                        }
                        catch { }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中(xlsx)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ExcelToTableForXLSX(string file, int startRowIndex)
        {
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                ISheet sheet = xssfworkbook.GetSheetAt(0);

                //表头
                IRow header = sheet.GetRow(startRowIndex);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        //continue;
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据
                for (int i = startRowIndex + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        IRow dataRow = sheet.GetRow(i);
                        if (dataRow != null)
                        {
                            dr[j] = GetValueTypeForXLSX(dataRow.GetCell(j) as XSSFCell);
                            if (dr[j] != null && dr[j].ToString() != string.Empty)
                            {
                                hasValue = true;
                            }
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 将DataTable数据导出到Excel文件中(xlsx)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file"></param>
        public static byte[] TableToExcelForXLSX(DataTable dt, string file)
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook();
            ISheet sheet = xssfworkbook.CreateSheet("Test");

            ICellStyle style = xssfworkbook.CreateCellStyle();
            //style.CloneStyleFrom(CellStyles[ExcelCellStyle.Style1]);

            //表头
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
                //cell.CellStyle =style;
            }

            //数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组
            MemoryStream stream = new MemoryStream();
            xssfworkbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
            }

            return buf;
        }

        /// <summary>
        /// 获取单元格类型(xlsx)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueTypeForXLSX(XSSFCell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.BLANK: //BLANK:
                    return null;
                case CellType.BOOLEAN: //BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.NUMERIC: //NUMERIC:
                    {
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            // 如果是date类型则 ，获取该cell的date值
                            return cell.DateCellValue.ToString("yyyy-MM-dd");
                        }
                        else
                        {// 纯数字
                            return cell.NumericCellValue;
                        }
                    }
                case CellType.STRING: //STRING:
                    return cell.StringCellValue.Trim();
                case CellType.ERROR: //ERROR:
                    return cell.ErrorCellValue;
                case CellType.FORMULA: //FORMULA:
                default:
                    return cell.NumericCellValue;
            }
        }

        public static object GetValue(HSSFCell cell, HSSFWorkbook workbook)
        {
            if (cell == null)
                return null;

            switch (cell.CellType)
            {
                case CellType.BLANK: //BLANK:
                    return null;
                case CellType.BOOLEAN: //BOOLEAN:
                    return cell.BooleanCellValue;
                case CellType.NUMERIC: //NUMERIC:
                    {
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            // 如果是date类型则 ，获取该cell的date值
                            return cell.DateCellValue.ToString("yyyy-MM-dd");
                        }
                        else
                        {// 纯数字
                            return cell.NumericCellValue;
                        }
                    }
                case CellType.STRING: //STRING:
                    return cell.StringCellValue;
                case CellType.ERROR: //ERROR:
                    return cell.ErrorCellValue;
                case CellType.FORMULA: //FORMULA:
                default:
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(workbook);
                        CellValue cellValue = e.Evaluate(cell);
                        switch (cellValue.CellType)
                        {
                            case CellType.BLANK: //BLANK:
                                return null;
                            case CellType.BOOLEAN: //BOOLEAN:
                                return cell.BooleanCellValue;
                            case CellType.NUMERIC: //NUMERIC:
                                return cell.NumericCellValue;
                            case CellType.STRING: //STRING:
                                return cell.StringCellValue;
                            case CellType.ERROR: //ERROR:
                                return cell.ErrorCellValue;
                            default:
                                return "";
                        }
                    }
            }
        }


        #endregion

    }
}
