using Anketa.DataAccess.DataContext;
using Anketa.DataAccess.Interfaces;
using Anketa.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Anketa.DataAccess.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User> AuthenticateAsync(int companyId, string password)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.CompanyId == companyId && u.Password == password && u.IsActive);
        }

        public async Task<User> GetByCompanyIdAsync(int companyId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.CompanyId == companyId);
        }

        public async Task<string> GetUserDepartmentAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Department)
                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllWithRolesAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .OrderBy(u => u.CompanyId)
                .ToListAsync();
        }

        public async Task<User?> GetByIdWithRoleAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
