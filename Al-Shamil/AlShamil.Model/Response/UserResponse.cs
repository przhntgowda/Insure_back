using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Response
{
    public class UserResponse
    {
        
        public int UserId { get; set; }
        public string? Guid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}
