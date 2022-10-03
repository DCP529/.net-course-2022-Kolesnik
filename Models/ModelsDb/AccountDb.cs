using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ModelsDb
{
    [Table(name: "account")]
    public class AccountDb
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("currency_name")]
        public string CurrencyName { get; set; }

        [Column("amount")]
        public int Amount { get; set; }

        [ForeignKey("client_id")]
        [Column("client_id")]
        public Guid ClientId { get; set; }


        public ClientDb Client { get; set; }
    }
}
