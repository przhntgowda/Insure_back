using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Dto
{
    public class CompanyMasterDataDto
    {
        public IEnumerable<CountryDto>? Country { get; set; }
        public List<StateDto>? State { get; set; }
        public IEnumerable<CurrencyDto>? Currency { get; set; }
    }
}
