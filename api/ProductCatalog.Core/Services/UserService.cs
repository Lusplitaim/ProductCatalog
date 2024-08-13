using ProductCatalog.Core.Data;
using ProductCatalog.Core.DTOs.User;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Managers;
using ProductCatalog.Core.Models;
using ProductCatalog.Core.Storages;

namespace ProductCatalog.Core.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserStorage m_UserStorage;
        private readonly IUnitOfWork m_UnitOfWork;
        private readonly ILoggerManager m_Logger;
        public UserService(IUserStorage userStorage, IUnitOfWork uow, ILoggerManager logger)
        {
            m_UserStorage = userStorage;
            m_UnitOfWork = uow;
            m_Logger = logger;
        }

        public async Task<ExecResult<UserDto>> CreateAsync(CreateUserDto model)
        {
            try
            {
                using var transaction = m_UnitOfWork.BeginTransaction();

                var result = await m_UserStorage.CreateAsync(model);
                if (!result.Succeeded)
                {
                    return result;
                }

                transaction.Commit();

                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to create user", ex);
            }
        }

        public async Task<ExecResult<UserDto>> UpdateAsync(int userId, UpdateUserDto model)
        {
            try
            {
                using var transaction = m_UnitOfWork.BeginTransaction();

                var result = await m_UserStorage.UpdateAsync(userId, model);
                if (!result.Succeeded)
                {
                    return result;
                }

                transaction.Commit();

                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to update user", ex);
            }
        }

        public async Task<IEnumerable<UserDto>> GetAsync()
        {
            try
            {
                var result = await m_UserStorage.GetAsync();
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to get users", ex);
            }
        }

        public async Task<UserDto> GetAsync(int id)
        {
            try
            {
                var result = await m_UserStorage.GetAsync(id, track: false);
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to get users", ex);
            }
        }

        public async Task<ExecResult> DeleteAsync(int userId)
        {
            try
            {
                var result = await m_UserStorage.DeleteAsync(userId);
                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                m_Logger.LogError(ex, ex.Message);
                throw new Exception("Failed to delete user", ex);
            }
        }
    }
}
