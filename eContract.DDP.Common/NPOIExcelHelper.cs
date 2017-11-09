using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using System.Data;
using NPOI.SS.UserModel;
using System.Reflection;
using NPOI.SS.Util;
using System.IO;

namespace eContract.DDP.Common
{
    public class NPOIExcelHelper
    {
        public const string Extensions = ".xls";
        public NPOIExcelHelper()
        {
            this.Hssfworkbook = new HSSFWorkbook();
        }
        /// <summary>
        /// 
        /// </summary>
        private HSSFWorkbook Hssfworkbook { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        private string FilePath { get; set; }

        /// <summary>
        /// 添加工作薄
        /// </summary>
        /// <param name="sheetName"></param>
        public HSSFSheet AddSheet(string sheetName)
        {
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = "sheet" + Hssfworkbook.Workbook.NumSheets;
            }
            return (HSSFSheet)Hssfworkbook.CreateSheet(sheetName);
        }
        /// <summary>
        /// 获取工作薄
        /// </summary>
        /// <param name="sheetName">工作薄名称</param>
        /// <returns>工作薄</returns>
        public HSSFSheet GetSheet(string sheetName)
        {
            return (HSSFSheet)Hssfworkbook.GetSheet(sheetName);
        }

        /// <summary>
        /// 获取工作薄
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>工作薄</returns>
        public HSSFSheet GetSheet(int index)
        {
            return (HSSFSheet)Hssfworkbook.GetSheetAt(index);
        }



