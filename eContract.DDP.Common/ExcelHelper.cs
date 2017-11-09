using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System.Drawing;
using NPOI.SS.Util;

namespace eContract.DDP.Common
{
    /// <summary>
    /// Excel导出导入帮助类
    /// </summary>  
    public class ExcelHelper
    {
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
            public const string Style13 = "Style13";
            public const string Style14 = "Style14";
            public const string Style15 = "Style15";
            public const string Style16 = "Style16";
            public const string Style17 = "Style17";
            public const string Style18 = "Style18";
            public const string Style19 = "Style19";
            public const string Style20 = "Style20";
            public const string Style21 = "Style21";
            public const string Style22 = "Style22";
            public const string Style23 = "Style23";
            public const string Style24 = "Style24";

            //order 
            public const string Style25 = "Style25";
            public const string Style26 = "Style26";
            public const string Style27 = "Style27";
            public const string Style28 = "Style28";
            public const string Style29 = "Style29";
            public const string Style30 = "Style30";
            public const string Style31 = "Style31";
            public const string Style32 = "Style32";
            public const string Style33 = "Style33";
            public const string Style34 = "Style34";

            //LT
            public const string Style35 = "Style35";
            public const string Style36 = "Style36";
            public const string Style37 = "Style37";
            public const string Style38 = "Style38";
            public const string Style39 = "Style39";

            //STA_CODE and OTB
            public const string Style40 = "Style40";
            public const string Style41 = "Style41";
            public const string Style42 = "Style42";
            public const string Style43 = "Style43";
            public const string Style44 = "Style44";
            public const string Style45 = "Style45";
            public const string Style46 = "Style46";
            public const string Style47 = "Style47";
            public const string Style48 = "Style48";
            public const string Style49 = "Style49";
            public const string Style50 = "Style50";
            public const string Style51 = "Style51";
            public const string Style52 = "Style52";
            public const string Style53 = "Style53";
            public const string Style54 = "Style54";
            public const string Style55 = "Style55";
            public const string Style56 = "Style56";
            public const string Style57 = "Style57";
            public const string Style58 = "Style58";
            public const string Style59 = "Style59";
            public const string Style60 = "Style60";
            public const string Style61 = "Style61";
            public const string Style62 = "Style62";
            public const string Style63 = "Style63";
            public const string Style64 = "Style64";
            public const string Style65 = "Style65";
            public const string Style66 = "Style66";
            public const string Style67 = "Style67";
            public const string Style68 = "Style68";
            public const string Style69 = "Style69";
            public const string Style70 = "Style70";
            public const string Style71 = "Style71";
            public const string Style72 = "Style72";
            public const string Style73 = "Style73";
            public const string Style74 = "Style74";

            public const string Style75 = "Style75";
            public const string Style76 = "Style76";
            public const string Style77 = "Style77";
            public const string Style78 = "Style78";

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

        static ExcelHelper()
        {
            CellStyles = new Dictionary<string, ICellStyle>();
            string path = string.Empty;
            if (HttpContext.Current != null)
            {
                path = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);
            }
            else
            {
                path = AppDomain.CurrentDomain.BaseDirectory;
            }
            using (FileStream file = new FileStream(Path.Combine(path, "ExcelStyle.xls"), FileMode.Open, FileAccess.Read))
            {
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
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

        public static byte[] Export(DataTable dt)
        {
            return Export(dt, 28 * 256);
        }

        public static byte[] Export(DataTable dt, int defaultColumnWidth)
        {
            HSSFWorkbook workbook = CreateWorkbook();

            CreateCurrentWorkbookStyles(workbook);

            ISheet sheet = workbook.CreateSheet("Sheet1");
            WriteTable(workbook, sheet, dt);
            IRow row = sheet.GetRow(0);
            for (int i = row.FirstCellNum; i < row.LastCellNum; i++)
            {
                sheet.SetColumnWidth(i, defaultColumnWidth);
            }

            return Render(workbook);
        }

        /// <summary>
        /// 创建用于当前工作簿的样式
        /// </summary>
        /// <param name="workbook"></param>
        private static void CreateCurrentWorkbookStyles(HSSFWorkbook workbook)
        {
            CurrentWorkbookCellStyles = new Dictionary<string, ICellStyle>();
            foreach (string csKey in CellStyles.Keys)
            {
                ICellStyle csNew = workbook.CreateCellStyle();
                csNew.CloneStyleFrom(CellStyles[csKey]);
                CurrentWorkbookCellStyles.Add(csKey, csNew);
            }
        }

        /// <summary>
        /// 输出成二进制
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public static byte[] Render(HSSFWorkbook workbook)
        {
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            return stream.ToArray();
        }

        /// <summary>
        /// 创建excel workbook
        /// </summary>
        /// <returns></returns>
        public static HSSFWorkbook CreateWorkbook()
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "eContract";
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "eContract Sunny";
            workbook.DocumentSummaryInformation = dsi;
            workbook.SummaryInformation = si;
            return workbook;
        }

