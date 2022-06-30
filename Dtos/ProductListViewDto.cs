using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Consumer.Dtos
{
    public class ProductListViewDto
    {
        public int productID { get; set; }

        public string productName { get; set; }

        public string CategoryName { get; set; }

        public string quantityPerUnit { get; set; }

        public decimal unitPrice { get; set; }

        public short unitsInStock { get; set; }
    }
}
