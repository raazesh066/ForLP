using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GermanHomeopathic.Models
{
    public class TaxViewModel:BaseViewModel
    {
        public int TaxId { get; set; }

         [DisplayName("Tax Name")]
         [Required(ErrorMessage = "Tax Name is Required")]
        public string TaxName { get; set; }

         [Required(ErrorMessage = "Value is Required")]
        public decimal? Value { get; set; }


        public List<TaxViewModel> lst { get;set;}

    }
}