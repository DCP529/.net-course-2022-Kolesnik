using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
    public class PassportNullException : Exception
    {
        public PassportNullException(string message) : base(message) { }
    }
}
