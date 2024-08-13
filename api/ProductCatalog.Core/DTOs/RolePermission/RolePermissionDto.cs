using ProductCatalog.Core.Data.Entities;
using ProductCatalog.Core.Models.Enums;

namespace ProductCatalog.Core.DTOs.RolePermission
{
    public class RolePermissionDto
    {
        public Area Area { get; set; }
        public AreaAction Action { get; set; }

        public static RolePermissionDto From(RolePermissionEntity entity)
        {
            return new()
            {
                Action = entity.Action,
                Area = entity.Area,
            };
        }
    }
}
