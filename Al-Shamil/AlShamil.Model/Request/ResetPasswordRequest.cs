using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Request
{
    public class ResetPasswordRequest
    {
        public string? Token { get; set; }
        public int UserId { get; set; }
        public string? Email {  get; set; }
        public string? Password { get; set; }
    }
}
