using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM
{
    public class Stock:BaseObjectModel
    {
        public Stock()
        {
            subcategory = new SubCategory();
        }


        public int StockId { get; set; }

        public SubCategory subcategory { get; set; }

        public bool IsReceived { get; set; }

        public int CurrentStock { get; set; }

        public decimal? Quantity { get; set; }

    }
}
