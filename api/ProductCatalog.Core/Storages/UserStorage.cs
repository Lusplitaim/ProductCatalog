using Microsoft.AspNetCore.Identity;
using ProductCatalog.Core.Data;
using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.DTOs.User;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Models.Enums;
using ProductCatalog.Core.Storages.Managers;

namespace ProductCatalog.Core.Storages
{
    internal class UserStorage : IUserStorage
    {
        private readonly UserManager<UserEntity> m_UserManager;
        private readonly SignInManager<UserEntity> m_SignInManager;
        private readonly IUnitOfWork m_UnitOfWork;
        public UserStorage(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IUnitOfWork uow)
        {
            m_UserManager = userManager;
            m_SignInManager = signInManager;
            m_UnitOfWork = uow;
        }

        public async Task<ExecResult<UserDto>> CreateAsync(CreateUserDto model)
        {
            var result = new ExecResult<UserDto>();

            UserEntity user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var createResult = await m_UserManager.CreateAsync(user, model.Password);
            if (!createResult.Succeeded)
            {
                result.AddErrors(createResult);
                return result;
            }

            await m_UnitOfWork.SaveAsync();

            var userRoleManager = new UserRoleManager();
            var userRoles = model.UserRoles.Count() > 0 ? model.UserRoles : [UserRole.User];
            await userRoleManager.AddRolesAsync(m_UnitOfWork, user.Id, userRoles);
            await m_UnitOfWork.SaveAsync();

            result.Result = UserDto.From(user);

            return result;
        }

        public async Task<ExecResult<UserDto>> UpdateAsync(int userId, UpdateUserDto model)
        {
            var result = new ExecResult<UserDto>();

            var userForUpdate = await m_UnitOfWork.UserRepository.GetAsync(userId);
            if (userForUpdate is null)
            {
                throw new NotFoundCoreException();
            }

            userForUpdate.UserName = model.UserName;

            if (model.UpdatedPassword is not null)
            {
                var pwdResetToken = await m_UserManager.GeneratePasswordResetTokenAsync(userForUpdate);
                var pwdUpdResult = await m_UserManager.ResetPasswordAsync(userForUpdate, pwdResetToken, model.UpdatedPassword);
                if (!pwdUpdResult.Succeeded)
                {
                    result.AddErrors(pwdUpdResult);
                    return result;
                }
            }

            var userRoleManager = new UserRoleManager();
            if (model.AddedRoles.Count() > 0)
            {
                await userRoleManager.AddRolesAsync(m_UnitOfWork, userId, model.AddedRoles);
            }
            if (model.RemovedRoles.Count() > 0)
            {
                await userRoleManager.RemoveRolesAsync(m_UnitOfWork, userId, model.RemovedRoles);
            }

            var updateResult = await m_UserManager.UpdateAsync(userForUpdate);
            if (!updateResult.Succeeded)
            {
                result.AddErrors(updateResult);
                return result;
            }

            await m_UnitOfWork.SaveAsync();

            result.Result = UserDto.From(userForUpdate);

            return result;
        }

        public async Task<ICollection<UserDto>> GetAsync()
        {
            var entities = await m_UnitOfWork.UserRepository.GetAsync();

            var result = entities.Select(UserDto.From).ToList();

            return result;
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

        public async Task<UserDto> GetAsync(int userId, bool track = true)
        {
            var userEntity = await m_UnitOfWork.UserRepository.GetAsync(userId, track);
            if (userEntity is null)
            {
                throw new Exception("User not found");
            }

            var result = UserDto.From(userEntity);

            return result;
        }

        public async Task<ExecResult> DeleteAsync(int userId)
        {
            var result = new ExecResult();

            var user = await m_UnitOfWork.UserRepository.GetAsync(userId);
            if (user is null)
            {
                throw new NotFoundCoreException();
            }

            var deleteResult = await m_UserManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                result.AddErrors(deleteResult);
                return result;
            }

            await m_UnitOfWork.SaveAsync();

            return result;
        }
    }
}
