using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Microsoft.Reporting.WebForms;
using DOM;

namespace GermanHomeopathic
{
    public partial class ReportChallan : System.Web.UI.Page
    {
        ChallanDAL challanDAL = new ChallanDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ChallanId =Convert.ToInt32(Request.QueryString["challanId"]);

               // ShowReport(ChallanId);
                List<Challan> lstChallan = new List<Challan>();

                


                lstChallan = challanDAL.ReadChallanReportDetailsByChallanId(ChallanId);

                //lstChallanDetails = challanDAL.ReadChallanSubReportDetailsByChallanId(ChallanId);
                this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                //this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reports/ASABillDetails.rdlc");
                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reports/ChallanReport.rdlc");
                ReportDataSource rpt = new ReportDataSource("DataSet1", lstChallan);
                //ReportDataSource rpt1 = new ReportDataSource("DataSet2", lstChallanDetails);
                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(rpt);
                //this.ReportViewer1.LocalReport.DataSources.Add(rpt1);
                
                //this.ReportViewer1.LocalReport.DataSources.Add(rpt1);
                this.ReportViewer1.DataBind();
            }
            
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            int ChallanId = Convert.ToInt32(Request.QueryString["challanId"]);

            List<ChallanDetails> lstChallanDetails = new List<ChallanDetails>();

            lstChallanDetails = challanDAL.ReadChallanSubReportDetailsByChallanId(ChallanId);
            e.DataSources.Add(new ReportDataSource("DataSet2", lstChallanDetails));
        }

        //private void ShowReport(int ChallanId)
        //{
        //    List<Challan> lstChallan = new List<Challan>();

        //    List<ChallanDetails> lstChallanDetails = new List<ChallanDetails>();


        //    lstChallan = challanDAL.ReadChallanReportDetailsByChallanId(ChallanId);

        //    lstChallanDetails = challanDAL.ReadChallanSubReportDetailsByChallanId(ChallanId);

        //    //this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reports/ASABillDetails.rdlc");
        //    this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reports/ChallanReport.rdlc");
        //    ReportDataSource rpt = new ReportDataSource("DataSet1", lstChallan);
        //    //ReportDataSource rpt1 = new ReportDataSource("DataSet2", lstChallanDetails);
        //    this.ReportViewer1.LocalReport.DataSources.Clear();
        //    this.ReportViewer1.LocalReport.DataSources.Add(rpt);
        //    //this.ReportViewer1.LocalReport.DataSources.Add(rpt1);
        //    this.ReportViewer1.DataBind();
        //}
    }
}