﻿using System;

namespace Services.Filters
{
    public class ClientFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int Phone { get; set; }
        public int Passport { get; set; }

        public Tuple<DateTime, DateTime> BirthDayRange { get; set; }
    }
}