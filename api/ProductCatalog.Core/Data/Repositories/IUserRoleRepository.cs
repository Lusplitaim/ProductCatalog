using Microsoft.AspNetCore.Identity;
using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.Data.Repositories
{
    public interface IUserRoleRepository
    {
        Task<ICollection<UserRoleEntity>> GetAsync();
        Task<ICollection<IdentityUserRole<int>>> GetByUserIdAsync(int userId);
        Task AddAsync(IEnumerable<IdentityUserRole<int>> userRoles);
        void Delete(IEnumerable<IdentityUserRole<int>> userRoles);
    }
}
