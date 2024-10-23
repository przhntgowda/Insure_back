using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Dto
{
    public class AccountGroupDto:BaseDto
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        [ForeignKey(nameof(CompanyDto))]
        public int CompanyId { get; set; }

        //Navigation Property
        public virtual CompanyDto? Company { get; set; }
        public virtual ICollection<AccountLedgerDto>? AccountLedger { get; set; }
        
    }
}