        public static HSSFWorkbook CopyWorkbook(FileStream file)
        {
            //FileStream file = new FileStream(templatePath, FileMode.Open, FileAccess.Read);

            HSSFWorkbook workbook = new HSSFWorkbook(file);
            CreateCurrentWorkbookStyles(workbook);

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "eContract";
            workbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "eContract Sunny";
            workbook.SummaryInformation = si;
            return workbook;

        }

        /// <summary>
        /// 设置本次导出基本信息. Add by shakken xie on 2010-5-19
        /// </summary>
        /// <param name="wookBook">HSSFWorkbook 实例</param>
        /// <param name="projectId">当前ProjectId</param>
        /// <param name="userRole">当前用户RoleId</param>
        /// <param name="planInfoList">PlanId|PlanType值|PlanStatus值 列表</param>
        public static void WriteCurstomerCustomProperties(HSSFWorkbook wookBook, string projectId, string userRole, List<string> planInfoList)
        {
            string result = string.Empty;
            result = projectId + "\r\n" + userRole + "\r\n";
            foreach (string item in planInfoList)
            {
                result += item + "\r\n";
            }
            result = result.Substring(0, result.Length - 2);
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "CRM System";
            si.Comments = result;
            wookBook.SummaryInformation = si;
        }

        /// <summary>
        /// 创建表头样式, Add by shakken xie on 2010-5-19
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="row"></param>
        public static void SetTableHeader(HSSFWorkbook workbook, int rowIndex)
        {
            ICellStyle style1 = workbook.CreateCellStyle();
            style1.CloneStyleFrom(CellStyles[ExcelCellStyle.Style1]);
            IRow row = workbook.GetSheet("Sheet1").GetRow(rowIndex);
            for (int i = row.FirstCellNum; i < row.LastCellNum; i++)
            {
                row.GetCell(i).CellStyle = style1;
            }
        }

        /// <summary>
        /// 给Sheet加密码
        /// </summary>
        /// <param name="sheet"></param>
        public static void AddPassword(HSSFSheet sheet)
        {
            //sheet.ProtectSheet(Utils.MD5Encode(DateTime.Now.ToString("yyyyMMdd")).Substring(2, 8));
        }

        /// <summary>
        /// 给Sheet加密码
        /// </summary>
        /// <param name="sheet"></param>
        public static void SetAutoColumnsWidth(HSSFSheet sheet)
        {
            for (int columnNum = 0; columnNum <= 30; columnNum++)
            {
                int columnWidth = (sheet.GetColumnWidth(columnNum) / 256) + 8;
                for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
                {
                    IRow currentRow;
                    //当前行未被使用过   
                    if (sheet.GetRow(rowNum) == null)
                    {
                        currentRow = sheet.CreateRow(rowNum);
                    }
                    else
                    {
                        currentRow = sheet.GetRow(rowNum);
                    }

                    if (currentRow.GetCell(columnNum) != null)
                    {
                        ICell currentCell = currentRow.GetCell(columnNum);
                        int length = System.Text.Encoding.Default.GetBytes(currentCell.ToString()).Length + 5;
                        if (columnWidth < length)
                        {
                            columnWidth = length;
                        }
                    }
                }
                sheet.SetColumnWidth(columnNum, columnWidth * 256);
            }

        }

        public static void SetColumnsWidth(HSSFSheet sheet,int iWidth)
        {
            for (int columnNum = 0; columnNum <= 30; columnNum++)
            {

                sheet.SetColumnWidth(columnNum, iWidth * 256);
            }

        }

