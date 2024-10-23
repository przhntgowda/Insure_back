using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Dto
{
    public class CountryDto:BaseDto
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<StateDto>? States { get; set; }
        public virtual ICollection<CompanyDto>? Company { get; set; }
    }
}
