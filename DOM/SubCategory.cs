using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM
{
    public class SubCategory:BaseObjectModel
    {
        public SubCategory()
        {
            category = new Category();
        }




        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public Category category { get; set; }

        public string PackedQuantity { get; set; }

        public string code { get; set; }

        public decimal Rate { get; set; }

        public decimal? Quantity { get; set; }

    }
}
