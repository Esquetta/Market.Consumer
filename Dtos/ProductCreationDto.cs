using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Consumer.Dtos
{
    public class ProductCreationDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        
        public int categoryID { get; set; }
        [Required]
        [Display(Name = "Quantity Per Unit")]
        public string quantityPerUnit { get; set; }
        [Required]
        [Display(Name = "Unit Price")]
        public decimal unitPrice { get; set; }
        [Required]
        [Display(Name = "Units In Stock")]
        public short unitsInStock { get; set; }

    }
}
