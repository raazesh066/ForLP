using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM
{
    public class ChallanDetails:BaseObjectModel
    {
        public int ChallanDetailId { get; set; }

        public int ChallanId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public decimal Rate { get; set; }

        public decimal? Discount { get; set; }

        public decimal Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        public int? SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }
    }
}
