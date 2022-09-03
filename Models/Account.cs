using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Account
    {
        public Currency Currency { get; set; }
        public int Amount { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (!(obj is Client))
            {
                return false;
            }

            var result = (Account)obj;

            return Currency.Equals(result.Currency)
                && Amount == result.Amount;

        }

        public override int GetHashCode()
        {
            return Currency.GetHashCode()
                + Amount.GetHashCode();
        }
    }
}
