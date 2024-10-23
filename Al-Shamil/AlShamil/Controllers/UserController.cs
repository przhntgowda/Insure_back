//using AlShamil.Business.Interface;
using AlShamil.BusinessEF.Interface;
using AlShamil.Model.Dto;
using AlShamil.Model.Request;
using AlShamil.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace AlShamil.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        // GET: UserController
        [HttpGet(Name = "GetUsersAsync")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetUsersAsync()
        {
            IEnumerable<UserDto>? usersDto = await _userBusiness.GetUsersAsync();
            IEnumerable<UserResponse> getUsers = usersDto.Select(x => new UserResponse()
            {
                UserId = x.Id,
                Guid = x.Guid,
                FirstName = x.FirstName,
                LastName = x.LastName,
                EmailAddress = x.EmailAddress,
                Password = x.Password,
                PhoneNumber = x.PhoneNumber,
                RoleId = x.RoleId,
                
            });
            return Ok(getUsers);
        }

        [HttpPost(Name ="CreateUserAsync")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateUserAsync(UserRequest userRequest)
        {
            if (ModelState.IsValid)
            {
                UserDto userDto = new()
                {
                    FirstName = userRequest.FirstName,
                    LastName = userRequest.LastName,
                    EmailAddress = userRequest.EmailAddress,
                    Password = userRequest.Password,
                    PhoneNumber = userRequest.PhoneNumber,
                    RoleId = userRequest.RoleId,
                    CreatedOn = DateTime.Now,
                    CreatedBy = User.FindFirstValue(JwtRegisteredClaimNames.Name)
                };
                bool result = await _userBusiness.CreateUserAsync(userDto);
                if(result)
                    return Ok("User Created Successfully");
            }
            return Ok("User Creation Failed");
            
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(UserRequest userRequest)
        {
            if (ModelState.IsValid)
            {
                UserDto userDto = new()
                {
                    Id=userRequest.UserId,
                    FirstName = userRequest.FirstName,
                    LastName = userRequest.LastName,
                    EmailAddress = userRequest.EmailAddress,
                    Password = userRequest.Password,
                    PhoneNumber = userRequest.PhoneNumber,
                    RoleId = userRequest.RoleId,
                    ModifiedOn = DateTime.Now,
                    ModifiedBy = User.FindFirstValue(JwtRegisteredClaimNames.Name)
                };
                bool result=await _userBusiness.UpdateUserAsync(userDto);
                if (result) 
                    return Ok("User Data Updated Successfully");
            }
            return Ok("Not Valid");
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            if (id != null)
            {
                bool isTrue = await _userBusiness.DeleteUserAsync(id);
                if (isTrue)
                    return Ok(true);
            }
            return Ok();
        }

    }
}
