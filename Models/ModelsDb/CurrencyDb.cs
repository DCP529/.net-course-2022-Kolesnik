using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsDb
{
    [Table(name: "currency")]
    public class CurrencyDb
    {
        [Key]
        [Column(name: "id")]
        public Guid Id { get; set; }

        [Column(name: "name")]
        public string Name { get; set; }

        [Column(name: "code")]
        public int Code { get; set; }        

        [Column(name: "account_id")]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public AccountDb AccountDb { get; set; }
    }
}
