using AlShamil.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.BusinessEF.Interface
{
    public interface IRoleBusiness
    {
        Task<bool> CreateRoleAsync(RoleDto roleDto);
    }
}
