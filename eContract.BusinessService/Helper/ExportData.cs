using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Data;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System.Diagnostics;

namespace eContract.BusinessService.Helper
{
    public static class ExportData
    {
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

        /// <summary>
        /// 导出Xls
        /// </summary>
        /// <param name="localFilePath">文件保存路径</param>
        /// <param name="dtSource">数据源</param>
        public static void ExportXls(string localFilePath, System.Data.DataTable dtSource)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建Sheet，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();
                    }

                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }

                    }
                    #endregion
                    rowIndex = 1;
                }
                #endregion

                #region 填充内容
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);
                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                }
                #endregion
                rowIndex++;
            }

            using (FileStream fs = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }

        }
        /// <summary>
        /// 导出Xlsx
        /// </summary>
        /// <param name="localFilePath">文件保存路径</param>
        /// <param name="dtSource">数据源</param>
        public static void ExportXlsx(string localFilePath, System.Data.DataTable dtSource)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet();

            XSSFCellStyle dateStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            XSSFDataFormat format = (XSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (XSSFSheet)workbook.CreateSheet();
                    }

                    #region 列头及样式
                    {
                        XSSFRow headerRow = (XSSFRow)sheet.CreateRow(0);
                        XSSFCellStyle headStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                        headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                        XSSFFont font = (XSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                    }
                    #endregion
                    rowIndex = 1;
                }
                #endregion

                #region 填充内容
                XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    XSSFCell newCell = (XSSFCell)dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);
                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion
                rowIndex++;
            }
            using (FileStream fs = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }
        }
        /// <summary>
        /// 导出Docx
        /// </summary>
        /// <param name="localFilePath">文件保存路径</param>
        /// <param name="dtSource">数据源</param>
        public static void ExportDocx(string localFilePath, System.Data.DataTable dtSource)
        {

            XWPFDocument doc = new XWPFDocument();

            XWPFTable table = doc.CreateTable(dtSource.Rows.Count + 1, dtSource.Columns.Count);

            for (int i = 0; i < dtSource.Rows.Count + 1; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    if (i == 0)
                    {
                        table.GetRow(i).GetCell(j).SetText(dtSource.Columns[j].ColumnName);
                    }
                    else
                    {
                        table.GetRow(i).GetCell(j).SetText(dtSource.Rows[i - 1][j].ToString());
                    }
                }
            }

            using (FileStream fs = new FileStream(localFilePath, FileMode.Create, FileAccess.Write))
            {
                doc.Write(fs);
            }
        }

        #region Extend Method
        public static byte[] SetFileToByteArray(string fileName)
        {
            byte[] bytes = null;
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                FileInfo fileInfo = new FileInfo(fileName);
                //fileSize = Convert.ToDecimal(fileInfo.Length / 1024).ToString("f2") + " K";
                int streamLength = (int)fs.Length;
                bytes = new byte[streamLength];
                fs.Read(bytes, 0, streamLength);
                fs.Close();
                return bytes;
            }
            catch
            {
                return bytes;
            }
        }

        public static void KillProcess(string processName)
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process(); 　　　　　　//得到所有打开的进程　
            try
            {
                var arr = Process.GetProcessesByName(processName);
                if (arr != null && arr.Length > 0)
                {
                    foreach (Process thisproc in arr)
                    {
                        if (!thisproc.CloseMainWindow())
                        {
                            if (thisproc != null)
                                thisproc.Kill();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}
