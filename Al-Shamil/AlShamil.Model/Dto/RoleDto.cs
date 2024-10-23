using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Dto
{
    public class RoleDto:BaseDto
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
       // public BaseDto? BaseData {  get; set; }

        public virtual ICollection<UserDto>? Users { get; set; }
        public virtual ICollection<RoleAccessDto>? RoleAccess { get; set; }

    }
}
