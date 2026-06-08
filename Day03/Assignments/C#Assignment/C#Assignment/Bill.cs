using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Assignment
{
     public class Bill
    {
        public string PatientName { get; set; }
        public int Age { get; set; }

        public decimal TotalAmount = 0;
        public decimal DiscountAmount = 0;
        public decimal TaxAmount = 0;


        public const decimal CONSULTATION = 500;
        public const decimal BLOOD_TEST = 200;
        public const decimal XRAY = 1000;
        public const decimal ADMISSION = 2000;
    }
}
