using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GermanHomeopathic.Controllers
{
    public class ReportsViewerController : Controller
    {
        //
        // GET: /ReportsViewer/

        public ActionResult Index(int ChallanId)
        {
            return Redirect("~/ReportChallan.aspx?challanId=" + ChallanId);
        }


        public ActionResult GenerateBill(int ChallanId, string BillId)
        {
            return Redirect("~/GenerateBill.aspx?ChallanId=" + ChallanId+"&BillId="+BillId);
        }


    }
}
