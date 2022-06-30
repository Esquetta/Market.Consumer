using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Consumer.Models
{
    public class Product
    {
        public int productID { get; set; }

        public string productName { get; set; }

        public int categoryID { get; set; }
        public Category Category { get; set; }

        public string quantityPerUnit { get; set; }

        public decimal unitPrice { get; set; }

        public short unitsInStock { get; set; }

    }
}
