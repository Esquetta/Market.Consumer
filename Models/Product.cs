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
        [Required]
        public string productName { get; set; }
        [Required]

        public int categoryID { get; set; }
        public Category category { get; set; }
        [Required]

        public string quantityPerUnit { get; set; }
        [Required]

        public decimal unitPrice { get; set; }
        [Required]

        public short unitsInStock { get; set; }

    }
}
