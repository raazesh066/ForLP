using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GermanHomeopathic.Models
{
    public class CategoryViewModel : BaseViewModel
    {
        public CategoryViewModel()
        {
            lstCategory = new List<CategoryViewModel>();
        }

        
        public int CategoryId { get; set; }


        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name is Required")]
        public string CategoryName { get; set; }

       [Required(ErrorMessage = "Rate is Required")]
        public decimal? Rate { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Quantity { get; set; }

        public List<CategoryViewModel> lstCategory { get; set; }
    }
}