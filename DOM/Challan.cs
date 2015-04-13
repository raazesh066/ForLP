using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM
{
    public class Challan:BaseObjectModel
    {
        public int ChallanId { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public int TinNumber { get; set; } 

        public decimal AmountWithoutTax { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string Remarks { get; set; }

        #region type of tax

        public decimal ecess { get; set; }

        public decimal shecess { get; set; }

        public decimal cst { get; set; }

        public decimal freight { get; set; }

        public decimal vat { get; set; }

        public decimal tcs { get; set; }
        #endregion

    }
}
