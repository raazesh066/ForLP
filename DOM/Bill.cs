using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM
{
    public class Bill:BaseObjectModel
    {
        public Bill()
        {
            customer = new Customer();
            challan = new Challan();
        }

        public Customer customer { get; set; }

        public string BillId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public Challan challan { get; set; }
    }
}
