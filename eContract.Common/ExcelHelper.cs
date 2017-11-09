using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Xml;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System.Drawing;
using eContract.Common;
using System.Reflection;
using System.ComponentModel;
using eContract.Common.Const;
using System.Data.OleDb;
using ICSharpCode.SharpZipLib.Zip;
using NPOI.SS.Util;
using eContract.Common.WebUtils;
using System.Text;

namespace eContract.Common {
    public class ExcelHelper
    {
        public static string EXPORT_EXCEL_FILENAME = "EXPORT_EXCEL_FILENAME";
        public static string EXPORT_EXCEL_TITLE = "EXPORT_EXCEL_TITLE";
        public static string EXPORT_EXCEL_FILE = "EXPORT_EXCEL_FILE";
        public static string EXPORT_EXCEL_CONTEXT = "EXPORT_EXCEL_CONTEXT";
        private HSSFWorkbook Hssfworkbook { get; set; }
        public ExcelHelper()
        {
            this.Hssfworkbook = new HSSFWorkbook();
        }

        public static void Export(ExcelConvertHelper.ExcelContext excelContext, ref string strHtml)
        {
            if (excelContext == null) return;
            if (string.IsNullOrEmpty(excelContext.FileName)) { excelContext.FileName = excelContext.Title + ".xls"; }
            strHtml = ExcelConvertHelper.ToHtml(excelContext);
        }

        public static void Export(List<ExcelConvertHelper.ExcelContext> excelContexts, ref string strHtml)
        {
            if (excelContexts == null || excelContexts.Count == 0) return;
            if (string.IsNullOrEmpty(excelContexts[0].FileName)) { excelContexts[0].FileName = excelContexts[0].Title + ".xls"; }

            foreach (ExcelConvertHelper.ExcelContext item in excelContexts)
            {
                strHtml += ExcelConvertHelper.ToHtml(item);
            }
        }

        public void ExportExcel(ExcelConvertHelper.ExcelContext excelContext, string file)
        {
            if (excelContext == null) return;
            if (string.IsNullOrEmpty(excelContext.FileName)) { excelContext.FileName = excelContext.Title + ".xls"; }
            AddSheet(excelContext.Data.TableName);
            BindSheetData(excelContext);

            CreateFile(file);
        }

        public void ExportExcel(List<ExcelConvertHelper.ExcelContext> excelContexts, string file)
        {
            if (excelContexts == null || excelContexts.Count == 0) return;
            if (string.IsNullOrEmpty(excelContexts[0].FileName)) { excelContexts[0].FileName = excelContexts[0].Title + ".xls"; }

            foreach (ExcelConvertHelper.ExcelContext item in excelContexts)
            {
                AddSheet(item.Data.TableName);
                BindSheetData(item);
            }

            CreateFile(file);
        }

        public HSSFSheet AddSheet(string sheetName)
        {
            if (string.IsNullOrEmpty(sheetName))
            {
                sheetName = "sheet" + Hssfworkbook.Workbook.NumSheets;
            }
            return (HSSFSheet)Hssfworkbook.CreateSheet(sheetName);
        }

        public HSSFSheet GetSheet(int index)
        {
            return (HSSFSheet)Hssfworkbook.GetSheetAt(index);
        }

