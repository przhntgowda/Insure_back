using AlShamil.BusinessEF.Interface;
using AlShamil.Model.Dto;
using AlShamil.Model.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlShamil.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleBusiness _roleBusiness;
        public RoleController(IRoleBusiness roleBusiness)
        {
            _roleBusiness = roleBusiness;
        }
        [HttpPost("CreateRoleAsync")]
        [Authorize]
        public async Task<IActionResult> CreateRoleAsync(RoleRequest roleRequest)
        {
            if(ModelState.IsValid && roleRequest.RoleName!=null)
            {
                RoleDto roleDto = new()
                {
                    Name=roleRequest.RoleName,
                };
                bool result=await _roleBusiness.CreateRoleAsync(roleDto);
                if (result)
                    return Ok("Role Added Successfully");
            }
            return Ok("Model is not valid");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRoleAsync(RoleRequest roleRequest)
        {
            if (ModelState.IsValid && roleRequest.RoleName !=null)
            {
                RoleDto roleDto = new()
                {
                    Id=roleRequest.RoleId,
                    Name=roleRequest.RoleName,
                };
            }
            return Ok();
        }
    }
}
