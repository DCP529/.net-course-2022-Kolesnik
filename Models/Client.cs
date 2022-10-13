﻿using Models.ModelsDb;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Client : Person
    {
        public List<Account> Accounts { get; set; }

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

            var result = (Client)obj;

            return FirstName == result.FirstName
                && LastName == result.LastName
                && Patronymic == result.Patronymic
                && Passport == result.Passport
                && Phone == result.Phone
                && BirthDate == result.BirthDate;
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() 
                + LastName.GetHashCode() 
                + Passport.GetHashCode() 
                + Patronymic.GetHashCode() 
                + Phone.GetHashCode()
                + BirthDate.GetHashCode();
        }
    }
}