        public static void SetAutoColumnsWidth(HSSFSheet sheet, int iColunms)
        {
            for (int columnNum = 0; columnNum <= iColunms; columnNum++)
            {
                int columnWidth = (sheet.GetColumnWidth(columnNum) / 256) + 8;
                for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
                {
                    IRow currentRow;
                    //当前行未被使用过   
                    if (sheet.GetRow(rowNum) == null)
                    {
                        currentRow = sheet.CreateRow(rowNum);
                    }
                    else
                    {
                        currentRow = sheet.GetRow(rowNum);
                    }

                    if (currentRow.GetCell(columnNum) != null)
                    {
                        ICell currentCell = currentRow.GetCell(columnNum);
                        int length = System.Text.Encoding.Default.GetBytes(currentCell.ToString()).Length + 5;
                        if (columnWidth < length)
                        {
                            columnWidth = length;
                        }
                    }
                }
                sheet.SetColumnWidth(columnNum, columnWidth * 256);
            }

        }


        /// <summary>
        /// 设置行高
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="height"></param>
        public static void SetRowHeight(ISheet sheet, int rowIndex, float height)
        {
            IRow row = sheet.GetRow(rowIndex);
            if (row == null)
            {
                row = sheet.CreateRow(rowIndex);
            }
            row.HeightInPoints = height;
        }

