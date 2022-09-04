using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Exceptions
{
    public class PassportNullException : ArgumentNullException
    {
        public PassportNullException(string message) : base(message) { }
    }
}
