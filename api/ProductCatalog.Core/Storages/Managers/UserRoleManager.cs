using Microsoft.AspNetCore.Identity;
using ProductCatalog.Core.Data;
using ProductCatalog.Core.Models.Enums;

namespace ProductCatalog.Core.Storages.Managers
{
    public class UserRoleManager
    {
        public async Task AddRolesAsync(IUnitOfWork unitOfWork, int userId, ICollection<UserRoleType> addedRoles)
        {
            List<IdentityUserRole<int>> userRoleEntities = [];
            foreach (var userRole in addedRoles)
            {
                userRoleEntities.Add(new() { UserId = userId, RoleId = (int)userRole });
            }
            await unitOfWork.UserRoleRepository.AddAsync(userRoleEntities);
        }

        public async Task RemoveRolesAsync(IUnitOfWork unitOfWork, int userId, ICollection<UserRoleType> removedRoles)
        {
            var userRoleEntities = await unitOfWork.UserRoleRepository.GetByUserIdAsync(userId);

            var entitiesForDelete = userRoleEntities.Where(ur => removedRoles.Contains((UserRoleType)ur.RoleId));

            unitOfWork.UserRoleRepository.Delete(entitiesForDelete);
        }
    }
}