        /// <summary>
        /// 设置单元格宽度
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="height"></param>
        public static void SetCellWidth(ISheet sheet, int columnIndex, int width)
        {
            sheet.SetColumnWidth(columnIndex, width * 256);
        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="sheet"></param>
        /// <param name="columnIndex"></param>
        public static void UnlockColumn(HSSFWorkbook workbook, HSSFSheet sheet, int columnIndex)
        {
            ICellStyle unlocked = workbook.CreateCellStyle();
            unlocked.IsLocked = false;
            for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
            {
                sheet.GetRow(i).GetCell(columnIndex).CellStyle = unlocked;
            }
        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="sheet"></param>
        /// <param name="columnIndex"></param>
        public static void UnlockCell(HSSFWorkbook workbook, ISheet sheet, int rowIndex, int columnIndex)
        {
            ICellStyle unlocked = workbook.CreateCellStyle();
            unlocked.IsLocked = false;
            sheet.GetRow(rowIndex).GetCell(columnIndex).CellStyle = unlocked;
        }

        /// <summary>
        /// 往sheet写入table
        /// </summary>
        public static void WriteTable(HSSFWorkbook workbook, ISheet sheet, DataTable dt)
        {
            WriteTable(workbook, sheet, dt, 0);
        }

        /// <summary>
        /// 往sheet写入table
        /// </summary>
        public static void WriteTable(HSSFWorkbook workbook, ISheet sheet, DataTable dt, int rowIndex)
        {
            WriteTable(workbook, sheet, dt, rowIndex, 0);
        }

        /// <summary>
        /// 往sheet写入table
        /// </summary>
        public static void WriteTable(HSSFWorkbook workbook, ISheet sheet, DataTable dt, int rowIndex, int columnIndex)
        {
            WriteTable(workbook, sheet, dt, rowIndex, columnIndex, true, false);
        }

        /// <summary>
        /// 往sheet写入table
        /// </summary>
        /// <param name="sheet">表单</param>
        /// <param name="dt">数据源</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="columnIndex">列号</param>
        /// <param name="writeHeader">是否写入表头</param>
        /// <param name="useCaption">若写入表头，是否使用Caption</param>
        public static void WriteTable(HSSFWorkbook workbook, ISheet sheet, DataTable dt, int rowIndex, int columnIndex, bool writeHeader, bool useCaption)
        {
            string[] header = new string[dt.Columns.Count];
            for (int i = 0; i < header.Length; i++)
            {
                if (useCaption)
                {
                    header[i] = dt.Columns[i].Caption;
                }
                else
                {
                    header[i] = dt.Columns[i].ColumnName;
                }
            }
            if (writeHeader)
            {
                ICellStyle style1 = workbook.CreateCellStyle();
                style1.CloneStyleFrom(CellStyles[ExcelCellStyle.Style1]);
                IRow row = WriteRow(sheet, header, rowIndex, columnIndex);
                for (int i = row.FirstCellNum; i < row.LastCellNum; i++)
                {
                    row.GetCell(i).CellStyle = style1;
                }
            }
            rowIndex++;
            ICellStyle style2 = workbook.CreateCellStyle();
            style2.CloneStyleFrom(CellStyles[ExcelCellStyle.Style2]);
            foreach (DataRow dr in dt.Rows)
            {
                IRow row = WriteRow(sheet, dr.ItemArray, rowIndex, columnIndex);

                rowIndex++;
            }
            //sheet.DisplayGridlines = false;
            //for (int i = columnIndex; i < columnIndex+dt.Columns.Count; i++)
            //{
            //    sheet.AutoSizeColumn(i, true);
            //}
        }

        public static void WriteTableStorMatch(HSSFWorkbook workbook, ISheet sheet, DataTable dt, int rowIndex, int columnIndex, bool writeHeader, bool useCaption)
        {
            string[] header = new string[dt.Columns.Count];
            for (int i = 0; i < header.Length; i++)
            {
                if (useCaption)
                {
                    header[i] = dt.Columns[i].Caption;
                }
                else
                {
                    header[i] = dt.Columns[i].ColumnName;
                }
            }
            if (writeHeader)
            {
                ICellStyle style2 = workbook.CreateCellStyle();
                style2.CloneStyleFrom(CellStyles[ExcelCellStyle.Style2]);
                IRow row = WriteRow(sheet, header, rowIndex, columnIndex);
                for (int i = row.FirstCellNum; i < row.LastCellNum; i++)
                {
                    row.GetCell(i).CellStyle = style2;
                }
            }
            rowIndex++;
            ICellStyle style24 = workbook.CreateCellStyle();
            style24.CloneStyleFrom(CellStyles[ExcelCellStyle.Style24]);
            foreach (DataRow dr in dt.Rows)
            {
                IRow row = WriteRow(sheet, dr.ItemArray, rowIndex, columnIndex);
                for (int i = row.FirstCellNum; i < row.LastCellNum; i++)
                {
                    if (i == 0 || i == 1 || i == 5 || i == 6 || i == 11)
                        row.GetCell(i).CellStyle = style24;
                }
                rowIndex++;
            }
        }
        /// <summary>
        /// 写入行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dr"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static IRow WriteRow(ISheet sheet, DataRow dr, int rowIndex, int columnIndex)
        {
            return WriteRow(sheet, dr.ItemArray, rowIndex, columnIndex);
        }

        /// <summary>
        /// 写入行
        /// </summary>
        public static IRow WriteRow(ISheet sheet, object[] dr, int rowIndex, int columnIndex)
        {
            IRow row = sheet.CreateRow(rowIndex);
            int i = 0;
            while (i < dr.Length)
            {
                WriteCell(row, dr[i], i);
                i++;
                columnIndex++;
            }
            return row;
        }

        /// <summary>
        /// 写入单元格
        /// </summary>
        public static ICell WriteCell(IRow row, object obj, int columnIndex)
        {
            ICell cell = row.GetCell(columnIndex);
            if (cell == null)
                cell = row.CreateCell(columnIndex);
            if (obj is string)
            {
                cell.SetCellValue((string)obj);
            }
            else if (obj is decimal)
            {
                cell.SetCellValue(Convert.ToDouble(obj));
            }
            else if (obj is DateTime)
            {
                cell.SetCellValue((DateTime)obj);
                if (CurrentWorkbookCellStyles.ContainsKey("DateTime"))
                {
                    cell.CellStyle = CurrentWorkbookCellStyles["DateTime"];
                }
            }
            else if (obj is bool)
            {
                cell.SetCellValue(obj.ToString());
            }
            else if (obj.GetType().IsValueType)
            {
                cell.SetCellValue(Convert.ToDouble(obj));
            }
            else
            {
                cell.SetCellValue(obj.ToString());
            }

            return cell;
        }

        /// <summary>
        /// 往单元格写数据
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ICell WriteCell(ISheet sheet, int columnIndex, int rowIndex, object obj)
        {
            IRow row = sheet.GetRow(rowIndex);
            if (row == null)
            {
                row = sheet.CreateRow(rowIndex);
            }
            return WriteCell(row, obj, columnIndex);
        }


        /// <summary>
        /// 合并行列写数据
        /// </summary>
        public static void WriteCellCombine(ISheet sheet1, int firstCol, int lastCol, int firstRow, int lastRow, object cellValue, ICellStyle style)
        {
            sheet1.AddMergedRegion(new CellRangeAddress(firstRow, lastRow, firstCol, lastCol));
            for (int j = firstRow; j <= lastRow; j++)
            {
                for (int i = firstCol; i <= lastCol; i++)
                {
                    WriteCell(sheet1, i, j, cellValue, style);
                }
            }
        }

        public static ICell WriteCell(ISheet sheet, int columnIndex, int rowIndex, object obj, ICellStyle style)
        {
            IRow row = sheet.GetRow(rowIndex);
            if (row == null)
            {
                row = sheet.CreateRow(rowIndex);
            }

            ICell cellText = WriteCell(row, obj, columnIndex);
            cellText.CellStyle = style;
            return cellText;
        }

        /// <summary>
        /// 在一个Cell内插入图片
        /// </summary>
        public static void WirtePic(HSSFWorkbook workbook, ISheet sheet, HSSFPatriarch patriarch, int columnIndex, int rowIndex, string picPath)
        {
            WirtePic(workbook, sheet, patriarch, columnIndex, columnIndex, rowIndex, picPath);
        }

        /// <summary>
        /// 在指定位置插入图片，跨Column。ColumnIndex2需大于或等于ColumnIndex1
        /// </summary>
        public static void WirtePic(HSSFWorkbook workbook, ISheet sheet, HSSFPatriarch patriarch, int columnIndex1, int columnIndex2, int rowIndex, string picPath)
        {
            if (!File.Exists(picPath))
            {
                return;
            }
            if (columnIndex2 < columnIndex1)
            {
                throw new Exception("ColumnIndex2需大于或等于ColumnIndex1");
            }
            int dx2 = 1023;
            int dy2 = 255;
            IRow row = sheet.GetRow(rowIndex);
            if (row != null)
            {
                int cWidth = 0;
                for (int i = columnIndex1; i < columnIndex2; i++)
                {
                    cWidth += sheet.GetColumnWidth(columnIndex1);
                }
                using (Image img = Image.FromFile(picPath))
                {
                    double w = cWidth * 7.0 / 256.0;
                    double h = (row.Height * 1.32 / 20.0);
                    if (((double)img.Width / (double)img.Height) > (cWidth * 7.0 / 256 / (row.Height * 1.32 / 20)))
                    {
                        double h1 = w * img.Height / img.Width;
                        dy2 = (int)(h1 * 255 / h);
                        if (dy2 < 0) dy2 = 0;
                        if (dy2 > 255) dy2 = 255;
                    }
                    else
                    {
                        double w1 = h * img.Width / img.Height;
                        dx2 = (int)(w1 * 1023 / w);
                        if (dy2 < 0) dy2 = 0;
                        if (dy2 > 1023) dy2 = 1023;
                    }
                }
            }
            HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, dx2, dy2, columnIndex1, rowIndex, columnIndex2, rowIndex);
            byte[] buff = File.ReadAllBytes(picPath);
            int pic = workbook.AddPicture(buff, PictureType.JPEG);
            anchor.AnchorType = 2;
            patriarch.CreatePicture(anchor, pic);
        }

        public static object GetValue(HSSFCell cell)
        {
            switch (cell.CellType)
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
                case CellType.FORMULA: //FORMULA:
                default:
                    return "=" + cell.CellFormula;
            }
        }
    }

