using DataLayer.Entities.User;
using DataLayer.Interfaces;
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
            return await _context.Users.FindAsync(id);
        }
    }
}
