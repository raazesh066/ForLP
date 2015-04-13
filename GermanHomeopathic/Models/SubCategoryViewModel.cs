using DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GermanHomeopathic.Models
{
    public class SubCategoryViewModel:BaseViewModel
    {
        public SubCategoryViewModel()
        {
            lstSubCategories = new List<SubCategoryViewModel>();
        }
        public int SubCategoryId { get; set; }


          [DisplayName("Category Name")]
          [Required(ErrorMessage = "Category Name is Required")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public List<CategoryViewModel> lstCategory { get; set; }

          [DisplayName("Sub CategoryName")]
          [Required(ErrorMessage = "Sub Category Name is Required")]
        public string SubcategoryName { get; set; }

          [DisplayName("Packed Quantity")]
        public string  PackedQuantity { get; set; }

        public string Code { get; set; }

        public decimal? Rate { get; set; }

        public List<SubCategoryViewModel> lstSubCategories { get; set; }
    }
}