        /// <summary>
        /// 添加row
        /// </summary>
        /// <param name="index">index</param>
        public void AddRow(HSSFSheet sheet, int index)
        {
            sheet.CreateRow(index);
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="table">数据源</param>
        public void BindDataSource(DataTable table)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            BindDataSource(ds);
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="list">对应列名</param>
        public void BindDataSource(DataTable table, Dictionary<string, string> list)
        {
            if (table == null) return;
            AddSheet(table.TableName);
            BindSheetData(table, list);
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="ds">数据源</param>
        public void BindDataSource(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0) return;
            foreach (DataTable table in ds.Tables)
            {
                AddSheet(table.TableName);
                BindSheetData(table);
            }
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <typeparam name="T">集合的基类</typeparam>
        /// <param name="list">数据源</param>
        /// <param name="sheetName">工作薄名称</param>
        public void BindDataSource<T>(List<T> list, string sheetName)
        {
            BindDataSource(list, sheetName, null);
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">工作薄</param>
        /// <param name="region">合并区域</param>
        public void Merged(HSSFSheet sheet, Region region)
        {
            sheet.AddMergedRegion(region);
        }

        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="style"></param>
        public void SetStyle(HSSFCell cell, HSSFCellStyle style)
        {
            InitializeStyle(style);
            cell.CellStyle = style;
        }

        /// <summary>
        /// 设置字体
        /// </summary>
        /// <param name="font">字体</param>
        /// <returns>样式</returns>
        public HSSFCellStyle SetFont(HSSFCellStyle style, HSSFFont font)
        {
            InitializeStyle(style);
            style.SetFont(font);
            return style;
        }

        /// <summary>
        /// 初始化样式
        /// </summary>
        /// <param name="style"></param>
        public void InitializeStyle(HSSFCellStyle style)
        {
            if (style == null)
            {
                style = (HSSFCellStyle)Hssfworkbook.CreateCellStyle();
            }
        }

        /// <summary>
        /// 设置单元格居中
        /// </summary>
        /// <returns></returns>
        public HSSFCellStyle SetCenter(HSSFCellStyle style)
        {
            InitializeStyle(style);
            style.Alignment = HorizontalAlignment.CENTER;
            return style;
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <typeparam name="T">集合的基类</typeparam>
        /// <param name="list">数据源</param>
        /// <param name="sheetName">工作薄名称</param>
        /// <param name="properties">需要的属性</param>
        public void BindDataSource<T>(List<T> list, string sheetName, Dictionary<string, string> properties)
        {
            if (list == null) return;
            HSSFSheet sheet = AddSheet(sheetName);
            HSSFRow _row = null;
            T obj = (T)Activator.CreateInstance(typeof(T));

            if (properties == null)
            {   //直接绑定所有属性
                PropertyInfo[] objProperty = obj.GetType().GetProperties();
                #region 添加列头
                _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum);
                for (int i = 0; i < objProperty.Length; i++)
                {
                    _row.CreateCell(i, CellType.STRING).SetCellValue(objProperty[i].Name);
                }
                #endregion
                #region 绑定数据源
                object val;
                foreach (T temp in list)
                {
                    _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum + 1);
                    for (int i = 0; i < objProperty.Length; i++)
                    {
                        val = objProperty[i].GetValue(temp, null);
                        //字符串属性
                        if (objProperty[i].PropertyType == typeof(string))
                        {
                            _row.CreateCell(i, CellType.STRING).SetCellValue(val == null ? "" : val.ToString());
                        }
                        else if (objProperty[i].PropertyType == typeof(int))
                        {
                            _row.CreateCell(i, CellType.NUMERIC).SetCellValue(val == null ? 0 : int.Parse(val.ToString()));
                        }
                        else if (objProperty[i].PropertyType == typeof(double) || objProperty[i].PropertyType == typeof(decimal))
                        {
                            _row.CreateCell(i, CellType.NUMERIC).SetCellValue(val == null ? 0 : double.Parse(val.ToString()));
                        }
                        else if (objProperty[i].PropertyType == typeof(DateTime))
                        {
                            _row.CreateCell(i, CellType.STRING).SetCellValue(val == null ? "" : val.ToString());
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region 添加列头
                _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum);
                int index = 0;
                foreach (string key in properties.Keys)
                {
                    _row.CreateCell(index, CellType.STRING).SetCellValue(properties[key]);
                    index++;
                }
                #endregion
                #region 绑定数据源
                object val;
                PropertyInfo info;
                foreach (T temp in list)
                {
                    index = 0;
                    _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum + 1);
                    foreach (string key in properties.Keys)
                    {
                        info = temp.GetType().GetProperty(key);
                        if (info == null)
                        {
                            index++;
                            continue;
                        }
                        val = info.GetValue(temp, null);

                        if (info.PropertyType == typeof(string))
                        {
                            _row.CreateCell(index, CellType.STRING).SetCellValue(val == null ? "" : val.ToString());
                        }
                        else if (info.PropertyType == typeof(int))
                        {
                            _row.CreateCell(index, CellType.NUMERIC).SetCellValue(val == null ? 0 : int.Parse(val.ToString()));
                        }
                        else if (info.PropertyType == typeof(double) || info.PropertyType == typeof(decimal))
                        {
                            _row.CreateCell(index, CellType.NUMERIC).SetCellValue(val == null ? 0 : double.Parse(val.ToString()));
                        }
                        else if (info.PropertyType == typeof(DateTime))
                        {
                            _row.CreateCell(index, CellType.STRING).SetCellValue(val == null ? "" : val.ToString());
                        }
                        index++;
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="table">数据源</param>
        public void BindSheetData(DataTable table)
        {
            BindSheetData(table, null);
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="list">绑定字段对应</param>
        public void BindSheetData(DataTable table, Dictionary<string, string> list)
        {
            if (table == null) return;
            HSSFSheet sheet = GetSheet(Hssfworkbook.Workbook.NumSheets - 1);
            if (sheet == null)
                sheet = AddSheet(table.TableName);
            HSSFRow _row = null;
            int index = 0;
            if (list != null)
            {   //有选择列头情况
                #region 添加列头
                _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum);
                foreach (string key in list.Keys)
                {
                    _row.CreateCell(index, CellType.STRING).SetCellValue(list[key]);
                    index++;
                }
                #endregion
                #region 绑定数据源
                foreach (DataRow row in table.Rows)
                {
                    index = 0;
                    _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum + 1);
                    foreach (string key in list.Keys)
                    {
                        try
                        {
                            _row.CreateCell(index, CellType.STRING).SetCellValue(row[key].ToString());
                        }
                        catch { }
                        index++;
                    }
                }
                #endregion
            }
            else
            {
                #region 添加列头
                _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum);
                foreach (DataColumn column in table.Columns)
                {
                    _row.CreateCell(index, CellType.STRING).SetCellValue(column.ColumnName);
                    index++;
                }
                #endregion
                #region 直接绑定所有数据
                foreach (DataRow row in table.Rows)
                {
                    _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum + 1);
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        _row.CreateCell(i, CellType.STRING).SetCellValue(row[i].ToString());
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        public void CreateFile(string filePath)
        {
            CreateFile(filePath, FileMode.OpenOrCreate);
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="mode">保存方式</param>
        public void CreateFile(string filePath, FileMode mode)
        {
            FilePath = filePath;
            VerifyFileName();
            FileStream file = new FileStream(FilePath, mode);
            Hssfworkbook.Write(file);
            file.Close();
        }


        /// <summary>
        /// 验证路径名称
        /// </summary>
        /// <param name="filePath">路径名称</param>
        /// <returns></returns>
        bool VerifyFileName()
        {
            string directoryName = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            if (FilePath.Contains(Extensions))
                return true;
            FilePath = FilePath + Extensions;
            return false;
        }


        public static void OutPutFile(string filePath, string fileName)
        {
            if (!File.Exists(HttpContext.Current.Server.MapPath(filePath)))
            {
                //throw new Exception(ToolsConfiguration.FileNotExist);
            }
            //根据文件路径获取文件名称
            //string fileName = Path.GetFileName(filePath);
            //获取客户端浏览器信息
            string browser = HttpContext.Current.Request.UserAgent.ToUpper();
            //判断浏览器,选择对应的输出文件名格式
            if (browser.Contains("MS") == true && browser.Contains("IE") == true)
            {
                fileName = HttpUtility.UrlEncode(fileName);
            }
            else if (browser.Contains("FIREFOX") == true)
            {
                fileName = "\"" + fileName + "\"";
            }
            else
            {
                fileName = HttpUtility.UrlEncode(fileName);
            }
            //读取文件流
            FileStream fileStream = File.OpenRead(HttpContext.Current.Server.MapPath(filePath));
            long fileSize = fileStream.Length;
            byte[] fileBuffer = new byte[fileSize];
            fileStream.Read(fileBuffer, 0, (int)fileSize);
            //读取完成之后要关闭
            fileStream.Close();
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());

            HttpContext.Current.Response.BinaryWrite(fileBuffer);
            HttpContext.Current.Response.End();
            HttpContext.Current.Response.Close();
        }

        /// <summary>
        /// 样式模板
        /// </summary>
        public static Dictionary<string, ICellStyle> CellStyles
        {
            get;
            private set;
        }

        public static Dictionary<string, ICellStyle> CurrentWorkbookCellStyles
        {
            get;
            private set;
        }
    }
}