using DataLayer.Entities.User;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _context;

        public UserRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetUser(int id)
        {
            var userEntity = await _context
                .Users
                .Include(r => r.Role)
                .Include(s => s.State)
                .Where(u => u.UserId == id)
                .FirstOrDefaultAsync();

            return userEntity;
        }

        public async Task<bool> EditUser(UserEntity editUser)
        {
            _context.Entry(editUser).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(UserEntity userEntity)
        {
            _context.Users.Remove(userEntity);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
