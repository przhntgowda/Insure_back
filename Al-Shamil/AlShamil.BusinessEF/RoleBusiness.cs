using AlShamil.BusinessEF.Interface;
using AlShamil.Model.Dto;
using AlShamilEntityData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.BusinessEF
{
    public class RoleBusiness:IRoleBusiness
    {
        private readonly IRoleData _roleData;
        public RoleBusiness(IRoleData roleData)
        {
            _roleData = roleData;
        }
        public async Task<bool> CreateRoleAsync(RoleDto roleDto)
        {
            return await _roleData.CreateRoleAsync(roleDto);
        }
    }
}
