using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlShamil.Model.Dto
{
    public class UserDto:BaseDto
    {
        [Key]
        public int Id { get; set; }
        public string? Guid { get;  set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress {  get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }

        [ForeignKey(nameof(RoleDto))]
        public int RoleId {  get; set; }
        //public BaseDto? BaseData { get; set; }

        //Navigation Property
        public virtual RoleDto? Role { get; set; }
        public virtual ICollection<PasswordResetTokenDto>? PasswordResetToken { get; set; }
       
    }
}
