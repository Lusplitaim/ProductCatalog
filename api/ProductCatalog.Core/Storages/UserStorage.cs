using Microsoft.AspNetCore.Identity;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.User;

namespace ProductCatalog.Core.Storages
{
    internal class UserStorage : IUserStorage
    {
        private readonly UserManager<UserEntity> m_UserManager;
        private readonly SignInManager<UserEntity> m_SignInManager;
        public UserStorage(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            m_UserManager = userManager;
            m_SignInManager = signInManager;
        }

        public async Task<bool> CreateAsync(CreateUserDto model)
        {
            UserEntity user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var createResult = await m_UserManager.CreateAsync(user, model.Password);
            if (!createResult.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var userEntity = await m_UserManager.FindByEmailAsync(email);
            if (userEntity is null)
            {
                throw new Exception("User not found");
            }

            var result = UserDto.From(userEntity);

            return result;
        }
    }
}
