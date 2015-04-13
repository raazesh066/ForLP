using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOM;
using System.ComponentModel;

namespace GermanHomeopathic.Models
{
    public class BillDetailViewModel
    {
        public BillDetailViewModel()
        {
            lstSubCategory = new List<SubCategory>();
        }


        public int ChallanId { get; set; }

        public string BillId { get; set; }

        [DisplayName("Category Name")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        [DisplayName("Sub Category Name")]
        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public decimal CategoryQuantity { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public decimal Quantity { get; set; }

        public decimal CurrentStock { get; set; }

        public List<BillDetailViewModel> lstDetails { get; set; }

        public List<SubCategory>  lstSubCategory {get;set;}
    }
}