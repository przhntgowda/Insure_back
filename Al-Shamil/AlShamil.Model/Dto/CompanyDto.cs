using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AlShamil.Model.Dto
{
    public class CompanyDto:BaseDto
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? EmailAddress { get; set; }

        [ForeignKey(nameof(CountryDto))]
        public int CountryId { get; set; }

        [ForeignKey(nameof(StateDto))]
        public int StateId { get; set; }
        public int Pincode { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Fax { get; set; }
        public string? Website { get; set; }
        public string? FormalName { get; set; }

        [ForeignKey(nameof(CurrencyDto))]
        public int CurrencyId { get; set; }

        //Navigation Property
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual CountryDto? Country { get; set; }

        //Navigation Property
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual StateDto? State { get; set; }

        //Navigation Property
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual CurrencyDto? Currency { get; set; }

        public virtual ICollection<AccountGroupDto>? AccountGroup { get; set; }
        public virtual ICollection<AccountLedgerDto>? AccountLedger { get; set; }
        public virtual ICollection<TransactionDto>? Transactions { get; set; }

    }
}
