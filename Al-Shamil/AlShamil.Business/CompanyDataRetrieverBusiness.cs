using AlShamil.Business.Interface;
using AlShamil.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Business
{
    public class CompanyDataRetrieverBusiness<T>:ICompanyDataRetrieverBusiness<T> where T : class
    {
        private readonly ICompanyDataRetrieverData<T> _companyDataRetrieverData;
        public CompanyDataRetrieverBusiness(ICompanyDataRetrieverData<T> companyDataRetrieverData)
        {
            _companyDataRetrieverData = companyDataRetrieverData;
        }
        public async Task<T> GetCompanyMasterData()
        {
            return await _companyDataRetrieverData.GetCompanyMasterData();
        }
    }
}
