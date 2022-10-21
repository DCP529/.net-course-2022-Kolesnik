﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CurrencyApiRequest
    {
        public string ApiKey { get; set; }

        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }

        public decimal Amount { get; set; }
    }
}