    public class XMLToExcel
    {
        private string xmlfile = @"a.xml";

        /// <summary>
        /// 生成excel文件时的服务器目录
        /// </summary>
        private string path;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">生成excel文件时的服务器目录</param>
        public XMLToExcel(string path)
        {
            this.path = path;
        }

        private XmlDocument xmlDocLoad()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                string tempath = HttpContext.Current.Server.MapPath("~/XMLTemplate/XMLExcelTemplate.xml");
                xmlDoc.Load(tempath);
                return xmlDoc;
            }
            catch
            {
                throw new Exception("加载XML文件：" + xmlfile + "产生错误,请检查文件");
            }


        }

        private XmlDocument BCWDXmlDocLoad()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                string tempath = HttpContext.Current.Server.MapPath("~/XMLTemplate/XMLBCWDExcelTemplate.xml");
                xmlDoc.Load(tempath);
                return xmlDoc;
            }
            catch
            {
                throw new Exception("加载XML文件：" + xmlfile + "产生错误,请检查文件");
            }

        }

        private XmlDocument TableToXMLDoc(DataTable dt, int[] hiddenColumnsIndex)
        {
            try
            {
                XmlDocument xmlDoc = xmlDocLoad();

                XmlNode xmlnode = xmlDoc.GetElementsByTagName("Table").Item(0);

                #region 设置列数和行数
                xmlnode.Attributes["ss:ExpandedColumnCount"].Value = dt.Columns.Count.ToString();
                xmlnode.Attributes["ss:ExpandedRowCount"].Value = ((int)(dt.Rows.Count + 1)).ToString();

                #endregion

                XmlElement xeRow;
                XmlElement xeCell;
                XmlElement xeData;
                XmlElement xeColum;
                XmlAttribute xa;


                #region 将具体的列隐藏
                if (hiddenColumnsIndex != null)
                {
                    for (int i = 0; i < hiddenColumnsIndex.Length; i++)
                    {
                        xeColum = xmlDoc.CreateElement("", "Column", "urn:schemas-microsoft-com:office:spreadsheet");
                        xa = xmlDoc.CreateAttribute("ss", "Index", "urn:schemas-microsoft-com:office:spreadsheet");
                        xa.Value = hiddenColumnsIndex[i].ToString();
                        xeColum.Attributes.Append(xa);

                        xa = xmlDoc.CreateAttribute("ss", "Hidden", "urn:schemas-microsoft-com:office:spreadsheet");
                        xa.Value = "1";
                        xeColum.Attributes.Append(xa);

                        xa = xmlDoc.CreateAttribute("ss", "AutoFitWidth", "urn:schemas-microsoft-com:office:spreadsheet");
                        xa.Value = "0";
                        xeColum.Attributes.Append(xa);
                        xmlnode.AppendChild(xeColum);

                        //  <Column ss:Index="6" ss:Hidden="1" ss:AutoFitWidth="0" />
                        //ss:Width="523.5"
                    }
                }
                #endregion

                #region 添加具体的列数据和行数据,第一行是标题，所以＋1
                xeRow = xmlDoc.CreateElement("", "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    xeCell = xmlDoc.CreateElement("", "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                    xeData = xmlDoc.CreateElement("", "Data", "urn:schemas-microsoft-com:office:spreadsheet");

                    xa = xmlDoc.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");


                    xa.Value = "String";

                    xeData.Attributes.Append(xa);
                    xeData.InnerText = dt.Columns[j].Caption;
                    xeCell.AppendChild(xeData);
                    xeRow.AppendChild(xeCell);
                    //xeCell.InnerXml = "<Data ss:Type='String'>" + dt.Columns[j].Caption + "</Data>";
                }
                xmlnode.AppendChild(xeRow);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    xeRow = xmlDoc.CreateElement("", "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        xeCell = xmlDoc.CreateElement("", "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                        xeData = xmlDoc.CreateElement("", "Data", "urn:schemas-microsoft-com:office:spreadsheet");
                        xa = xmlDoc.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
                        long a = 0;
                        if (!dt.Rows[i][j].ToString().StartsWith("0"))
                        {
                            if (Int64.TryParse(dt.Rows[i][j].ToString(), out a))
                            {
                                xa.Value = "Number";
                            }
                            else
                            {
                                xa.Value = "String";
                            }
                        }
                        else
                        {
                            if (dt.Rows[i][j].ToString().Length == 1)
                            {
                                xa.Value = "Number";
                            }
                            else
                            {
                                xa.Value = "String";
                            }

                        }
                        xeData.Attributes.Append(xa);
                        xeData.InnerText = dt.Rows[i][j].ToString();
                        xeCell.AppendChild(xeData);

                        // xeCell.InnerText = HttpContext.Current.Server.HtmlEncode("<Data ss:Type='String'>" + dt.Rows[i][j].ToString() + "</Data>").ToString(); 

                        xeRow.AppendChild(xeCell);
                    }
                    xmlnode.AppendChild(xeRow);
                }
                #endregion

                return xmlDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }



        private XmlDocument TableToBCWDXMLDoc(DataTable dt, string year)
        {
            try
            {
                XmlDocument xmlDoc = BCWDXmlDocLoad();
                XmlNode xmlnode = xmlDoc.GetElementsByTagName("Table").Item(0);
                xmlnode.ChildNodes[18].ChildNodes[0].ChildNodes[0].InnerText = year;
                XmlElement xeRow;
                XmlElement xeCell;
                XmlElement xeData;
                XmlElement xeColum;
                XmlAttribute xa;
                xmlnode.Attributes["ss:ExpandedRowCount"].Value = ((int)(dt.Rows.Count + 2)).ToString();



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    xeRow = xmlDoc.CreateElement("", "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        xeCell = xmlDoc.CreateElement("", "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                        xeData = xmlDoc.CreateElement("", "Data", "urn:schemas-microsoft-com:office:spreadsheet");
                        xa = xmlDoc.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");

                        xa.Value = "String";

                        xeData.Attributes.Append(xa);
                        xeData.InnerText = dt.Rows[i][j].ToString();
                        xeCell.AppendChild(xeData);

                        // xeCell.InnerText = HttpContext.Current.Server.HtmlEncode("<Data ss:Type='String'>" + dt.Rows[i][j].ToString() + "</Data>").ToString(); 

                        xeRow.AppendChild(xeCell);
                    }
                    xmlnode.AppendChild(xeRow);
                }
                return xmlDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// datatable to excel
        /// </summary>
        /// <param name="dt">需要生产excel 的datatable</param>
        /// <param name="hiddenColumnsIndex">隐藏列的数组,从1开始</param>
        /// <param name="filename">excel的文件名，不含后缀</param>
        /// <returns>excel文件的服务器地址</returns>
        public string TableToBCWDExcel(DataTable dt, string fileName, string year)
        {
            XmlDocument xmlDoc = TableToBCWDXMLDoc(dt, year);
            string fullPath = path + "/" + fileName + ".xls";

            //Frank, I changed here to check if the directory exist and create it. Peter
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            xmlDoc.Save(fullPath);
            return fullPath;
        }


        /// <summary>
        /// datatable to BCWDexcel
        /// </summary>
        /// <param name="dt">需要生产BCWDexcel 的datatable</param>

        /// <param name="filename">excel的文件名，不含后缀</param>
        /// <returns>excel文件的服务器地址</returns>
        public string TableToExcel(DataTable dt, int[] hiddenColumnsIndex, string fileName)
        {
            XmlDocument xmlDoc = TableToXMLDoc(dt, hiddenColumnsIndex);
            string fullPath = path + "/" + fileName + ".xls";

            //Frank, I changed here to check if the directory exist and create it. Peter
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            xmlDoc.Save(fullPath);
            return fullPath;
        }


        /// <summary>
        /// ExcelTo DateTable
        /// </summary>
        /// <param name="excelFile">excel文件的服务器地址</param>
        /// <param name="needColumnsIndex">需要获取的列，为null表示全部获取</param>
        /// <returns></returns>
        public DataTable ExcelToTable(string excelFile, int[] needColumnsIndex)
        {
            return ExcelToTable(excelFile, needColumnsIndex, false);
        }

        /// <summary>
        /// ExcelTo DateTable
        /// </summary>
        /// <param name="excelFile">excel文件的服务器地址</param>
        /// <returns></returns>
        public DataTable ExcelToTable(string excelFile)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(excelFile);
            }
            catch
            {
                throw new Exception("Excel 文件格式不对,可能存在一些特殊字符!");

            }
            XmlNodeList xnlist = xmlDoc.GetElementsByTagName("Row");

            for (int i = 0; i < xnlist.Count; i++)
            {

                if (i != 0)
                {
                    dr = dt.NewRow();

                }
                for (int j = 0; j < xnlist[i].ChildNodes.Count; j++)
                {
                    if (i == 0)
                    {
                        string columnName = xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value;
                        if (!dt.Columns.Contains(columnName))
                        {
                            dt.Columns.Add(columnName);
                        }
                        else
                        {
                            dt.Columns.Add(columnName + j.ToString());
                        }
                    }
                    else
                    {
                        if (xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild != null)
                        {
                            dr[j] = xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value;
                        }
                        else
                        {
                            dr[j] = "";
                        }
                    }
                }
                if (i != 0)
                {
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }


        /// <summary>
        /// BCWD Excel To DateTable
        /// </summary>
        /// <param name="excelFile">BCWD excel文件的服务器地址</param>
        /// <returns></returns>
        public DataTable BCWDExcelToTable(string excelFile, out string year)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(excelFile);
            }
            catch
            {
                throw new Exception("Excel 文件格式不对,可能存在一些特殊字符!");

            }
            XmlNode xmlnode = xmlDoc.GetElementsByTagName("Table").Item(0);
            year = xmlnode.ChildNodes[18].ChildNodes[0].ChildNodes[0].InnerText;
            XmlNodeList xnlist = xmlDoc.GetElementsByTagName("Row");

            for (int i = 0; i < xnlist.Count; i++)
            {

                if (i != 0)
                {
                    if (i != 1)
                    {
                        dr = dt.NewRow();

                    }
                    for (int j = 0; j < xnlist[i].ChildNodes.Count; j++)
                    {
                        if (i == 1)
                        {
                            dt.Columns.Add(xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value);
                        }
                        else
                        {
                            if (xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild != null)
                            {
                                dr[j] = xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value;
                            }
                            else
                            {
                                dr[j] = "0";
                            }
                        }
                    }
                    if (i != 1)
                    {
                        dt.Rows.Add(dr);
                    }

                }


            }
            return dt;

        }


        /// <summary>
        /// ExcelTo DateTable
        /// </summary>
        /// <param name="excelFile">excel文件的服务器地址</param>
        /// <param name="needColumnsIndex">需要获取的列，为null表示全部获取</param>
        /// <param name="reversalColumnsIndex">true则从列的最后开始获取对应needColumnsIndex</param>
        /// <returns></returns>
        public DataTable ExcelToTable(string excelFile, int[] needColumnsIndex, bool? reversalColumnsIndex)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(excelFile);
            }
            catch
            {
                throw new Exception("Excel 文件格式不对,可能存在一些特殊字符!");

            }
            try
            {
                XmlNodeList xnlist = xmlDoc.GetElementsByTagName("Row");
                //颠倒列索引,因为从0开始，所以要减去1
                if (reversalColumnsIndex.HasValue && reversalColumnsIndex.Value)
                {
                    for (int i = 0; i < needColumnsIndex.Length; i++)
                    {
                        needColumnsIndex[i] = xnlist[0].ChildNodes.Count - needColumnsIndex[i] - 1;
                    }
                }

                for (int i = 0; i < xnlist.Count; i++)
                {

                    if (i != 0)
                    {
                        dr = dt.NewRow();

                    }
                    for (int j = 0; j < xnlist[i].ChildNodes.Count; j++)
                    {
                        if (i == 0)
                        {
                            if (needColumnsIndex != null)
                            {
                                for (int k = 0; k < needColumnsIndex.Length; k++)
                                {
                                    if (j == needColumnsIndex[k])
                                    {
                                        dt.Columns.Add(xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value);
                                    }
                                }
                            }
                            else
                            {
                                dt.Columns.Add(xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value);
                            }
                        }
                        else
                        {
                            if (needColumnsIndex != null)
                            {
                                for (int k = 0; k < needColumnsIndex.Length; k++)
                                {
                                    if (j == needColumnsIndex[k])
                                    {
                                        if (!xnlist[i].ChildNodes.Item(j).FirstChild.HasChildNodes)
                                        {
                                            dr[k] = "";
                                        }
                                        else
                                        {
                                            dr[k] = xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (xnlist[i].ChildNodes.Item(j).FirstChild.HasChildNodes)
                                {
                                    dr[j] = "";
                                }
                                else
                                {
                                    dr[j] = xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value;
                                }
                            }
                        }
                    }
                    if (i != 0)
                    {
                        dt.Rows.Add(dr);
                    }

                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Excel Format Error");
            }
        }

        /// <summary>
        /// 根据Dictionary来生成对应的Table结构
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static DataTable CreateDateTableScheme(IDictionary<string, string> dic)
        {
            DataTable dt = new DataTable();
            foreach (KeyValuePair<string, string> de in dic)
            {
                DataColumn dColumn = new DataColumn(de.Key);
                dColumn.Caption = de.Value;
                dt.Columns.Add(dColumn);
            }
            return dt;
        }
    }
}
