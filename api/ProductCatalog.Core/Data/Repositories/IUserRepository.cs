using ProductCatalog.Core.Data.Entities;

namespace ProductCatalog.Core.Data.Repositories
{
    public interface IUserRepository
    {
        Task<ICollection<UserEntity>> GetAsync();
        Task<UserEntity?> GetAsync(int userId, bool track = true);
    }
}
