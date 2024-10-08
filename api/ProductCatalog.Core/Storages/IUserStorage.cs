﻿using ProductCatalog.Core.DTOs.User;
using ProductCatalog.Core.Models;

namespace ProductCatalog.Core.Storages
{
    public interface IUserStorage
    {
        Task<ExecResult<UserDto>> CreateAsync(CreateUserDto model);
        Task<ExecResult<UserDto>> UpdateAsync(int userId, UpdateUserDto model);
        Task<ExecResult> DeleteAsync(int userId);
        Task<ICollection<UserDto>> GetAsync();
        Task<UserDto> GetAsync(string email);
        Task<UserDto> GetAsync(int userId, bool track = true);
    }
}
