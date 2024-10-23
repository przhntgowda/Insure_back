using AlShamil.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamilEntityData.Interface
{
    public interface IRoleData
    {
        Task<bool> CreateRoleAsync(RoleDto roleDto);
    }
}
