using DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GermanHomeopathic.Models
{
    public class ChallanViewModel
    {
        public ChallanViewModel()
        {
            Customer = new CustomerViewModel();
            lstChallanDetails = new List<ChallanDetails>();
        }
        public int BillId { get; set; }

      [Required(ErrorMessage = "Customer Name is Required")]
        public int CustomerId { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }


        public CustomerViewModel Customer { get; set; }

        public decimal Quantity { get; set; }

        public decimal AmountWithoutTax { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string Remarks { get; set; }

        public List<ChallanDetails> lstChallanDetails { get; set; }

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
