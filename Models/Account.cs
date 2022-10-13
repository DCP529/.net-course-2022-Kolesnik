using Models.ModelsDb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading;

namespace Models
{
    public class Account
    {        
        public Guid Id { get; set; }       
        public string CurrencyName { get; set; }        
        public int Amount { get; set; }       
        public Guid ClientId { get; set; }
        public ClientDb Client { get; set; }
    }
}
