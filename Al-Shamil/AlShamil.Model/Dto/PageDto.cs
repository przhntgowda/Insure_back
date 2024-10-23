using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Dto
{
    public class PageDto:BaseDto
    {
        [Key]
        public int Id { get; set; }
        public string? PageName { get; set; }
        public virtual ICollection<RoleAccessDto>? RoleAccess { get; set; }
        
    }
}
