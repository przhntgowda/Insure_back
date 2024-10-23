using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Dto
{
    public class RoleAccessDto:BaseDto
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(RoleDto))]
        public int RoleId { get; set; }

        [ForeignKey(nameof(PageDto))]
        public int PageId { get; set; }

        public virtual RoleDto? Role { get; set; }
        public virtual PageDto? Page { get; set; }
        
    }
}
