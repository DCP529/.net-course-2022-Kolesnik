﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public struct Currency
    {
        public string Name { get; set; }
        public int Code { get; set; }

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

            var result = (Currency)obj;

            return Name == result.Name
                && Code == result.Code;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode()
                + Code.GetHashCode();
        }
    }
}
