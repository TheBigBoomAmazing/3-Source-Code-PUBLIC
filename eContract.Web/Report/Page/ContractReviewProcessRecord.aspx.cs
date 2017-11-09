using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eContract.Web.Report.Page
{
    public partial class ContractReviewProcessRecord : System.Web.UI.Page
    {
        public static StiReport StiReport;
        public static StiExportFormat StiFormat;
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = Request.QueryString["id"]; //"8b4e0e61-34e9-493d-8456-68266e0686bd";//
            if (!string.IsNullOrEmpty(ID))
            {
                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["ReportConnection"].ToString();//"Integrated Security=True;Initial Catalog=eContract_test;Data Source=.";

                string strRptPath = Server.MapPath("~/Report/ReportMRT/ContractReviewProcessReport.mrt");
                StiReport = new Stimulsoft.Report.StiReport();
                StiReport.Load(strRptPath);
                StiReport.Dictionary.Databases.Clear();
                StiReport.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("report", "report", connString, false));

                StiReport["CONTRACT_ID"] = ID;

                //string TempPath = Server.MapPath("~/TempFile/PrintReport/WORD/");
                //string TempFile = TempPath + "OrderDelivery" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                //System.IO.FileInfo file = new System.IO.FileInfo(TempFile);
                //if (!file.Directory.Exists)
                //{
                //    file.Directory.Create();
                //}
                //StiReport.Compile();
                StiReport.Render();
                //reportView.Report = StiReport;
                //StiReport.ExportDocument(StiExportFormat.Word2007, TempFile);
                //this.StiWebViewer1.Report = StiReport;
                //Expfile(TempFile);

                this.StiWebViewer1.Report = StiReport;
            }
        }
    }
}