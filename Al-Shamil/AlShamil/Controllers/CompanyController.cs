//using AlShamil.Business.Interface;
using AlShamil.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlShamil.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CompanyController : ControllerBase
    {
        //private readonly ICompanyDataRetrieverBusiness<CompanyMasterDataDto> _companyDataRetrieverBusiness;
        //public CompanyController(ICompanyDataRetrieverBusiness<CompanyMasterDataDto> companyDataRetrieverBusiness)
        //{
        //    _companyDataRetrieverBusiness = companyDataRetrieverBusiness;
        //}

        [HttpGet(Name = "GetCompanyMasterData")]
        [Authorize]
        public async Task<IActionResult> GetCompanyMasterData()
        {
            return Ok();
        }
    }
}