        public void BindSheetData(ExcelConvertHelper.ExcelContext excelContext)
        {
            DataTable table = excelContext.Data;
            if (table == null)
            {
                return;
            }
            int dataRows = table.Rows.Count;
            if (dataRows == 0)
            {
                dataRows = 1;
            }
            int sheetCount = dataRows % 65000 == 0 ? dataRows / 65000 : ((dataRows / 65000) + 1);
            int dataIndex = 0;
            for (int k = 1; k <= sheetCount; k++)
            {
                if (Hssfworkbook.Workbook.NumSheets < k)
                {
                    AddSheet(table.TableName + k);
                }
                HSSFSheet sheet = GetSheet(k - 1);
                if (sheet == null)
                {
                    sheet = AddSheet(table.TableName);
                }
                sheet.DisplayGridlines = false;
                HSSFRow _row = null;
                ICell _cell = null;

                IFont titleFont = Hssfworkbook.CreateFont();
                titleFont.FontName = "微软雅黑";
                titleFont.FontHeightInPoints = 12;
                titleFont.Boldweight = (short)FontBoldWeight.BOLD;

                IFont headFont = Hssfworkbook.CreateFont();
                headFont.FontName = "微软雅黑";
                headFont.FontHeightInPoints = 10;
                headFont.Boldweight = (short)FontBoldWeight.BOLD;

                IFont bodyfont = Hssfworkbook.CreateFont();
                bodyfont.FontName = "微软雅黑";
                bodyfont.FontHeightInPoints = 10;

                ICellStyle titleAlignCenterStyle = Hssfworkbook.CreateCellStyle();
                titleAlignCenterStyle.Alignment = HorizontalAlignment.CENTER;
                titleAlignCenterStyle.SetFont(titleFont);
                titleAlignCenterStyle.BorderTop = BorderStyle.THIN;
                titleAlignCenterStyle.BorderRight = BorderStyle.THIN;
                titleAlignCenterStyle.BorderLeft = BorderStyle.THIN;
                titleAlignCenterStyle.BorderBottom = BorderStyle.THIN;

                ICellStyle headAlignCenterStyle = Hssfworkbook.CreateCellStyle();
                headAlignCenterStyle.Alignment = HorizontalAlignment.CENTER;
                headAlignCenterStyle.SetFont(headFont);
                headAlignCenterStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
                headAlignCenterStyle.FillForegroundColor = HSSFColor.ORANGE.index;
                headAlignCenterStyle.BorderTop = BorderStyle.THIN;
                headAlignCenterStyle.BorderRight = BorderStyle.THIN;
                headAlignCenterStyle.BorderLeft = BorderStyle.THIN;
                headAlignCenterStyle.BorderBottom = BorderStyle.THIN;

                ICellStyle alignLeftStyle = Hssfworkbook.CreateCellStyle();
                alignLeftStyle.Alignment = HorizontalAlignment.LEFT;
                alignLeftStyle.SetFont(bodyfont);
                alignLeftStyle.BorderTop = BorderStyle.THIN;
                alignLeftStyle.BorderRight = BorderStyle.THIN;
                alignLeftStyle.BorderLeft = BorderStyle.THIN;
                alignLeftStyle.BorderBottom = BorderStyle.THIN;

                ICellStyle alignCenterStyle = Hssfworkbook.CreateCellStyle();
                alignCenterStyle.Alignment = HorizontalAlignment.CENTER;
                alignCenterStyle.SetFont(bodyfont);
                alignCenterStyle.BorderTop = BorderStyle.THIN;
                alignCenterStyle.BorderRight = BorderStyle.THIN;
                alignCenterStyle.BorderLeft = BorderStyle.THIN;
                alignCenterStyle.BorderBottom = BorderStyle.THIN;

                int index = 0;
                #region 添加Title
                if (!string.IsNullOrEmpty(excelContext.Title))
                {
                    _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum);
                    _cell = _row.CreateCell(index, CellType.STRING);
                    _cell.SetCellValue(excelContext.Title);
                    _cell.CellStyle = titleAlignCenterStyle;
                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, excelContext.Columns.Count - 1));
                    index++;
                }
                #endregion
                #region 添加列头
                if (index >= 1)
                {
                    _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum + 1);
                }
                else
                {
                    _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum);
                }
                index = 0;
                int length = 0;
                foreach (var column in excelContext.Columns)
                {
                    _cell = _row.CreateCell(index, CellType.STRING);
                    _cell.SetCellValue(column.Name);
                    _cell.CellStyle = headAlignCenterStyle;
                    length = Encoding.UTF8.GetBytes(_cell.ToString()).Length;
                    sheet.SetColumnWidth(index, (length + 5) * 256);  
                    index++;
                }
                #endregion
                #region 直接绑定所有数据
                for (int j = (k - 1) * 65000; j < dataRows; j++)
                {
                    DataRow row = table.Rows[j];
                    _row = (HSSFRow)sheet.CreateRow(sheet.LastRowNum + 1);
                    for (int i = 0; i < excelContext.Columns.Count; i++)
                    {
                        _cell = _row.CreateCell(i, CellType.STRING);
                        if (table.Columns.Contains(excelContext.Columns[i].Key))
                        {
                            _cell.SetCellValue(row[excelContext.Columns[i].Key].ToString());
                            if (GetAlignStyle(table.Columns[excelContext.Columns[i].Key]) == "center")
                            {
                                _cell.CellStyle = alignCenterStyle;
                            }
                            else
                            {
                                _cell.CellStyle = alignLeftStyle;
                            }
                        }
                        else
                        {
                            _cell.SetCellValue("");
                            _cell.CellStyle = alignLeftStyle;
                        }
                    }
                    dataIndex++;
                    if (dataIndex >= 65000)
                    {
                        dataIndex = 0;
                        break;
                    }
                }
                #endregion
            }
        }

        private string GetAlignStyle(DataColumn column)
        {
            //column.DataType.Equals(System.Type.GetType("System.String"))
            if (column.DataType.FullName == "System.String")
            {
                return "left";
            }
            else
            {
                return "center";
            }
        }

        private void CreateFile(string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                Hssfworkbook.Write(file);
            }
        }
    }

    public class ExcelConvertHelper {
        public static class ExcelCellStyle {
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
        }

        /// <summary>
        /// 样式模板
        /// </summary>
        public static Dictionary<string, ICellStyle> CellStyles {
            get;
            private set;
        }

        public static Dictionary<string, ICellStyle> CurrentWorkbookCellStyles {
            get;
            private set;
        }

        public ExcelConvertHelper() {
            CellStyles = new Dictionary<string, ICellStyle>();
            string path = string.Empty;
            if (HttpContext.Current != null) {
                path = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);
            } else {
                path = AppDomain.CurrentDomain.BaseDirectory;
            }
            using (FileStream file = new FileStream(Path.Combine(path, "ExcelStyle.xls"), FileMode.Open, FileAccess.Read)) {
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                ISheet s = hssfworkbook.GetSheetAt(0);
                for (int i = s.FirstRowNum; i <= s.LastRowNum; i++) {
                    ICell c0 = s.GetRow(i).GetCell(0);
                    ICell c1 = s.GetRow(i).GetCell(1);
                    CellStyles.Add(c0.StringCellValue, c1.CellStyle);
                    if (c0.StringCellValue == "DateTime") {
                        IDataFormat format = hssfworkbook.CreateDataFormat();
                        CellStyles["DateTime"].DataFormat = format.GetFormat(c1.StringCellValue);
                    }
                }
            }
        }

        public static DataTable GetDataFromZipFile(string strFilePath, string strSheetName, ref string strError, int iPhotoIndex) {

            // 解压到临时目录
            string distDir = Path.GetDirectoryName(strFilePath);

            string strDirFile = distDir + "\\" + Path.GetFileNameWithoutExtension(strFilePath);
            if (!Directory.Exists(strDirFile))
                Directory.CreateDirectory(strDirFile);
            try {
                FastZip fastZip = new FastZip();
                fastZip.ExtractZip(strFilePath, strDirFile, "");
            } catch {
                strError = "解压失败";
                return null;
            }

            string[] strFiles = Directory.GetFiles(strDirFile, "*.xls");

            if (strFiles.Length == 0) {
                strError = "压缩包中没有excel文件";
                return null;
            }

            string[] strImageFiles = Directory.GetFiles(strDirFile, "*.jpg");
            DataTable dtPhoto = new DataTable();
            dtPhoto.Columns.Add("OriPhoto");
            dtPhoto.Columns.Add("NewPhoto");
            if (strImageFiles.Length > 0) {
                int i = 0;
                foreach (string strImageFile in strImageFiles) {
                    string strFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + i.ToString() + ".jpg";
                    // ImageHelper.MakeThumbnail(strImageFile, strDirFile + "\\Thum" + Path.GetFileName(strImageFile), 150, 150, "H");
                    ImageHelper.MakeThumbnail(strImageFile, strDirFile + "\\" + strFileName, 150, 150, "H");
                    DataRow dr = dtPhoto.NewRow();
                    dr["OriPhoto"] = Path.GetFileName(strImageFile);
                    dr["NewPhoto"] = strFileName;
                    dtPhoto.Rows.Add(dr);
                    i++;
                }
            }

            DataTable dt = new DataTable();
            // 解析服务器excel
            string strPartConnection = "Provider=Microsoft.Jet.OLEDB.4.0;"
          + " Extended Properties='Excel 8.0; HDR=YES;IMEX=1;';";

            string strConnection = strPartConnection
               + "Data Source=" + strFiles[0].ToString();
            OleDbConnection connection = new OleDbConnection(strConnection);
            connection.Open();
            try {
                string strCommand = "select * From " + " [" + strSheetName + "$]";
                OleDbDataAdapter ds = new OleDbDataAdapter(strCommand, connection);
                ds.Fill(dt);
            } catch {
                strError = "读取Excel失败";
                dt = null;
            } finally {
                connection.Close();
            }
            if (dt != null) {
                dt.Columns.Add("PhotoPath");

                foreach (DataRow dr in dt.Rows) {
                    foreach (DataRow drPhoto in dtPhoto.Rows) {
                        if (dr[iPhotoIndex].ToString().Trim().ToLower().Equals(drPhoto["OriPhoto"].ToString().Trim().ToLower())) {
                            dr["PhotoPath"] = drPhoto["NewPhoto"].ToString();
                            break;
                        }
                    }
                }
            }
            return dt;
        }

        public static DataTable GetDataFromZipFile(string strFilePath, ref string strError, int iPhotoIndex) {

            // 解压到临时目录
            string distDir = Path.GetDirectoryName(strFilePath);

            string strDirFile = distDir + "\\" + Path.GetFileNameWithoutExtension(strFilePath);
            if (!Directory.Exists(strDirFile))
                Directory.CreateDirectory(strDirFile);
            try {
                FastZip fastZip = new FastZip();
                fastZip.ExtractZip(strFilePath, strDirFile, "");
            } catch {
                strError = "解压失败";
                return null;
            }

            string[] strFiles = Directory.GetFiles(strDirFile, "*.xls");

            if (strFiles.Length == 0) {
                strError = "压缩包中没有excel文件";
                return null;
            }

            //string[] strImageFiles = Directory.GetFiles(strDirFile, "*.jpg|*.jpeg|*.png");
            List<string> strImageFiles = new List<string>();

            string imgtype = "*.jpeg|*.jpg|*.png";
            string[] ImageType = imgtype.Split('|');
            for (int i = 0; i < ImageType.Length; i++) {
                string[] dirs = Directory.GetFiles(strDirFile, ImageType[i]); // string[] dirs = Directory.GetFiles(@"d:\My Documents\My Pictures", "*.jpg"); int j = 0;
                foreach (string dir in dirs)
                    strImageFiles.Add(dir);
            }


            DataTable dtPhoto = new DataTable();
            dtPhoto.Columns.Add("OriPhoto");
            dtPhoto.Columns.Add("NewPhoto");
            if (strImageFiles.Count > 0) {
                int i = 0;
                foreach (string strImageFile in strImageFiles) {
                    string strFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + i.ToString() + Path.GetExtension(strImageFile);
                    // ImageHelper.MakeThumbnail(strImageFile, strDirFile + "\\Thum" + Path.GetFileName(strImageFile), 150, 150, "H");
                    ImageHelper.MakeThumbnail(strImageFile, strDirFile + "\\" + strFileName, 150, 150, "H");
                    DataRow dr = dtPhoto.NewRow();
                    dr["OriPhoto"] = Path.GetFileName(strImageFile);
                    dr["NewPhoto"] = strFileName;
                    dtPhoto.Rows.Add(dr);
                    i++;
                }
            }

            DataTable dt = new DataTable();

            try {
                dt = ReadExcel(strFiles[0].ToString());
            } catch {
                strError = "读取Excel失败";
                dt = null;
            }

            if (dt != null) {
                dt.Columns.Add("PhotoPath");

                foreach (DataRow dr in dt.Rows) {
                    foreach (DataRow drPhoto in dtPhoto.Rows) {
                        if (dr[iPhotoIndex].ToString().Trim().ToLower().Equals(drPhoto["OriPhoto"].ToString().Trim().ToLower()) || dr[iPhotoIndex].ToString().Trim().ToLower().Equals(drPhoto["OriPhoto"].ToString().Trim().ToLower().Replace(".jpg", ""))) {
                            dr["PhotoPath"] = drPhoto["NewPhoto"].ToString();
                            break;
                        }
                    }
                }
            }
            return dt;
        }

        public static DataTable GetDataFromExcelFile(string strFilePath, string strSheetName, ref string strError) {


            DataTable dt = new DataTable();
            // 解析服务器excel
            string strPartConnection = "Provider=Microsoft.Jet.OLEDB.4.0;"
          + " Extended Properties='Excel 8.0; HDR=YES;IMEX=1;';";

            string strConnection = strPartConnection
               + "Data Source=" + strFilePath;
            OleDbConnection connection = new OleDbConnection(strConnection);
            connection.Open();
            try {
                string strCommand = "select * From " + " [" + strSheetName + "$]";
                OleDbDataAdapter ds = new OleDbDataAdapter(strCommand, connection);
                ds.Fill(dt);
            } catch {
                strError = "读取Excel失败";
                dt = null;
            } finally {
                connection.Close();
            }
            return dt;
        }

        public static DataTable GetDataFromExcelFile(string strFilePath, ref string strError)
        {
            try
            {
                return ReadExcel(strFilePath);
            }
            catch (Exception ex)
            {
                strError = "读取Excel失败" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// sheetCount表示Excel中包含所需读取的Sheet个数，存数据的Sheet不可跳跃，即不可在Sheet1和Sheet3，需连续；
        /// sheetFirstDataRow表示每个Sheet中（从第1个Sheet开始定义）第一行数据（非表头）的开始行号，为空或未定义时默认第一行为表头，第二行为数据行，可以自行定义；
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="sheetCount"></param>
        /// <param name="sheetFirstDataRow"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public static DataSet GetDataFromExcelFile(string strFilePath, int sheetCount, Dictionary<int,int> sheetFirstDataRow, ref string strError)
        {
            try
            {
                return ReadExcel(strFilePath, sheetCount, sheetFirstDataRow);
            }
            catch (Exception ex)
            {
                strError = "读取Excel失败" + ex.Message;
                return null;
            }
        }

        public static byte[] Export(DataTable dt, string _exportfilePath, string strCycleName, string strTotal, string Upload, string Temp,
            string LocationReject, string PhotoReject, string PassQty, string WaitQty) {
            FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read);

            HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);

            ISheet sheet1 = workbook.GetSheetAt(0);
            ICellStyle styleCenter = workbook.CreateCellStyle();
            styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
            ExcelConvertHelper.WriteCell(sheet1, 3, 1, strCycleName, styleCenter);
            ExcelConvertHelper.WriteCell(sheet1, 3, 2, strTotal, styleCenter);
            ExcelConvertHelper.WriteCell(sheet1, 3, 3, Upload, styleCenter);
            ExcelConvertHelper.WriteCell(sheet1, 3, 4, Temp, styleCenter);
            ExcelConvertHelper.WriteCell(sheet1, 3, 5, LocationReject, styleCenter);
            ExcelConvertHelper.WriteCell(sheet1, 3, 6, PhotoReject, styleCenter);
            ExcelConvertHelper.WriteCell(sheet1, 3, 7, PassQty, styleCenter);
            ExcelConvertHelper.WriteCell(sheet1, 3, 8, WaitQty, styleCenter);
            int iStartRow = 10;
            for (int i = 0; i < dt.Rows.Count; i++) {
                ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["STORE_CODE"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["STORE_NAME"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["STORE_ADDRESS"].ToString(), styleCenter);

                ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["region_name"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, dt.Rows[i]["customer_code"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 5, iStartRow + i, dt.Rows[i]["prefixCN"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 6, iStartRow + i, dt.Rows[i]["channel_name"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 7, iStartRow + i, dt.Rows[i]["businessModel"].ToString(), styleCenter);

                ExcelConvertHelper.WriteCell(sheet1, 8, iStartRow + i, dt.Rows[i]["CUSTOMER_NAME"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 9, iStartRow + i, dt.Rows[i]["UPLOAD_USER"].ToString(), styleCenter);

                ExcelConvertHelper.WriteCell(sheet1, 10, iStartRow + i, dt.Rows[i]["LBSLOCATION"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 11, iStartRow + i, dt.Rows[i]["STANDLOCATION"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 12, iStartRow + i, dt.Rows[i]["DIFF_RANGE"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 13, iStartRow + i, dt.Rows[i]["LOCATION_DIFF"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 14, iStartRow + i, dt.Rows[i]["STATUS"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 15, iStartRow + i, dt.Rows[i]["RESOLVE_SOLUTION"].ToString(), styleCenter);
                ExcelConvertHelper.WriteCell(sheet1, 16, iStartRow + i, dt.Rows[i]["AM"].ToString(), styleCenter);
            }


            return Render(workbook);

        }

        public static byte[] ExportTempStore(DataTable dt, string _exportfilePath) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);
                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
                    int iStartRow = 1;
                    for (int i = 0; i < dt.Rows.Count; i++) {
                        ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["TEMP_STORE_CODE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["STORE_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["CONTACT_PHONE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["STORE_ADDRESS"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, dt.Rows[i]["USER_NAME_CN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 5, iStartRow + i, SysConst.FormatControlTime(DateTime.Parse(dt.Rows[i]["SER_INSERT_TIME"].ToString())), styleCenter);
                    }
                    return Render(workbook);
                }
            }
        }

        /// <summary>
        /// 导出陈列报告
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="_exportfilePath"></param>
        /// <returns></returns>
        public static byte[] ExportOrVmReport(DataTable dt, string _exportfilePath) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using ()
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);
                    //ExcelHelper.WriteCell(sheet1, 0, 0, "店铺编号");
                    //ExcelHelper.WriteCell(sheet1, 1, 0, "店铺名称");
                    //ExcelHelper.WriteCell(sheet1, 2, 0, "店铺类型");
                    //ExcelHelper.WriteCell(sheet1, 3, 0, "检查人员");
                    //ExcelHelper.WriteCell(sheet1, 4, 0, "检查时间");
                    //ExcelHelper.WriteCell(sheet1, 5, 0, "得分");

                    int iStartRow = 1;
                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
                    for (int i = 0; i < dt.Rows.Count; i++) {

                        ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["REGIONNAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["PROVINCENAMECN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["CITYNAMECN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["TIERNAMECN"].ToString(), styleCenter);

                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, dt.Rows[i]["STORE_CODE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 5, iStartRow + i, dt.Rows[i]["STORE_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 6, iStartRow + i, dt.Rows[i]["format"].ToString(), styleCenter);
                        if (WebCaching.CurrentLanguage.ToString().Equals("zh-CN"))
                            ExcelConvertHelper.WriteCell(sheet1, 7, iStartRow + i, dt.Rows[i]["USER_NAME_CN"].ToString(), styleCenter);
                        else
                            ExcelConvertHelper.WriteCell(sheet1, 7, iStartRow + i, dt.Rows[i]["USER_NAME_EN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 8, iStartRow + i, SysConst.FormatControlTime(DateTime.Parse(dt.Rows[i]["SERVER_INSERT_TIME"].ToString())), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 9, iStartRow + i, dt.Rows[i]["TOTAL_SCORE"].ToString(), styleCenter);
                    }
                    return Render(workbook);
                }
            }
        }

        public static byte[] InfoDesriptionReport(DataTable dt, string _exportfilePath) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);
                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
                    int iStartRow = 1;

                    for (int i = 0; i < dt.Rows.Count; i++) {
                        ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["STORE_CODE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["STORE_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["FORMAT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["CUSETOMER_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, dt.Rows[i]["DESCRIPTION"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 5, iStartRow + i, SysConst.FormatControlTime(DateTime.Parse(dt.Rows[i]["SERVER_INSERT_TIME"].ToString())), styleCenter);
                    }
                    return Render(workbook);
                }
            }
        }

        /// <summary>
        /// 导出陈列报告
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="_exportfilePath"></param>
        /// <returns></returns>
        public static byte[] ExportFrVmReport(DataTable dt, string _exportfilePath) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using ()
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);
                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
                    int iStartRow = 1;

                    for (int i = 0; i < dt.Rows.Count; i++) {
                        ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["CUSTOMERNAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["REGIONNAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["PROVINCENAMECN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["CITYNAMECN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, dt.Rows[i]["TIERNAMECN"].ToString(), styleCenter);

                        ExcelConvertHelper.WriteCell(sheet1, 5, iStartRow + i, dt.Rows[i]["STORE_CODE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 6, iStartRow + i, dt.Rows[i]["nameCN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 7, iStartRow + i, dt.Rows[i]["format"].ToString(), styleCenter);
                        if (WebCaching.CurrentLanguage.ToString().Equals("zh-CN")) {
                            ExcelConvertHelper.WriteCell(sheet1, 8, iStartRow + i, dt.Rows[i]["USER_NAME_CN"].ToString(), styleCenter);
                        } else {
                            ExcelConvertHelper.WriteCell(sheet1, 8, iStartRow + i, dt.Rows[i]["USER_NAME_EN"].ToString(), styleCenter);
                        }
                        ExcelConvertHelper.WriteCell(sheet1, 9, iStartRow + i, SysConst.FormatControlTime(DateTime.Parse(dt.Rows[i]["SERVER_INSERT_TIME"].ToString())), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 10, iStartRow + i, dt.Rows[i]["TOTAL_SCORE"].ToString(), styleCenter);
                    }
                    return Render(workbook);
                }
            }
        }
        public static byte[] ExportFrRmsReport(DataTable dt, string _exportfilePath) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);
                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
                    int iStartRow = 1;

                    for (int i = 0; i < dt.Rows.Count; i++) {


                        ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["STORE_CODE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["nameCN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["format"].ToString(), styleCenter);
                        if (WebCaching.CurrentLanguage.ToString().Equals("zh-CN"))
                            ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["USER_NAME_CN"].ToString(), styleCenter);
                        else
                            ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["USER_NAME_EN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, SysConst.FormatControlTime(DateTime.Parse(dt.Rows[i]["SERVER_INSERT_TIME"].ToString())), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 5, iStartRow + i, dt.Rows[i]["TOTAL_SCORE"].ToString(), styleCenter);
                    }
                    return Render(workbook);
                }
            }
        }
        public static byte[] ExportReport(DataTable dt, string _exportfilePath, Dictionary<string, string> list) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);
                    ICellStyle styleBolder = workbook.CreateCellStyle();
                    styleBolder.CloneStyleFrom(CellStyles[ExcelCellStyle.Style3]);

                    int cols = 0;
                    foreach (KeyValuePair<string, string> item in list) {
                        ExcelConvertHelper.WriteCell(sheet1, cols, 0, item.Value, styleBolder);
                        cols++;
                    }

                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
                    int iStartRow = 1;

                    for (int i = 0; i < dt.Rows.Count; i++) {
                        int col = 0;
                        foreach (KeyValuePair<string, string> item in list) {
                            ExcelConvertHelper.WriteCell(sheet1, col, iStartRow + i, dt.Rows[i][item.Key].ToString(), styleCenter);
                            col++;
                        }

                    }
                    return Render(workbook);
                }
            }
        }

        /// <summary>
        /// 导出店铺最新形象报告
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="_exportfilePath"></param>
        /// <returns></returns>
        public static byte[] ExportFrVisitPhotoReport(DataTable dt, string _exportfilePath) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);
                    //ExcelHelper.WriteCell(sheet1, 0, 0, "店铺编号");
                    //ExcelHelper.WriteCell(sheet1, 1, 0, "店铺名称");
                    //ExcelHelper.WriteCell(sheet1, 2, 0, "店铺类型");
                    //ExcelHelper.WriteCell(sheet1, 3, 0, "检查人员");
                    //ExcelHelper.WriteCell(sheet1, 4, 0, "检查时间");

                    int iStartRow = 1;
                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
                    for (int i = 0; i < dt.Rows.Count; i++) {
                        ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["STORE_CODE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["nameCN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["format"].ToString(), styleCenter);
                        if (WebCaching.CurrentLanguage.ToString().Equals("zh-CN")) {
                            ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["USER_NAME_CN"].ToString(), styleCenter);
                        } else {
                            ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["USER_NAME_EN"].ToString(), styleCenter);
                        }
                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, SysConst.FormatControlTime(DateTime.Parse(dt.Rows[i]["SERVER_INSERT_TIME"].ToString())), styleCenter);
                    }
                    return Render(workbook);
                }
            }
        }

        /// <summary>
        /// 导出零售报告
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="_exportfilePath"></param>
        /// <returns></returns>
        public static byte[] ExportOrRmsReport(DataTable dt, string _exportfilePath, List<string> list) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);
                    //ExcelHelper.WriteCell(sheet1, 0, 0, "区域");
                    //ExcelHelper.WriteCell(sheet1, 1, 0, "店名");
                    //ExcelHelper.WriteCell(sheet1, 2, 0, "渠道");
                    //ExcelHelper.WriteCell(sheet1, 3, 0, "店长");
                    //ExcelHelper.WriteCell(sheet1, 4, 0, "区长");
                    //ExcelHelper.WriteCell(sheet1, 5, 0, "店铺管理");
                    //ExcelHelper.WriteCell(sheet1, 6, 0, "销售前的准备");
                    //ExcelHelper.WriteCell(sheet1, 7, 0, "销售指标");
                    //ExcelHelper.WriteCell(sheet1, 8, 0, "关于产品");
                    //ExcelHelper.WriteCell(sheet1, 9, 0, "关于人员");
                    //ExcelHelper.WriteCell(sheet1, 10, 0, "总分");
                    //ExcelHelper.WriteCell(sheet1, 11, 0, "跟进点");
                    ICellStyle style1 = workbook.CreateCellStyle();
                    style1.CloneStyleFrom(CellStyles[ExcelCellStyle.Style3]);
                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);

                    int cols = 0;
                    foreach (string item in list) {
                        ExcelConvertHelper.WriteCell(sheet1, cols, 0, item, style1);
                        cols++;
                    }


                    int iStartRow = 1;
                    int col = dt.Columns.Count;
                    for (int i = 0; i < dt.Rows.Count; i++) {
                        for (int j = 1; j < col; j++) {
                            ExcelConvertHelper.WriteCell(sheet1, j - 1, iStartRow + i, dt.Rows[i][j].ToString(), styleCenter);
                        }
                    }
                    return Render(workbook);
                }
            }
        }

        /// <summary>
        /// 服务标准报告
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="_exportfilePath"></param>
        /// <returns></returns>
        public static byte[] ExportOrAssReport(DataTable dt, string _exportfilePath) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);
                    //ExcelHelper.WriteCell(sheet1, 0, 0, "区域");
                    //ExcelHelper.WriteCell(sheet1, 1, 0, "店名");
                    //ExcelHelper.WriteCell(sheet1, 2, 0, "渠道");
                    //ExcelHelper.WriteCell(sheet1, 3, 0, "店长");
                    //ExcelHelper.WriteCell(sheet1, 4, 0, "区长");
                    //ExcelHelper.WriteCell(sheet1, 5, 0, "店员名字");
                    //ExcelHelper.WriteCell(sheet1, 6, 0, "得分");
                    //ExcelHelper.WriteCell(sheet1, 7, 0, "赞赏点");
                    //ExcelHelper.WriteCell(sheet1, 8, 0, "提升点");
                    //ExcelHelper.WriteCell(sheet1, 9, 0, "跟进点及总结");

                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
                    int iStartRow = 1;

                    for (int i = 0; i < dt.Rows.Count; i++) {
                        ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["adminRegion"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["STORE_CODE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["STORE_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["subChannel"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, dt.Rows[i]["contact"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 5, iStartRow + i, dt.Rows[i]["rsd"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 6, iStartRow + i, dt.Rows[i]["STAFF_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 7, iStartRow + i, dt.Rows[i]["SCORE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 8, iStartRow + i, dt.Rows[i]["ADMIRED_POINT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 9, iStartRow + i, dt.Rows[i]["ELEVATED_POINT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 10, iStartRow + i, dt.Rows[i]["SUMMARY"].ToString(), styleCenter);
                        if (WebCaching.CurrentLanguage.ToString().Equals("zh-CN"))
                            ExcelConvertHelper.WriteCell(sheet1, 11, iStartRow + i, dt.Rows[i]["USER_NAME_CN"].ToString(), styleCenter);
                        else
                            ExcelConvertHelper.WriteCell(sheet1, 11, iStartRow + i, dt.Rows[i]["USER_NAME_EN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 12, iStartRow + i, SysConst.FormatControlTime(Convert.ToDateTime(dt.Rows[i]["USER_SUBMIT_TIME"].ToString())), styleCenter);
                    }
                    return Render(workbook);
                }
            }
        }

        /// <summary>
        /// 导出库存报告 
        /// </summary>
        public static byte[] ExportOrInvReport(DataTable dt, string _exportfilePath) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);

                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
                    int iStartRow = 1;

                    for (int i = 0; i < dt.Rows.Count; i++) {
                        ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["STORE_CODE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["STORE_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["format"].ToString(), styleCenter);
                        if (WebCaching.CurrentLanguage.ToString().Equals("zh-CN"))
                            ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["USER_NAME_CN"].ToString(), styleCenter);
                        else
                            ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["USER_NAME_EN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, SysConst.FormatControlTime(DateTime.Parse(dt.Rows[i]["SERVER_INSERT_TIME"].ToString())), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 5, iStartRow + i, dt.Rows[i]["rsd"].ToString(), styleCenter);
                    }
                    return Render(workbook);
                }
            }
        }


        public static byte[] ExportFRStoreVisitReportByUser(DataTable dt, string _exportfilePath) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);


                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
                    int iStartRow = 3;


                    for (int i = 0; i < dt.Rows.Count; i++) {
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["RowNumber"].ToString(), styleCenter);
                        if (WebCaching.CurrentLanguage.ToString().Equals("zh-CN"))
                            ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["USER_NAME_CN"].ToString(), styleCenter);
                        else
                            ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["USER_NAME_EN"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["USER_TYPE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, dt.Rows[i]["NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 5, iStartRow + i, dt.Rows[i]["CITY_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 6, iStartRow + i, dt.Rows[i]["CITY_TIER"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 7, iStartRow + i, dt.Rows[i]["MANAGE_STORE_NUM"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 8, iStartRow + i, dt.Rows[i]["VISIT_STORE_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 9, iStartRow + i, dt.Rows[i]["PHOTO_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 10, iStartRow + i, dt.Rows[i]["CAMP_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 11, iStartRow + i, dt.Rows[i]["CHK_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 12, iStartRow + i, dt.Rows[i]["TOTAL_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 13, iStartRow + i, dt.Rows[i]["TOTAL_TIME"].ToString(), styleCenter);
                    }
                    return Render(workbook);
                }
            }
        }

        public static byte[] ExportFRStoreVisitReportByStore(DataTable dt, string _exportfilePath) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);

                    int iStartRow = 2;

                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);

                    for (int i = 0; i < dt.Rows.Count; i++) {
                        ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["RowNumber"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["STORE_CODE"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["STORE_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["PROVINCE_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, dt.Rows[i]["CITY_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 5, iStartRow + i, dt.Rows[i]["TIER"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 6, iStartRow + i, dt.Rows[i]["CUSTOMER_NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 7, iStartRow + i, dt.Rows[i]["PHOTO_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 8, iStartRow + i, dt.Rows[i]["CAMP_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 9, iStartRow + i, dt.Rows[i]["CHK_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 10, iStartRow + i, dt.Rows[i]["TOTAL_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 11, iStartRow + i, dt.Rows[i]["TOTAL_TIME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 12, iStartRow + i, dt.Rows[i]["AVG_SCORE"].ToString(), styleCenter);
                    }
                    return Render(workbook);
                }
            }
        }

        public static byte[] ExportFRStoreVisitReportByCustomer(DataTable dt, string _exportfilePath) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);


                    int iStartRow = 1;
                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);

                    for (int i = 0; i < dt.Rows.Count; i++) {
                        ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["RowNumber"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["NAME"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 2, iStartRow + i, dt.Rows[i]["ID"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 3, iStartRow + i, dt.Rows[i]["VISIT_STORE_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, dt.Rows[i]["PHOTO_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 5, iStartRow + i, dt.Rows[i]["CAMP_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 6, iStartRow + i, dt.Rows[i]["CHK_COUNT"].ToString(), styleCenter);
                        ExcelConvertHelper.WriteCell(sheet1, 7, iStartRow + i, dt.Rows[i]["TOTAL_SCORE"].ToString(), styleCenter);
                    }
                    return Render(workbook);
                }
            }
        }


        /// <summary>
        /// 导出陈列检查报告汇总
        /// </summary>
        public static byte[] ExportReportFrVm(DataTable dt, string _exportfilePath, Dictionary<string, string> dictionary) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);
                    ICellStyle styleCenterBoder = workbook.CreateCellStyle();
                    styleCenterBoder.CloneStyleFrom(CellStyles[ExcelCellStyle.Style7]);
                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);
                    ICellStyle styleLeft = workbook.CreateCellStyle();
                    styleLeft.CloneStyleFrom(CellStyles[ExcelCellStyle.Style2]);
                    ICellStyle styleRight = workbook.CreateCellStyle();
                    styleRight.CloneStyleFrom(CellStyles[ExcelCellStyle.Style4]);
                    ExcelConvertHelper.WriteCell(sheet1, 0, 0, dictionary["locCodeText"], styleCenterBoder);
                    ExcelConvertHelper.WriteCell(sheet1, 1, 0, dictionary["txtCodeText"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 3, 0, dictionary["locNameText"], styleCenterBoder);
                    ExcelConvertHelper.WriteCell(sheet1, 4, 0, dictionary["txtNameText"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 7, 0, dictionary["locCountText"], styleCenterBoder);
                    ExcelConvertHelper.WriteCell(sheet1, 8, 0, dictionary["txtCountText"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 9, 0, dictionary["locAvg"], styleCenterBoder);
                    ExcelConvertHelper.WriteCell(sheet1, 10, 0, dictionary["txtAvg"], styleCenter);

                    int iStartRow = 2;
                    for (int i = 0; i < dt.Rows.Count; i++) {
                        ExcelConvertHelper.WriteCell(sheet1, 0, 0, iStartRow + i, dt.Rows[i][0].ToString(), styleLeft);
                        ExcelConvertHelper.WriteCell(sheet1, 1, 9, iStartRow + i, dt.Rows[i][1].ToString(), styleLeft);
                        ExcelConvertHelper.WriteCell(sheet1, 10, 10, iStartRow + i, dt.Rows[i][2].ToString(), styleRight);

                    }
                    return Render(workbook);
                }
            }
        }

        /// <summary>
        /// 导出库存
        /// </summary>
        public static byte[] ExportORVMInventorySearch(DataTable dt, string _exportfilePath, Dictionary<string, string> dictionary) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);

                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);

                    ICellStyle styleLeft = workbook.CreateCellStyle();
                    styleLeft.CloneStyleFrom(CellStyles[ExcelCellStyle.Style2]);
                    styleLeft.WrapText = true;


                    ExcelConvertHelper.WriteCell(sheet1, 2, 4, 0, dictionary["STORE_NAME"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 6, 7, 0, dictionary["USER_NAME_CN"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 2, 4, 1, dictionary["CHECK_IN_TIME"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 6, 7, 1, dictionary["contact"], styleCenter);


                    int currentRow = 3;

                    if (dt != null && dt.Rows.Count > 0) {
                        string oldId = string.Empty;
                        int sameRow = 0;
                        if (dt.Rows[0]["OR_INV_CATEGORY_ID"] != null) {
                            oldId = dt.Rows[0]["OR_INV_CATEGORY_ID"].ToString();
                        }

                        for (int i = 0; i < dt.Rows.Count; i++) {
                            currentRow++;
                            if (dt.Rows[i]["OR_INV_CATEGORY_ID"].ToString() != oldId) {
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameRow, currentRow - 1, 0, 0));
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameRow, currentRow - 1, 1, 1));
                                oldId = dt.Rows[i]["OR_INV_CATEGORY_ID"].ToString();
                                sameRow = 1;
                            } else {
                                sameRow++;
                            }
                            ExcelConvertHelper.WriteCell(sheet1, 0, currentRow, dt.Rows[i]["ORDER_NO"].ToString(), styleCenter);
                            ExcelConvertHelper.WriteCell(sheet1, 1, currentRow, dt.Rows[i]["NAME_CN"].ToString(), styleCenter);

                            if (dt.Rows[i]["NAME_CN"].ToString() == "其它方面") {
                                ExcelConvertHelper.WriteCellWrap(sheet1, 2, 7, currentRow, dictionary["other"], styleLeft, workbook);
                            }
                            if (dt.Rows[i]["ITEM_NAME_CN"] != null) {
                                ExcelConvertHelper.WriteCellWrap(sheet1, 2, 5, currentRow, dt.Rows[i]["ITEM_NAME_CN"].ToString(), styleLeft, workbook);

                            }
                            if (dt.Rows[i]["CHECK_RESULT"] != null) {
                                if (dt.Rows[i]["CHECK_RESULT"].ToString() == YesNoType.Yes.GetHashCode().ToString()) {
                                    ExcelConvertHelper.WriteCell(sheet1, 6, 7, currentRow, "√", styleCenter);

                                } else {
                                    ExcelConvertHelper.WriteCell(sheet1, 6, 7, currentRow, "×", styleCenter);

                                }
                            }

                        }
                    }

                    currentRow++;
                    ExcelConvertHelper.WriteCell(sheet1, 0, 1, currentRow, "小结", styleCenter);

                    ExcelConvertHelper.WriteCellWrap(sheet1, 2, 7, currentRow, dictionary["CONCLUSION"], styleLeft, workbook);

                    return Render(workbook);
                }
            }
        }

        /// <summary>
        /// 自动换行写单元格
        /// </summary>
        public static void WriteCellWrap(ISheet sheet1, int firstCol, int lastCol, int row, string cellValue, ICellStyle style, HSSFWorkbook workbook) {
            string item = cellValue;
            int width = 0;
            sheet1.AddMergedRegion(new CellRangeAddress(row, row, firstCol, lastCol));
            for (int i = firstCol; i <= lastCol; i++) {
                double dwidth = Convert.ToDouble(sheet1.GetColumnWidth(i)) / 256;
                width = width + Convert.ToInt32((dwidth - 0.62) * 256);
            }
            short oldHeight = 300;
            short fontSize = style.GetFont(workbook).FontHeight;
            if (ExcelConvertHelper.RenStrLength(cellValue) * fontSize > width) {
                oldHeight = Convert.ToInt16((ExcelConvertHelper.RenStrLength(cellValue) * fontSize / width + 1) * 300);
                sheet1.GetRow(row).Height = oldHeight;
            }
            for (int i = firstCol; i <= lastCol; i++)
                ExcelConvertHelper.WriteCell(sheet1, i, row, item, style);
        }

        /// <summary>
        /// 字符长度
        /// </summary>
        public static int RenStrLength(string str) {
            int lenght = 0;
            if (string.IsNullOrEmpty(str)) {
                return lenght;
            } else {
                char[] strToArray = str.ToCharArray();
                string strSign = "。：；！.,:;!()（）";
                for (int i = 0; i < strToArray.Length; i++) {
                    if (Validator.IsChinese(strToArray[i].ToString()) || strSign.IndexOf(strToArray[i]) > 0) {
                        lenght = lenght + 2;
                    } else {
                        lenght++;
                    }
                }
            }
            return lenght;
        }

        /// <summary>
        /// 导出OR陈列检查前十后十项
        /// </summary>
        public static byte[] ExportORVMTop10Down10DetailReport(DataTable dt, string _exportfilePath, string strTitle, string strAnlyze, string locCode, string txtCode, string locName, string txtName, string locCount, string txtCount) {
            using (FileStream file = new FileStream(_exportfilePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);

                    ICellStyle style = workbook.CreateCellStyle();
                    NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    style.SetFont(font);
                    style.Alignment = HorizontalAlignment.CENTER;

                    sheet1.SetColumnWidth(0, 12 * 256);
                    sheet1.SetColumnWidth(1, 20 * 256);
                    sheet1.SetColumnWidth(2, 12 * 256);
                    sheet1.SetColumnWidth(3, 40 * 256);
                    sheet1.SetColumnWidth(4, 12 * 256);
                    sheet1.SetColumnWidth(5, 10 * 256);

                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 5));
                    ExcelConvertHelper.WriteCell(sheet1, 0, 0, strTitle + "--" + strAnlyze);

                    sheet1.GetRow(0).GetCell(0).CellStyle = style;

                    ExcelConvertHelper.WriteCell(sheet1, 0, 1, locCode);
                    ExcelConvertHelper.WriteCell(sheet1, 1, 1, txtCode);
                    ExcelConvertHelper.WriteCell(sheet1, 2, 1, locName);
                    ExcelConvertHelper.WriteCell(sheet1, 3, 1, txtName);
                    ExcelConvertHelper.WriteCell(sheet1, 4, 1, locCount);
                    ExcelConvertHelper.WriteCell(sheet1, 5, 1, txtCount);

                    int iStartRow = 3;

                    for (int i = 0; i < dt.Rows.Count; i++) {
                        sheet1.AddMergedRegion(new CellRangeAddress(iStartRow + i, iStartRow + i, 1, 3));
                        ExcelConvertHelper.WriteCell(sheet1, 0, iStartRow + i, dt.Rows[i]["RANKING"].ToString());
                        ExcelConvertHelper.WriteCell(sheet1, 1, iStartRow + i, dt.Rows[i]["ITEM_NAME_CN"].ToString());
                        ExcelConvertHelper.WriteCell(sheet1, 4, iStartRow + i, dt.Rows[i]["SUM_SCORE"].ToString());

                    }
                    return Render(workbook);
                }
            }
        }

        /// <summary>
        /// 导出分销零售报告
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filePath"></param>
        /// <param name="list"></param>
        /// <param name="listTitle"></param>
        /// <param name="listActual"></param>
        /// <param name="listStander"></param>
        /// <returns></returns>
        public static byte[] ExportFRRmsSearchDetail(DataTable dt, string filePath, List<string> list, List<string> listTitle, List<string> listActual, List<string> listStander) {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);

                    ICellStyle styleRight = workbook.CreateCellStyle();
                    styleRight.CloneStyleFrom(CellStyles[ExcelCellStyle.Style4]);

                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);

                    //CellStyle styleCenterBolder = workbook.CreateCellStyle();
                    //styleCenterBolder.CloneStyleFrom(CellStyles[ExcelCellStyle.Style2]);

                    ICellStyle styleCenterNoBackGroud = workbook.CreateCellStyle();
                    styleCenterNoBackGroud.CloneStyleFrom(CellStyles[ExcelCellStyle.Style7]);

                    ICellStyle styleLeft = workbook.CreateCellStyle();
                    styleLeft.CloneStyleFrom(CellStyles[ExcelCellStyle.Style2]);

                    ICellStyle styleMerger = workbook.CreateCellStyle();
                    styleMerger.CloneStyleFrom(CellStyles[ExcelCellStyle.Style10]);

                    int flagCol = 3;
                    foreach (string str in list) {
                        ExcelConvertHelper.WriteCell(sheet1, flagCol, 0, str, styleCenter);
                        flagCol = flagCol + 2;
                    }
                    flagCol = 1;
                    foreach (string str in listTitle) {
                        ExcelConvertHelper.WriteCell(sheet1, flagCol, 1, str, styleCenterNoBackGroud);
                        flagCol++;
                    }
                    flagCol = 1;
                    foreach (string str in listActual) {
                        ExcelConvertHelper.WriteCell(sheet1, flagCol, 2, str, styleCenter);
                        flagCol++;
                    }
                    flagCol = 1;
                    foreach (string str in listStander) {
                        ExcelConvertHelper.WriteCell(sheet1, flagCol, 3, str, styleCenter);
                        flagCol++;
                    }
                    int currentRow = 5;
                    if (dt != null && dt.Rows.Count > 0) {
                        string oldParentId = string.Empty;
                        string oldChildId = string.Empty;
                        string prefixId = string.Empty;
                        int sameParent = 0;
                        int sameChild = 0;

                        int count = 0;

                        if (dt.Rows[0]["ParentId"] != null) {
                            oldParentId = dt.Rows[0]["ParentId"].ToString();
                        }
                        if (dt.Rows[0]["ChildId"] != null) {
                            oldChildId = dt.Rows[0]["ChildId"].ToString();
                        }
                        prefixId = oldParentId;
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            currentRow++;
                            count++;

                            if (dt.Rows[i]["ParentId"].ToString() != oldParentId) {
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameParent, currentRow - 1, 0, 0));
                                prefixId = oldParentId;
                                oldParentId = dt.Rows[i]["ParentId"].ToString();
                                sameParent = 1;
                            } else {
                                sameParent++;
                            }
                            ExcelConvertHelper.WriteCell(sheet1, 0, currentRow, dt.Rows[i]["PARENT"].ToString(), styleLeft);

                            if (dt.Rows[i]["ChildId"].ToString() != oldChildId || (dt.Rows[i]["ChildId"].ToString() == "" && prefixId != dt.Rows[i]["ParentId"].ToString())) {
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameChild, currentRow - 1, 1, 1));
                                oldChildId = dt.Rows[i]["ChildId"].ToString();
                                sameChild = 1;
                            } else {
                                sameChild++;
                            }
                            ExcelConvertHelper.WriteCell(sheet1, 1, currentRow, dt.Rows[i]["CHILD"].ToString(), styleLeft);
                            ExcelConvertHelper.WriteCell(sheet1, 2, 5, currentRow, dt.Rows[i]["ITEM_NAME_CN"].ToString(), styleMerger);
                            ExcelConvertHelper.WriteCell(sheet1, 6, currentRow, dt.Rows[i]["SCORE_OPTION"].ToString(), styleLeft);
                            if (dt.Rows[i]["SCORE"].ToString() == "-1") {
                                ExcelConvertHelper.WriteCell(sheet1, 7, currentRow, "N/A", styleRight);
                            } else {
                                ExcelConvertHelper.WriteCell(sheet1, 7, currentRow, dt.Rows[i]["SCORE"].ToString(), styleRight);
                            }
                            ExcelConvertHelper.WriteCell(sheet1, 8, currentRow, dt.Rows[i]["REMARK"].ToString(), styleLeft);
                            if (i == dt.Rows.Count - 1) {
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameParent + 1, currentRow, 0, 0));
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameChild + 1, currentRow, 1, 1));
                            }
                        }
                    }
                    return Render(workbook);
                }
            }
        }


        /// <summary>
        /// 导出分销陈列详细报告
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filePath"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static byte[] ExportFRVMSearchDetail(DataTable dt, string filePath, List<string> list) {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);

                    ICellStyle styleRight = workbook.CreateCellStyle();
                    styleRight.CloneStyleFrom(CellStyles[ExcelCellStyle.Style4]);

                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);

                    ICellStyle styleCenterNoBorder = workbook.CreateCellStyle();
                    styleCenterNoBorder.CloneStyleFrom(CellStyles[ExcelCellStyle.Style8]);

                    ICellStyle styleLeft = workbook.CreateCellStyle();
                    styleLeft.CloneStyleFrom(CellStyles[ExcelCellStyle.Style2]);

                    int flagRow = 0;
                    int flagCol = 5;
                    foreach (string str in list) {
                        if (flagRow % 2 == 0) {
                            flagCol = 2;
                            ExcelConvertHelper.WriteCell(sheet1, flagCol, flagRow / 2, str);
                        } else {
                            flagCol = 4;
                            ExcelConvertHelper.WriteCell(sheet1, flagCol, flagCol + 1, flagRow / 2, str);
                        }

                        flagRow++;
                    }
                    int currentRow = 5;
                    int order = 1;
                    if (dt != null && dt.Rows.Count > 0) {

                        string oldCategoryID = string.Empty;
                        string oldItemId = string.Empty;
                        int iCategoryCount = 0;
                        int iItemCount = 0;
                        if (dt.Rows[0]["FR_VM_CATEGORY_ID"] != null) {
                            oldCategoryID = dt.Rows[0]["FR_VM_CATEGORY_ID"].ToString();
                        }
                        if (dt.Rows[0]["FR_VM_ITEM_ID"] != null) {
                            oldItemId = dt.Rows[0]["FR_VM_ITEM_ID"].ToString();
                        }
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            order++;
                            currentRow++;
                            if (dt.Rows[i]["FR_VM_CATEGORY_ID"].ToString() != oldCategoryID) {
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - iCategoryCount, currentRow - 1, 0, 0));
                                oldCategoryID = dt.Rows[i]["FR_VM_CATEGORY_ID"].ToString();
                                iCategoryCount = 1;
                            } else {
                                iCategoryCount++;
                            }
                            ExcelConvertHelper.WriteCell(sheet1, 0, currentRow, dt.Rows[i]["CATEGORY_NAME_CN"].ToString(), styleLeft);
                            if (dt.Rows[i]["FR_VM_ITEM_ID"].ToString() != oldItemId) {

                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - iItemCount, currentRow - 1, 1, 1));
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - iItemCount, currentRow - 1, 2, 2));
                                oldItemId = dt.Rows[i]["FR_VM_ITEM_ID"].ToString();
                                iItemCount = 1;
                            } else {

                                iItemCount++;
                                order--;
                            }
                            ExcelConvertHelper.WriteCell(sheet1, 1, currentRow, order.ToString(), styleLeft);
                            ExcelConvertHelper.WriteCell(sheet1, 2, currentRow, dt.Rows[i]["ITEM_NAME_CN"].ToString(), styleLeft);
                            ExcelConvertHelper.WriteCell(sheet1, 3, currentRow, dt.Rows[i]["SCORE_OPTION"].ToString(), styleLeft);
                            if (dt.Rows[i]["SCORE"].ToString() == "-1") {
                                ExcelConvertHelper.WriteCell(sheet1, 4, currentRow, "N/A", styleRight);
                            } else {
                                ExcelConvertHelper.WriteCell(sheet1, 4, currentRow, dt.Rows[i]["SCORE"].ToString(), styleRight);
                            }
                            ExcelConvertHelper.WriteCell(sheet1, 5, 5, currentRow, dt.Rows[i]["REMARK"].ToString(), styleLeft);
                            if (i == dt.Rows.Count - 1) {

                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - iCategoryCount + 1, currentRow, 0, 0));

                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - iItemCount + 1, currentRow, 1, 1));

                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - iItemCount + 1, currentRow, 2, 2));
                            }



                        }
                    }
                    return Render(workbook);
                }
            }
        }
        /// <summary>
        /// 导出陈列检查详细报告
        /// </summary>
        public static byte[] ExportORVMSearchDetail(DataTable dtItemGrid, DataTable dtStaffGrid, string filePath, Dictionary<string, string> dictionary) {

            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                //using (HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file))
                {
                    HSSFWorkbook workbook = ExcelConvertHelper.CopyWorkbook(file);
                    ISheet sheet1 = workbook.GetSheetAt(0);

                    ICellStyle styleCenterBack = workbook.CreateCellStyle();
                    styleCenterBack.CloneStyleFrom(CellStyles[ExcelCellStyle.Style3]);

                    ICellStyle styleCenter = workbook.CreateCellStyle();
                    styleCenter.CloneStyleFrom(CellStyles[ExcelCellStyle.Style6]);

                    ICellStyle styleLeft = workbook.CreateCellStyle();
                    styleLeft.CloneStyleFrom(CellStyles[ExcelCellStyle.Style2]);
                    styleLeft.WrapText = true;

                    ICellStyle styleRight = workbook.CreateCellStyle();
                    styleRight.CloneStyleFrom(CellStyles[ExcelCellStyle.Style4]);

                    ICellStyle styleCenterNoBg = workbook.CreateCellStyle();
                    styleCenterNoBg.CloneStyleFrom(CellStyles[ExcelCellStyle.Style7]);

                    ExcelConvertHelper.WriteCell(sheet1, 4, 4, dictionary["adminRegion"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 7, 4, dictionary["nameCN"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 10, 4, dictionary["rsd"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 4, 5, dictionary["STORE_NAME"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 7, 5, dictionary["format"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 10, 5, dictionary["contact"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 4, 6, dictionary["USER_ACCOUNT"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 7, 6, dictionary["CHECK_IN_TIME"], styleCenter);
                    ExcelConvertHelper.WriteCell(sheet1, 10, 6, dictionary["CHECK_OUT_TIME"], styleCenter);


                    int MaxLenght = 0;
                    if (dtItemGrid != null && dtItemGrid.Rows.Count > 0) {
                        for (int i = 0; i < dtItemGrid.Rows.Count; i++) {
                            if (dtItemGrid.Rows[i]["SCORE_OPTION"].ToString().Split(',').Length > MaxLenght) {
                                MaxLenght = dtItemGrid.Rows[i]["SCORE_OPTION"].ToString().Split(',').Length;
                            }
                        }
                    }
                    if (dtStaffGrid != null && dtStaffGrid.Rows.Count > 0) {
                        for (int i = 0; i < dtStaffGrid.Rows.Count; i++) {
                            if (dtStaffGrid.Rows[i]["SCORE_OPTION"].ToString().Split(',').Length > MaxLenght) {
                                MaxLenght = dtStaffGrid.Rows[i]["SCORE_OPTION"].ToString().Split(',').Length;
                            }
                        }
                    }

                    int currentRow = 8;

                    ExcelConvertHelper.WriteCell(sheet1, 0, 8 + MaxLenght + 3, currentRow, dictionary["orVmItemGridTitle"], styleCenterNoBg);



                    currentRow++;
                    ExcelConvertHelper.WriteCell(sheet1, 0, 0, currentRow, dictionary["weight"], styleCenterBack);
                    ExcelConvertHelper.WriteCell(sheet1, 1, 7, currentRow, dictionary["item"], styleCenterBack);
                    ExcelConvertHelper.WriteCell(sheet1, 8, 8 + MaxLenght - 1, currentRow, dictionary["scores"], styleCenterBack);
                    ExcelConvertHelper.WriteCell(sheet1, 9 + MaxLenght - 1, 9 + MaxLenght - 1, currentRow, dictionary["score"], styleCenterBack);
                    ExcelConvertHelper.WriteCell(sheet1, 10 + MaxLenght - 1, 12 + MaxLenght - 1, currentRow, dictionary["remark"], styleCenterBack);


                    if (dtItemGrid != null && dtItemGrid.Rows.Count > 0) {
                        string oldParentId = string.Empty;
                        string oldChildId = string.Empty;
                        string prefixId = string.Empty;

                        int sameParent = 0;
                        int sameChild = 0;
                        bool IsChild = true;

                        int count = 0;

                        if (dtItemGrid.Rows[0]["ParentID"] != null) {
                            oldParentId = dtItemGrid.Rows[0]["ParentID"].ToString();
                        }
                        if (dtItemGrid.Rows[0]["ChildID"] != null) {
                            oldChildId = dtItemGrid.Rows[0]["ChildID"].ToString();
                        }

                        for (int i = 0; i < dtItemGrid.Rows.Count; i++) {
                            currentRow++;
                            count++;


                            if (dtItemGrid.Rows[i]["ParentID"].ToString() != oldParentId) {
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameParent, currentRow - 1, 0, 0));
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameParent, currentRow - 1, 1, 1));
                                oldParentId = dtItemGrid.Rows[i]["ParentID"].ToString();
                                sameParent = 1;
                            } else {
                                sameParent++;
                            }

                            ExcelConvertHelper.WriteCell(sheet1, 0, currentRow, Math.Round((Convert.ToDecimal(dtItemGrid.Rows[i]["WEIGHT"].ToString()) * 100), 0).ToString() + "%", styleCenter);//style3
                            ExcelConvertHelper.WriteCell(sheet1, 1, currentRow, dtItemGrid.Rows[i]["ParentName"].ToString(), styleCenter);//style3


                            if (dtItemGrid.Rows[i]["ChildID"].ToString() != "") {
                                IsChild = true;

                                if (dtItemGrid.Rows[i]["ChildID"].ToString() != oldChildId && IsChild) {
                                    sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameChild, currentRow - 1, 2, 2));
                                    oldChildId = dtItemGrid.Rows[i]["ChildID"].ToString();
                                    sameChild = 1;
                                } else {
                                    sameChild++;
                                }


                                ExcelConvertHelper.WriteCell(sheet1, 2, currentRow, dtItemGrid.Rows[i]["ChildName"].ToString(), styleCenter);
                                ExcelConvertHelper.WriteCellWrap(sheet1, 3, 7, currentRow, dtItemGrid.Rows[i]["ItemName"].ToString(), styleLeft, workbook);

                            } else {
                                IsChild = false;
                                ExcelConvertHelper.WriteCellWrap(sheet1, 2, 7, currentRow, dtItemGrid.Rows[i]["ItemName"].ToString(), styleLeft, workbook);
                                sameChild = 1;
                            }
                            int currentCol = 8;
                            if (dtItemGrid.Rows[i]["SCORE_OPTION"] != null) {
                                string[] scoresOption = dtItemGrid.Rows[i]["SCORE_OPTION"].ToString().Split(',');

                                if (scoresOption.Length < MaxLenght) {
                                    for (int j = 0; j < MaxLenght - scoresOption.Length; j++) {
                                        ExcelConvertHelper.WriteCell(sheet1, currentCol + j, currentRow, @"\", styleCenter);
                                    }
                                }
                                currentCol = currentCol + MaxLenght - scoresOption.Length;
                                for (int j = 0; j < scoresOption.Length; j++) {
                                    ExcelConvertHelper.WriteCell(sheet1, currentCol + j, currentRow, scoresOption[j], styleCenter);
                                }
                                currentCol = 8 + MaxLenght;
                            }
                            if (dtItemGrid.Rows[i]["SCORE"] != null) {
                                if (dtItemGrid.Rows[i]["SCORE"].ToString() == "-1") {
                                    ExcelConvertHelper.WriteCell(sheet1, currentCol, currentRow, "N/A", styleRight);
                                } else {
                                    ExcelConvertHelper.WriteCell(sheet1, currentCol, currentRow, dtItemGrid.Rows[i]["SCORE"].ToString(), styleRight);
                                }
                            }
                            if (dtItemGrid.Rows[i]["REMARK"] != null) {
                                ExcelConvertHelper.WriteCell(sheet1, currentCol + 1, currentCol + 3, currentRow, dtItemGrid.Rows[i]["REMARK"].ToString(), styleLeft);
                            }
                            if (i == dtItemGrid.Rows.Count - 1) {
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameParent + 1, currentRow, 0, 0));
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameParent + 1, currentRow, 1, 1));
                            }
                        }
                    }

                    currentRow++;
                    ExcelConvertHelper.WriteCell(sheet1, 0, 8 + MaxLenght + 3, currentRow, dictionary["orVmStaffGridTitle"], styleCenterNoBg);

                    currentRow++;
                    ExcelConvertHelper.WriteCell(sheet1, 0, 0, currentRow, dictionary["weight"], styleCenterBack);
                    ExcelConvertHelper.WriteCell(sheet1, 1, 1, currentRow, dictionary["staff"], styleCenterBack);
                    ExcelConvertHelper.WriteCell(sheet1, 2, 7, currentRow, dictionary["item"], styleCenterBack);
                    ExcelConvertHelper.WriteCell(sheet1, 8, 8 + MaxLenght - 1, currentRow, dictionary["scores"], styleCenterBack);
                    ExcelConvertHelper.WriteCell(sheet1, 9 + MaxLenght - 1, 9 + MaxLenght - 1, currentRow, dictionary["score"], styleCenterBack);
                    ExcelConvertHelper.WriteCell(sheet1, 10 + MaxLenght - 1, 12 + MaxLenght - 1, currentRow, dictionary["remark"], styleCenterBack);

                    if (dtStaffGrid != null && dtStaffGrid.Rows.Count > 0) {
                        string oldParentId = string.Empty;
                        int sameParent = 0;
                        if (dtItemGrid.Rows[0]["ParentID"] != null) {
                            oldParentId = dtStaffGrid.Rows[0]["ParentID"].ToString();
                        }

                        for (int i = 0; i < dtStaffGrid.Rows.Count; i++) {
                            currentRow++;
                            if (dtStaffGrid.Rows[i]["ParentID"].ToString() != oldParentId) {
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameParent, currentRow - 1, 0, 0));
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameParent, currentRow - 1, 1, 1));
                                oldParentId = dtStaffGrid.Rows[i]["ParentID"].ToString();
                                sameParent = 1;
                            } else {
                                sameParent++;
                            }
                            ExcelConvertHelper.WriteCell(sheet1, 0, currentRow, Math.Round((Convert.ToDecimal(dtStaffGrid.Rows[i]["WEIGHT"].ToString()) * 100), 0).ToString() + "%", styleCenter);
                            ExcelConvertHelper.WriteCell(sheet1, 1, currentRow, dtStaffGrid.Rows[i]["STAFF_NAME"].ToString(), styleCenter);


                            ExcelConvertHelper.WriteCell(sheet1, 2, 7, currentRow, dtStaffGrid.Rows[i]["ItemName"].ToString(), styleLeft);

                            int currentCol = 8;
                            if (dtStaffGrid.Rows[i]["SCORE_OPTION"] != null) {
                                string[] scoresOption = dtStaffGrid.Rows[i]["SCORE_OPTION"].ToString().Split(',');

                                if (scoresOption.Length < MaxLenght) {
                                    for (int j = 0; j < MaxLenght - scoresOption.Length; j++) {
                                        ExcelConvertHelper.WriteCell(sheet1, currentCol + j, currentRow, @"\", styleCenter);
                                    }
                                }
                                currentCol = currentCol + MaxLenght - scoresOption.Length;
                                for (int j = 0; j < scoresOption.Length; j++) {
                                    ExcelConvertHelper.WriteCell(sheet1, currentCol + j, currentRow, scoresOption[j], styleCenter);
                                }
                                currentCol = 8 + MaxLenght;
                            }
                            if (dtStaffGrid.Rows[i]["SCORE"] != null) {
                                if (dtStaffGrid.Rows[i]["SCORE"].ToString() == "-1") {
                                    ExcelConvertHelper.WriteCell(sheet1, currentCol, currentRow, "N/A", styleRight);
                                } else {
                                    ExcelConvertHelper.WriteCell(sheet1, currentCol, currentRow, dtStaffGrid.Rows[i]["SCORE"].ToString(), styleRight);
                                }
                            }
                            if (dtStaffGrid.Rows[i]["REMARK"] != null) {
                                ExcelConvertHelper.WriteCell(sheet1, currentCol + 1, currentCol + 3, currentRow, dtStaffGrid.Rows[i]["REMARK"].ToString(), styleLeft);
                            }
                            if (i == dtStaffGrid.Rows.Count - 1) {
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameParent + 1, currentRow, 0, 0));
                                sheet1.AddMergedRegion(new CellRangeAddress(currentRow - sameParent + 1, currentRow, 1, 1));
                            }
                        }
                    }

                    currentRow++;
                    ExcelConvertHelper.WriteCell(sheet1, 0, 7, currentRow, dictionary["locTotalScore"], styleLeft);
                    ExcelConvertHelper.WriteCell(sheet1, 8, 8 + MaxLenght + 3, currentRow, dictionary["txtTotalScore"], styleLeft);

                    if (!string.IsNullOrEmpty(dictionary["txtLoseReseaion"])) {
                        currentRow++;
                        ExcelConvertHelper.WriteCell(sheet1, 0, 1, currentRow, dictionary["loseReseasion"], styleLeft);
                        ExcelConvertHelper.WriteCell(sheet1, 2, 8 + MaxLenght + 3, currentRow, dictionary["txtLoseReseaion"], styleLeft);
                    }

                    currentRow++;
                    ExcelConvertHelper.WriteCell(sheet1, 0, 1, currentRow, dictionary["remark"], styleLeft);
                    ExcelConvertHelper.WriteCell(sheet1, 2, 8 + MaxLenght + 3, currentRow, dictionary["interpret1"], styleLeft);

                    currentRow++;
                    ExcelConvertHelper.WriteCell(sheet1, 2, 8 + MaxLenght + 3, currentRow, dictionary["interpret2"], styleLeft);
                    currentRow++;
                    ExcelConvertHelper.WriteCell(sheet1, 2, 8 + MaxLenght + 3, currentRow, dictionary["interpret3"], styleLeft);

                    currentRow++;
                    ExcelConvertHelper.WriteCell(sheet1, 2, 8 + MaxLenght + 3, currentRow, dictionary["interpret4"], styleLeft);

                    return Render(workbook);
                }
            }

        }


        public static void WriteCell(ISheet sheet1, int firstCol, int lastCol, int row, string cellValue, ICellStyle style) {
            sheet1.AddMergedRegion(new CellRangeAddress(row, row, firstCol, lastCol));
            for (int i = firstCol; i <= lastCol; i++)
                ExcelConvertHelper.WriteCell(sheet1, i, row, cellValue, style);


        }

        public static void WriteCell(ISheet sheet1, int firstCol, int lastCol, int row, string cellValue) {
            sheet1.AddMergedRegion(new CellRangeAddress(row, row, firstCol, lastCol));
            ExcelConvertHelper.WriteCell(sheet1, firstCol, row, cellValue);
        }

        public static void WriteCell(ISheet sheet1, int col, int row, string cellValue, ICellStyle style) {
            ExcelConvertHelper.WriteCell(sheet1, col, row, cellValue);
            sheet1.GetRow(row).GetCell(col).CellStyle = style;


        }


        public static void WriteObjCell(ISheet sheet1, int col, int row, object cellValue, ICellStyle style) {
            ExcelConvertHelper.WriteCell(sheet1, col, row, cellValue);
            sheet1.GetRow(row).GetCell(col).CellStyle = style;

        }

        public static string GetEnumDescript(int iEnumHasCode, Type enumType) {
            FieldInfo[] fieldinfos = enumType.GetFields();

            if (fieldinfos != null) {
                foreach (FieldInfo field in fieldinfos) {

                    Object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (objs != null && objs.Length > 0) {
                        if (Enum.Parse(enumType, field.Name).GetHashCode() == iEnumHasCode) {
                            DescriptionAttribute da = (DescriptionAttribute)objs[0];
                            return da.Description;
                        }
                    }
                }
            }

            return "";
        }

        public static byte[] Export(DataTable dt) {
            return Export(dt, 28 * 256);
        }

        public static byte[] Export(DataTable dt, int defaultColumnWidth) {
            HSSFWorkbook workbook = CreateWorkbook();

            CreateCurrentWorkbookStyles(workbook);

            ISheet sheet = workbook.CreateSheet("Sheet1");
            WriteTable(workbook, sheet, dt);
            IRow row = sheet.GetRow(0);
            for (int i = row.FirstCellNum; i < row.LastCellNum; i++) {
                sheet.SetColumnWidth(i, defaultColumnWidth);
            }

            return Render(workbook);
        }

        /// <summary>
        /// 创建用于当前工作簿的样式
        /// </summary>
        /// <param name="workbook"></param>
        private static void CreateCurrentWorkbookStyles(HSSFWorkbook workbook) {
            CurrentWorkbookCellStyles = new Dictionary<string, ICellStyle>();
            foreach (string csKey in CellStyles.Keys) {
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
        public static byte[] Render(HSSFWorkbook workbook) {
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            return stream.ToArray();
        }

        /// <summary>
        /// 创建excel workbook
        /// </summary>
        /// <returns></returns>
        public static HSSFWorkbook CreateWorkbook() {
            HSSFWorkbook workbook = new HSSFWorkbook();
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "VSA";
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "VSA System";
            workbook.DocumentSummaryInformation = dsi;
            workbook.SummaryInformation = si;
            return workbook;
        }

        public static HSSFWorkbook CopyWorkbook(FileStream file) {
            //FileStream file = new FileStream(templatePath, FileMode.Open, FileAccess.Read);

            HSSFWorkbook workbook = new HSSFWorkbook(file);

            CreateCurrentWorkbookStyles(workbook);

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "VSA";
            workbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "VSA System";
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
        public static void WriteCurstomerCustomProperties(HSSFWorkbook wookBook, string projectId, string userRole, List<string> planInfoList) {
            string result = string.Empty;
            result = projectId + "\r\n" + userRole + "\r\n";
            foreach (string item in planInfoList) {
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
        public static void SetTableHeader(HSSFWorkbook workbook, int rowIndex) {
            ICellStyle style1 = workbook.CreateCellStyle();
            style1.CloneStyleFrom(CellStyles[ExcelCellStyle.Style1]);
            IRow row = workbook.GetSheet("Sheet1").GetRow(rowIndex);
            for (int i = row.FirstCellNum; i < row.LastCellNum; i++) {
                row.GetCell(i).CellStyle = style1;
            }
        }

        /// <summary>
        /// 给Sheet加密码
        /// </summary>
        /// <param name="sheet"></param>
        public static void AddPassword(HSSFSheet sheet) {
            sheet.ProtectSheet(Utils.MD5Encode(DateTime.Now.ToString("yyyyMMdd")).Substring(2, 8));
        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="sheet"></param>
        /// <param name="columnIndex"></param>
        public static void UnlockColumn(HSSFWorkbook workbook, HSSFSheet sheet, int columnIndex) {
            ICellStyle unlocked = workbook.CreateCellStyle();
            unlocked.IsLocked = false;
            for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++) {
                sheet.GetRow(i).GetCell(columnIndex).CellStyle = unlocked;
            }
        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="sheet"></param>
        /// <param name="columnIndex"></param>
        public static void UnlockCell(HSSFWorkbook workbook, ISheet sheet, int rowIndex, int columnIndex) {
            ICellStyle unlocked = workbook.CreateCellStyle();
            unlocked.IsLocked = false;
            sheet.GetRow(rowIndex).GetCell(columnIndex).CellStyle = unlocked;
        }

        /// <summary>
        /// 往sheet写入table
        /// </summary>
        public static void WriteTable(HSSFWorkbook workbook, ISheet sheet, DataTable dt) {
            WriteTable(workbook, sheet, dt, 0);
        }

        /// <summary>
        /// 往sheet写入table
        /// </summary>
        public static void WriteTable(HSSFWorkbook workbook, ISheet sheet, DataTable dt, int rowIndex) {
            WriteTable(workbook, sheet, dt, rowIndex, 0);
        }

        /// <summary>
        /// 往sheet写入table
        /// </summary>
        public static void WriteTable(HSSFWorkbook workbook, ISheet sheet, DataTable dt, int rowIndex, int columnIndex) {
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
        public static void WriteTable(HSSFWorkbook workbook, ISheet sheet, DataTable dt, int rowIndex, int columnIndex, bool writeHeader, bool useCaption) {
            string[] header = new string[dt.Columns.Count];
            for (int i = 0; i < header.Length; i++) {
                if (useCaption) {
                    header[i] = dt.Columns[i].Caption;
                } else {
                    header[i] = dt.Columns[i].ColumnName;
                }
            }
            if (writeHeader) {
                ICellStyle style1 = workbook.CreateCellStyle();
                style1.CloneStyleFrom(CellStyles[ExcelCellStyle.Style1]);
                IRow row = WriteRow(sheet, header, rowIndex, columnIndex);
                for (int i = row.FirstCellNum; i < row.LastCellNum; i++) {
                    row.GetCell(i).CellStyle = style1;
                }
            }
            rowIndex++;
            ICellStyle style2 = workbook.CreateCellStyle();
            style2.CloneStyleFrom(CellStyles[ExcelCellStyle.Style2]);
            foreach (DataRow dr in dt.Rows) {
                IRow row = WriteRow(sheet, dr.ItemArray, rowIndex, columnIndex);

                rowIndex++;
            }
            //sheet.DisplayGridlines = false;
            //for (int i = columnIndex; i < columnIndex+dt.Columns.Count; i++)
            //{
            //    sheet.AutoSizeColumn(i, true);
            //}
        }

        /// <summary>
        /// 写入行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dr"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static IRow WriteRow(ISheet sheet, DataRow dr, int rowIndex, int columnIndex) {
            return WriteRow(sheet, dr.ItemArray, rowIndex, columnIndex);
        }

        /// <summary>
        /// 写入行
        /// </summary>
        public static IRow WriteRow(ISheet sheet, object[] dr, int rowIndex, int columnIndex) {
            IRow row = sheet.CreateRow(rowIndex);
            int i = 0;
            while (i < dr.Length) {
                WriteCell(row, dr[i], i);
                i++;
                columnIndex++;
            }
            return row;
        }

        /// <summary>
        /// 写入单元格
        /// </summary>
        public static ICell WriteCell(IRow row, object obj, int columnIndex) {
            ICell cell = row.GetCell(columnIndex);
            if (cell == null)
                cell = row.CreateCell(columnIndex);
            if (obj is string) {
                cell.SetCellValue((string)obj);
            } else if (obj is decimal) {
                cell.SetCellValue(Convert.ToDouble(obj));
            } else if (obj is DateTime) {
                cell.SetCellValue((DateTime)obj);
                if (CurrentWorkbookCellStyles.ContainsKey("DateTime")) {
                    cell.CellStyle = CurrentWorkbookCellStyles["DateTime"];
                }
            } else if (obj is bool) {
                cell.SetCellValue(obj.ToString());
            } else if (obj.GetType().IsValueType) {
                cell.SetCellValue(Convert.ToDouble(obj));
            } else {
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
        public static ICell WriteCell(ISheet sheet, int columnIndex, int rowIndex, object obj) {
            IRow row = sheet.GetRow(rowIndex);
            if (row == null) {
                row = sheet.CreateRow(rowIndex);
            }
            return WriteCell(row, obj, columnIndex);
        }

        /// <summary>
        /// 在一个Cell内插入图片
        /// </summary>
        public static void WirtePic(HSSFWorkbook workbook, ISheet sheet, HSSFPatriarch patriarch, int columnIndex, int rowIndex, string picPath) {
            WirtePic(workbook, sheet, patriarch, columnIndex, columnIndex, rowIndex, picPath);
        }

        /// <summary>
        /// 在指定位置插入图片，跨Column。ColumnIndex2需大于或等于ColumnIndex1
        /// </summary>
        public static void WirtePic(HSSFWorkbook workbook, ISheet sheet, HSSFPatriarch patriarch, int columnIndex1, int columnIndex2, int rowIndex, string picPath) {
            if (!File.Exists(picPath)) {
                return;
            }
            if (columnIndex2 < columnIndex1) {
                throw new Exception("ColumnIndex2需大于或等于ColumnIndex1");
            }
            int dx2 = 1023;
            int dy2 = 255;
            IRow row = sheet.GetRow(rowIndex);
            if (row != null) {
                int cWidth = 0;
                for (int i = columnIndex1; i < columnIndex2; i++) {
                    cWidth += sheet.GetColumnWidth(columnIndex1);
                }
                using (Image img = Image.FromFile(picPath)) {
                    double w = cWidth * 7.0 / 256.0;
                    double h = (row.Height * 1.32 / 20.0);
                    if (((double)img.Width / (double)img.Height) > (cWidth * 7.0 / 256 / (row.Height * 1.32 / 20))) {
                        double h1 = w * img.Height / img.Width;
                        dy2 = (int)(h1 * 255 / h);
                        if (dy2 < 0) dy2 = 0;
                        if (dy2 > 255) dy2 = 255;
                    } else {
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

        public static object GetValue(HSSFCell cell) {
            if (cell == null)
                return null;
            switch (cell.CellType) {
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

        public static object GetIcellValue(ICell cell)
        {
           if (cell == null)
                return "";
           switch (cell.CellType)
           {
               case CellType.BLANK: //BLANK:
                   //return null;
                   return "";
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
                   return "=" + cell.CellFormula;
           }
        }

        public static DataSet ReadExcel(string filePath, int sheetCount, Dictionary<int, int> sheetFirstDataRow)
        {
            DataSet ds = new DataSet();            
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook hssfworkbook = null;
                if (filePath.IndexOf(".xlsx") > 0) // 2007版本
                {
                    hssfworkbook = new NPOI.XSSF.UserModel.XSSFWorkbook(file);
                }
                else if (filePath.IndexOf(".xls") > 0) // 2003版本
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
                for (int k = 0; k < sheetCount; k++)
                {
                    ISheet s = hssfworkbook.GetSheetAt(k);
                    int firstDataRow = 1;
                    if (sheetFirstDataRow != null && sheetFirstDataRow.Count > 0 && sheetFirstDataRow.ContainsKey(k + 1))
                    {
                        firstDataRow = sheetFirstDataRow[k + 1] - 1;
                    }
                    IRow header = s.GetRow(firstDataRow - 1);
                    DataTable dt = new DataTable();
                    dt.TableName = s.SheetName;
                    List<int> columns = new List<int>();
                    for (int i = 0; i < header.LastCellNum; i++)
                    {
                        object obj = GetIcellValue(header.GetCell(i) as ICell);
                        if (obj == null || obj.ToString() == string.Empty)
                        {
                            dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        }
                        else
                        {
                            dt.Columns.Add(new DataColumn(obj.ToString()));
                        }
                        columns.Add(i);
                    }
                    for (int i = firstDataRow; i <= s.LastRowNum; i++)
                    {
                        if (s.GetRow(i)==null)
                            continue;
                        DataRow dr = dt.NewRow();
                        bool hasValue = false;
                        foreach (int j in columns)
                        {
                            dr[j] = GetIcellValue(s.GetRow(i).GetCell(j) as ICell);
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
                    ds.Tables.Add(dt);
                }              
            }
            return ds;
        }

        public static DataTable ReadExcel(string filePath) {
            DataTable dt = new DataTable();
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                ISheet s = hssfworkbook.GetSheetAt(0);
                IRow header = s.GetRow(s.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++) {
                    object obj = GetValue(header.GetCell(i) as HSSFCell);
                    if (obj == null || obj.ToString() == string.Empty) {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        //continue;
                    } else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                for (int i = s.FirstRowNum + 1; i <= s.LastRowNum; i++) {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns) {
                        dr[j] = GetValue(s.GetRow(i).GetCell(j) as HSSFCell);
                        if (dr[j] != null && dr[j].ToString() != string.Empty) {
                            hasValue = true;
                        }
                    }
                    if (hasValue) {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 导入自营店铺时候用到 add by jerry.lu 2011.11.24
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable ReadExcel2(string filePath) {
            DataTable dt = new DataTable();
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                ISheet s = hssfworkbook.GetSheetAt(0);
                IRow header = s.GetRow(s.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++) {
                    object obj = GetValue(header.GetCell(i) as HSSFCell);
                    if (obj == null || obj.ToString() == string.Empty) {
                        obj = string.Empty;
                    }
                    columns.Add(i);
                    dt.Columns.Add(new DataColumn(obj.ToString()));
                }
                for (int i = s.FirstRowNum + 1; i <= s.LastRowNum; i++) {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns) {

                        try {
                            dr[j] = GetValue(s.GetRow(i).GetCell(j) as HSSFCell);
                        } catch { };
                        if (dr[j] != null && dr[j].ToString() != string.Empty) {
                            hasValue = true;
                        }
                    }
                    if (hasValue) {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }


        #region ToHTML
        public class ExcelColumn {
            public string Key { get; set; }
            public string Name { get; set; }
            public string Format { get; set; }
            public string Style { get; set; }  //可以设置列的对齐方式 manoen + 2016.03.16
        }

        public class ColumnList : List<ExcelColumn> {
            public ExcelColumn this[string key] {
                get {
                    foreach (var item in this) {
                        if (item.Key == key)
                            return item;
                    }
                    return null;
                }
            }

            public void Add(ColumnList list) {
                foreach (var item in list) {
                    this.Add(item);
                }
            }

            public void Add(string key, string name) {
                ExcelColumn col = new ExcelColumn();
                col.Key = key;
                col.Name = name;
                Add(col);
            }

            /// <summary>
            /// manoen
            /// 2016.03.16 11:19
            /// </summary>
            /// <param name="key"></param>
            /// <param name="name"></param>
            public void Add(string key, string name, string style) {
                ExcelColumn col = new ExcelColumn();
                col.Key = key;
                col.Name = name;
                col.Style = style;
                Add(col);
            }

            public bool ContainsKey(string key) {
                foreach (var item in this) {
                    if (item.Key == key) {
                        return true;
                    }
                }
                return false;
            }
        }

        public class ExcelContext {
            public string HeaderBackground { get; set; }
            public ColumnList Columns { get; set; }
            public DataTable Data { get; set; }
            public string FileName { get; set; }
            public string Title { get; set; }
            public string Condition { get; set; }
            public bool ShowTitle { get; set; }

            public ExcelContext() {
                FileName = string.Empty;
                Title = string.Empty;
                Condition = string.Empty;
                ShowTitle = true;
                Columns = new ColumnList();
            }

            public void AddColumns(ColumnList columns) {
                if (columns != null) {
                    foreach (var item in columns) {
                        Columns.Add((ExcelColumn)item);
                    }
                }
            }
        }

        public static string ToHtml(ExcelContext excelContext) {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<style>.xlsText{mso-number-format:""@"";}</style>")
                .Append(@"<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />")
                .Append(@"<table border=""1"" align=""center"" cellspacing=""1"" cellpadding=""1"" width=""100%"">")
            .Append(GetHtmlHeader(excelContext))
            .Append(GetHtmlBody(excelContext))
            .Append("</table>");
            return sb.ToString();
        }

        static StringBuilder GetHtmlHeader(ExcelContext excelContext) {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<thead>");
            if (!string.IsNullOrEmpty(excelContext.Title)) {
                sb.Append(@"<tr><th align=""center"" colspan=""")
                    .Append(excelContext.Columns.Count)
                    .Append(@"""><font size=""+1"" ");
                if (!string.IsNullOrEmpty(excelContext.HeaderBackground)) {
                    sb.Append(" color=\"").Append(excelContext.HeaderBackground).Append("\"");
                }
                sb.Append(">").Append(excelContext.Title)
                    .Append(@"</font></th></tr>");
            }
            if (!string.IsNullOrEmpty(excelContext.Condition)) {
                sb.Append(@"<tr><td colspan=""")
                    .Append(excelContext.Columns.Count)
                    .Append(@""" ><font size=""+1"">统计条件:</font>")
                    .Append(excelContext.Condition).Append(@"</td></tr>");
            };

            //标题列
            sb.Append(@"</thead>");
            sb.Append("<tr>");
            foreach (var item in excelContext.Columns) {
                //manoen + 2016.03.16 11:27
                if (string.IsNullOrEmpty(item.Style)) {
                    sb.Append(@"<th style=""text-align:center; background:#F3C458;"">");
                } else {
                    sb.AppendFormat(@"<th style=""text-align:{0}; background:#F3C458;"">", item.Style);
                }
                sb.Append(item.Name);
                sb.Append("</th>");
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
        static StringBuilder GetHtmlBody(ExcelContext excelContext) {
            StringBuilder sb = new StringBuilder();
            string alignStyle = "";
            object tpValue = "";
            foreach (DataRow dr in excelContext.Data.Rows)
            {
                sb.Append("<tr>");
                foreach (var column in excelContext.Columns)
                {

                    //manoen + 2016.03.16 11:27
                    //Modified by Andy 20160420
                    if (excelContext.Data.Columns.Contains(column.Key))
                    {
                        alignStyle = GetAlignStyle(excelContext.Data.Columns[column.Key]);
                        tpValue = dr[column.Key];
                    }
                    else
                    {
                        alignStyle = "left";
                        tpValue = "";
                    }
                    if (string.IsNullOrEmpty(column.Style))
                    {
                        //sb.Append(@"<td style=""text-align:center;"" width=""20%"" class=""xlsText"">");
                        sb.AppendFormat(@"<td style=""text-align:{0};"" width=""20%"" class=""xlsText"">", alignStyle);
                    }
                    else
                    {
                        sb.AppendFormat(@"<td style=""text-align:{0};"" width=""20%"" class=""xlsText"">", column.Style);
                    }

                    sb.Append(ValueFormat(tpValue, column.Format));
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            return sb;
        }

        //Added by Andy 20160420
        static string GetAlignStyle(DataColumn column)
        {
            //column.DataType.Equals(System.Type.GetType("System.String"))
            if (column.DataType.FullName == "System.String")
            {
                return "left";
            }
            else
            {
                return "center";
            }
            //else if (column.DataType.FullName == "System.Int32")
            //{
            //    return "center";
            //}
            //else if (column.DataType.FullName == "System.Boolean")
            //{
            //    return "center";
            //}
            //else if (column.DataType.FullName == "System.Decimal")
            //{
            //    return "center";
            //}
            //else if (column.DataType.FullName == "System.Double")
            //{
            //    return "center";
            //}
            //else if (column.DataType.FullName == "System.DateTime")
            //{
            //    return "center";
            //}                      
        }

        /// <summary>
        /// 格式化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        static object ValueFormat(object obj, string format) {
            if (obj == DBNull.Value) return string.Empty;
            if (obj is string) {
                if (!string.IsNullOrEmpty(format)) {
                    return string.Format(format, obj);
                } else {
                    return (string)obj;
                }
            } else if (obj is decimal) {
                if (!string.IsNullOrEmpty(format)) {
                    return ((decimal)obj).ToString(format);
                } else {
                    return (decimal)obj;
                }
            } else if (obj is int) {
                if (!string.IsNullOrEmpty(format)) {
                    return ((int)obj).ToString(format);
                } else {
                    return (int)obj;
                }
            } else if (obj is long) {
                if (!string.IsNullOrEmpty(format)) {
                    return ((long)obj).ToString(format);
                } else {
                    return (long)obj;
                }
            } else if (obj is float) {
                if (!string.IsNullOrEmpty(format)) {
                    return ((float)obj).ToString(format);
                } else {
                    return (float)obj;
                }
            } else if (obj is DateTime) {
                if (!string.IsNullOrEmpty(format)) {
                    return ((DateTime)obj).ToString(format);
                } else {
                    return (DateTime)obj;
                }
            } else { return obj.ToString(); }
        }
        #endregion

    }

    public class XMLToExcel {
        private string xmlfile = @"a.xml";

        /// <summary>
        /// 生成excel文件时的服务器目录
        /// </summary>
        private string path;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">生成excel文件时的服务器目录</param>
        public XMLToExcel(string path) {
            this.path = path;
        }

        private XmlDocument xmlDocLoad() {
            try {
                XmlDocument xmlDoc = new XmlDocument();
                string tempath = HttpContext.Current.Server.MapPath("~/XMLTemplate/XMLExcelTemplate.xml");
                xmlDoc.Load(tempath);
                return xmlDoc;
            } catch {
                throw new Exception("加载XML文件：" + xmlfile + "产生错误,请检查文件");
            }
        }

        private XmlDocument BCWDXmlDocLoad() {
            try {
                XmlDocument xmlDoc = new XmlDocument();
                string tempath = HttpContext.Current.Server.MapPath("~/XMLTemplate/XMLBCWDExcelTemplate.xml");
                xmlDoc.Load(tempath);
                return xmlDoc;
            } catch {
                throw new Exception("加载XML文件：" + xmlfile + "产生错误,请检查文件");
            }

        }

        private XmlDocument TableToXMLDoc(DataTable dt, int[] hiddenColumnsIndex) {
            try {
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
                if (hiddenColumnsIndex != null) {
                    for (int i = 0; i < hiddenColumnsIndex.Length; i++) {
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
                for (int j = 0; j < dt.Columns.Count; j++) {

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
                for (int i = 0; i < dt.Rows.Count; i++) {
                    xeRow = xmlDoc.CreateElement("", "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                    for (int j = 0; j < dt.Columns.Count; j++) {
                        xeCell = xmlDoc.CreateElement("", "Cell", "urn:schemas-microsoft-com:office:spreadsheet");
                        xeData = xmlDoc.CreateElement("", "Data", "urn:schemas-microsoft-com:office:spreadsheet");
                        xa = xmlDoc.CreateAttribute("ss", "Type", "urn:schemas-microsoft-com:office:spreadsheet");
                        long a = 0;
                        if (!dt.Rows[i][j].ToString().StartsWith("0")) {
                            if (Int64.TryParse(dt.Rows[i][j].ToString(), out a)) {
                                xa.Value = "Number";
                            } else {
                                xa.Value = "String";
                            }
                        } else {
                            if (dt.Rows[i][j].ToString().Length == 1) {
                                xa.Value = "Number";
                            } else {
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
            } catch (Exception ex) {
                throw ex;
            }



        }



        private XmlDocument TableToBCWDXMLDoc(DataTable dt, string year) {
            try {
                XmlDocument xmlDoc = BCWDXmlDocLoad();
                XmlNode xmlnode = xmlDoc.GetElementsByTagName("Table").Item(0);
                xmlnode.ChildNodes[18].ChildNodes[0].ChildNodes[0].InnerText = year;
                XmlElement xeRow;
                XmlElement xeCell;
                XmlElement xeData;
                XmlElement xeColum;
                XmlAttribute xa;
                xmlnode.Attributes["ss:ExpandedRowCount"].Value = ((int)(dt.Rows.Count + 2)).ToString();



                for (int i = 0; i < dt.Rows.Count; i++) {
                    xeRow = xmlDoc.CreateElement("", "Row", "urn:schemas-microsoft-com:office:spreadsheet");
                    for (int j = 0; j < dt.Columns.Count; j++) {
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
            } catch (Exception ex) {
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
        public string TableToBCWDExcel(DataTable dt, string fileName, string year) {
            XmlDocument xmlDoc = TableToBCWDXMLDoc(dt, year);
            string fullPath = path + "/" + fileName + ".xls";

            //Frank, I changed here to check if the directory exist and create it. Peter
            if (!Directory.Exists(path)) {
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
        public string TableToExcel(DataTable dt, int[] hiddenColumnsIndex, string fileName) {
            XmlDocument xmlDoc = TableToXMLDoc(dt, hiddenColumnsIndex);
            string fullPath = path + "/" + fileName + ".xls";

            //Frank, I changed here to check if the directory exist and create it. Peter
            if (!Directory.Exists(path)) {
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
        public DataTable ExcelToTable(string excelFile, int[] needColumnsIndex) {
            return ExcelToTable(excelFile, needColumnsIndex, false);
        }

        /// <summary>
        /// ExcelTo DateTable
        /// </summary>
        /// <param name="excelFile">excel文件的服务器地址</param>
        /// <returns></returns>
        public DataTable ExcelToTable(string excelFile) {
            DataTable dt = new DataTable();
            DataRow dr = null;
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(excelFile);
            } catch {
                throw new Exception("Excel 文件格式不对,可能存在一些特殊字符!");

            }
            XmlNodeList xnlist = xmlDoc.GetElementsByTagName("Row");

            for (int i = 0; i < xnlist.Count; i++) {

                if (i != 0) {
                    dr = dt.NewRow();

                }
                for (int j = 0; j < xnlist[i].ChildNodes.Count; j++) {
                    if (i == 0) {
                        string columnName = xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value;
                        if (!dt.Columns.Contains(columnName)) {
                            dt.Columns.Add(columnName);
                        } else {
                            dt.Columns.Add(columnName + j.ToString());
                        }
                    } else {
                        if (xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild != null) {
                            dr[j] = xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value;
                        } else {
                            dr[j] = "";
                        }
                    }
                }
                if (i != 0) {
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
        public DataTable BCWDExcelToTable(string excelFile, out string year) {
            DataTable dt = new DataTable();
            DataRow dr = null;
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(excelFile);
            } catch {
                throw new Exception("Excel 文件格式不对,可能存在一些特殊字符!");

            }
            XmlNode xmlnode = xmlDoc.GetElementsByTagName("Table").Item(0);
            year = xmlnode.ChildNodes[18].ChildNodes[0].ChildNodes[0].InnerText;
            XmlNodeList xnlist = xmlDoc.GetElementsByTagName("Row");

            for (int i = 0; i < xnlist.Count; i++) {

                if (i != 0) {
                    if (i != 1) {
                        dr = dt.NewRow();

                    }
                    for (int j = 0; j < xnlist[i].ChildNodes.Count; j++) {
                        if (i == 1) {
                            dt.Columns.Add(xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value);
                        } else {
                            if (xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild != null) {
                                dr[j] = xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value;
                            } else {
                                dr[j] = "0";
                            }
                        }
                    }
                    if (i != 1) {
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
        public DataTable ExcelToTable(string excelFile, int[] needColumnsIndex, bool? reversalColumnsIndex) {
            DataTable dt = new DataTable();
            DataRow dr = null;
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(excelFile);
            } catch {
                throw new Exception("Excel 文件格式不对,可能存在一些特殊字符!");

            }
            try {
                XmlNodeList xnlist = xmlDoc.GetElementsByTagName("Row");
                //颠倒列索引,因为从0开始，所以要减去1
                if (reversalColumnsIndex.HasValue && reversalColumnsIndex.Value) {
                    for (int i = 0; i < needColumnsIndex.Length; i++) {
                        needColumnsIndex[i] = xnlist[0].ChildNodes.Count - needColumnsIndex[i] - 1;
                    }
                }

                for (int i = 0; i < xnlist.Count; i++) {

                    if (i != 0) {
                        dr = dt.NewRow();

                    }
                    for (int j = 0; j < xnlist[i].ChildNodes.Count; j++) {
                        if (i == 0) {
                            if (needColumnsIndex != null) {
                                for (int k = 0; k < needColumnsIndex.Length; k++) {
                                    if (j == needColumnsIndex[k]) {
                                        dt.Columns.Add(xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value);
                                    }
                                }
                            } else {
                                dt.Columns.Add(xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value);
                            }
                        } else {
                            if (needColumnsIndex != null) {
                                for (int k = 0; k < needColumnsIndex.Length; k++) {
                                    if (j == needColumnsIndex[k]) {
                                        if (!xnlist[i].ChildNodes.Item(j).FirstChild.HasChildNodes) {
                                            dr[k] = "";
                                        } else {
                                            dr[k] = xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value;
                                        }
                                    }
                                }
                            } else {
                                if (xnlist[i].ChildNodes.Item(j).FirstChild.HasChildNodes) {
                                    dr[j] = "";
                                } else {
                                    dr[j] = xnlist[i].ChildNodes.Item(j).FirstChild.FirstChild.Value;
                                }
                            }
                        }
                    }
                    if (i != 0) {
                        dt.Rows.Add(dr);
                    }

                }
                return dt;
            } catch (Exception ex) {
                throw new Exception("Excel Format Error");
            }
        }

        /// <summary>
        /// 根据Dictionary来生成对应的Table结构
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static DataTable CreateDateTableScheme(IDictionary<string, string> dic) {
            DataTable dt = new DataTable();
            foreach (KeyValuePair<string, string> de in dic) {
                DataColumn dColumn = new DataColumn(de.Key);
                dColumn.Caption = de.Value;
                dt.Columns.Add(dColumn);
            }
            return dt;
        }




    }
}
