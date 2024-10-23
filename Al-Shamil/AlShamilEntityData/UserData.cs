using AlShamil.Model.Dto;
using AlShamilEntityData.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamilEntityData
{
    public class UserData:IUserData
    {
        private readonly AlShamilDbContext _db;
        public UserData(AlShamilDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            IEnumerable<UserDto> userDtos = await _db.User.ToListAsync();
            return userDtos;
        }
        public async Task<bool>CreateUserAsync(UserDto userDto)
        {
             _db.Add(userDto);
            int r = await _db.SaveChangesAsync();
            return r == 1;
        }
        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            UserDto? user=await _db.User.SingleOrDefaultAsync(x => x.Id==userDto.Id);
            user.FirstName=userDto.FirstName;
            user.LastName=userDto.LastName;
            user.EmailAddress=userDto.EmailAddress;
            user.PhoneNumber=userDto.PhoneNumber;
            user.RoleId=userDto.RoleId;
            user.ModifiedBy=userDto.ModifiedBy;
            user.ModifiedOn=userDto.ModifiedOn;
            _db.Update(user);
            int r=await _db.SaveChangesAsync();
            return r == 1;
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            UserDto? user=await _db.User.SingleOrDefaultAsync(x => x.Id == id);
            _db.Remove(user);
            int r= await _db.SaveChangesAsync();
            return r == 1;
        }
    }
}
