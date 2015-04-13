using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOM
{
    public class Category:BaseObjectModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public decimal Rate { get; set; }

        public decimal? Discount { get; set; }

    }
}
