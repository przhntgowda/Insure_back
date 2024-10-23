using AlShamil.Model.Dto;
using AlShamilEntityData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamilEntityData
{
    public class RoleData:IRoleData
    {
        private readonly AlShamilDbContext _db;
        public RoleData(AlShamilDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateRoleAsync(RoleDto roleDto)
        {
            await _db.AddAsync(roleDto);
            int r = await _db.SaveChangesAsync();
            return r == 1;

        }
    }
}
