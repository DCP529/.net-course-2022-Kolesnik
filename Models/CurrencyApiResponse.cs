using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CurrencyApiResponse
    {
        public int Error { get; set; }
        public string ErrorMessage { get; set; }
        public decimal Amount { get; set; }
    }
}