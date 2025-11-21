using Anketa.Domain.Entities;

namespace Anketa.DataAccess.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByCompanyIdAsync(int companyId);
        Task<User> AuthenticateAsync(int companyId, string password);
        Task<string> GetUserDepartmentAsync(int userId);

        // User management helpers
        Task<List<User>> GetAllWithRolesAsync();
        Task<User?> GetByIdWithRoleAsync(int id);
    }
}
