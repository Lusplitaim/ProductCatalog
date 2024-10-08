﻿using ProductCatalog.Core.Models.Enums;

namespace ProductCatalog.Core.DTOs.User
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string? UpdatedPassword { get; set; }
        public ICollection<UserRoleType> AddedRoles { get; set; } = [];
        public ICollection<UserRoleType> RemovedRoles { get; set; } = [];
    }
}
