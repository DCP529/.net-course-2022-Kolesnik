using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsDb
{
    [Table(name: "client")]
    public class ClientDb
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("patronymic")]
        public string Patronymic { get; set; }

        [Column("passport")]
        public int Passport { get; set; }

        [Column("phone")]
        public int Phone { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("bonus")]
        public int Bonus { get; set; }
        
        public ICollection<AccountDb> Accounts { get; set; }
    }
}
