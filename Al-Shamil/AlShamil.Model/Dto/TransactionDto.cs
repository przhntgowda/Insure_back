using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlShamil.Model.Dto
{
    public class TransactionDto:BaseDto
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date {  get; set; }

        [ForeignKey(nameof(AccountLedgerDto))]
        public int LedgerId { get; set; }
        
        public string? DebitAmount { get; set; }
        public string? CreditAmount { get; set; }
        public string? Narration {  get; set; }

        [ForeignKey(nameof(CompanyDto))]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(PaymentTypeDto))]
        public int PaymentTypeId { get; set; }

        public virtual CompanyDto? Company { get; set; }
        public virtual PaymentTypeDto? PaymentType { get; set; }
        
    }
}
