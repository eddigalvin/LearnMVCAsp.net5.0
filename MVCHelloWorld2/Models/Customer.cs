using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Customer
    {
        [Required]
        [RegularExpression("^[A-Z]{3,3}[0-9]{4,4}")]
        [Key]
        public string CustomerCode { get; set; }
        [Required]
        [StringLength(10)]
        public string CustomerName { get; set; }

        public decimal CustomerAmount { get; set; }

        public string CustomerAmountColor { get; set; }
       
       // public double Amount { get; set; }
    }
}