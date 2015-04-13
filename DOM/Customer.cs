using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM
{
    public class Customer:BaseObjectModel
    {
        public Customer()
        {
            tax = new Tax();
        }



        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public int CustomerPhone { get; set; }

        public int TinNumber { get; set; }

        public Tax tax { get; set; }

    }
}
