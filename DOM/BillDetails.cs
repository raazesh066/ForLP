using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM
{
    public class BillDetails:BaseObjectModel
    {

        public BillDetails()
        {
            challan = new Challan();
        }


        public int ChallanId { get; set; }

        public string BillId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public decimal CategoryQuantity { get; set; }

        public decimal Quantity { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public decimal CurrentStock { get; set; }


        public Challan challan { get; set; }

    }
}
