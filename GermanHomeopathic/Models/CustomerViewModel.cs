using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOM;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GermanHomeopathic.Models
{
    public class CustomerViewModel:BaseViewModel
    {

        public int CustomerId { get; set; }

         [DisplayName("Customer Name")]
         [Required(ErrorMessage = "Customer Name is Required")]
        public string CustomerName { get; set; }

         [DisplayName("Customer Address")]
         [Required(ErrorMessage = "Customer Address is Required")]
        public string CustomerAddress { get; set; }

         [DisplayName("Customer Phone")]
         [Required(ErrorMessage = "Customer Phone Number is Required")]
        public int? CustomerPhone { get; set; }

         [DisplayName("Tin Number")]
         [Required(ErrorMessage = "Tin Number is Required")]
        public int? TinNumber { get; set; }


        public List<TaxViewModel> lstTax { get; set; }

         [DisplayName("Tax Type")]
         [Required(ErrorMessage = "Tax Name is Required")]
        public int TaxId { get; set; }

         public string TaxName { get; set; }

         public decimal TaxValue { get; set; }


         public List<CustomerViewModel> Customers { get; set; }


    }
}