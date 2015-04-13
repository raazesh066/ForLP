using DAL;
using DOM;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GermanHomeopathic
{
    public partial class GenerateBill : System.Web.UI.Page
    {

        ChallanDAL challanDAL = new ChallanDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ChallanId = Convert.ToInt32(Request.QueryString["ChallanId"]);
                string BillId = Request.QueryString["BillId"];

                //List<BillDetails> lstbill = new List<BillDetails>();

               // lstbill = challanDAL.ReadBillDetailsByChallanIdBillId(ChallanId,BillId);
                List<Challan> lstChallan = new List<Challan>();


                lstChallan = challanDAL.ReadChallanReportDetailsByChallanId(ChallanId);

                
                this.ReportViewer2.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
              
                this.ReportViewer2.LocalReport.ReportPath = Server.MapPath("Reports/BillReport.rdlc");

                ReportDataSource rpt = new ReportDataSource("DataSet1", lstChallan);
                //ReportDataSource rpt = new ReportDataSource("DataSet1", lstbill);
              
                this.ReportViewer2.LocalReport.DataSources.Clear();
                this.ReportViewer2.LocalReport.DataSources.Add(rpt);
               
                this.ReportViewer2.DataBind();
            }
        }



        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            int ChallanId = Convert.ToInt32(Request.QueryString["ChallanId"]);
            string BillId = Request.QueryString["BillId"];

            List<ChallanDetails> lstChallanDetails = new List<ChallanDetails>();

            lstChallanDetails = challanDAL.ReadBillSubReportDetailsByChallanIdBillId(ChallanId,BillId);

            e.DataSources.Add(new ReportDataSource("DataSet2", lstChallanDetails));
        }


    }
}