using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Response
{
    public class ForgotPasswordResponse
    {
        public int UserId { get; set; }
        public string? Token { get; set; }
        public string? Email { get; set; }
    }
}
