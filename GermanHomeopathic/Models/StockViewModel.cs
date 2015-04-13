using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DOM;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GermanHomeopathic.Models
{
    public class StockViewModel
    {

        public StockViewModel()
        {
            lstCategory = new List<CategoryViewModel>();
            lstSubCategory = new List<SubCategoryViewModel>();
            lstStock = new List<StockViewModel>();
        }

         public int StockId { get; set; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name is Required")]
         public int? CategoryId { get; set; }
         public string CategoryName { get; set; }
         public List<CategoryViewModel> lstCategory { get; set; }

         [DisplayName("SubCategory Name")]
         [Required(ErrorMessage = "SubCategory Name is Required")]
         public int? SubCategoryId { get; set; }
         public string SubCategoryName { get; set; }
         public List<SubCategoryViewModel> lstSubCategory { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
         public decimal? Quantity { get; set; }

         public bool isReceived { get; set; }

         public List<StockViewModel> lstStock { get; set; }

    }
}