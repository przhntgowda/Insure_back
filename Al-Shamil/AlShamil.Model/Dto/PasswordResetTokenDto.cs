using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Dto
{
    public class PasswordResetTokenDto
    {
        [Key]
        public int Id { get; set; }
        public string? Token { get; set; }
        public DateTime ExipryDate { get; set; }

        [ForeignKey(nameof(UserDto))]
        public int UserId { get; set; }
        public UserDto? User { get; set; }
    }
}
