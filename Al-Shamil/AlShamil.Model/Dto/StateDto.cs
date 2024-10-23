using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Dto
{
    public class StateDto:BaseDto
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        [ForeignKey(nameof(CountryDto))]
        public int CountryId { get; set; }

        //Navigation Property
        public virtual CountryDto? Country { get; set; }

        public virtual ICollection<CompanyDto>? Company { get; set; }
    }
}
