using DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GermanHomeopathic.Models
{
    public class BillViewModel
    {
        public BillViewModel()
        {
            lstCustomer = new List<CustomerViewModel>();
            lstBill = new List<BillViewModel>();
        }


        [DisplayName("Customer Name")]
        [Required(ErrorMessage = "Customer Name is Required")]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

         [DisplayName("Bill Id")]
        public string BillId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int ChallanId { get; set; }

        public DateTime BillDate { get; set; }

        public decimal AmountWithoutTax { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }
      

        public List<BillViewModel> lstBill { get; set; }

      

        public List<CustomerViewModel> lstCustomer { get; set; }

    }
}