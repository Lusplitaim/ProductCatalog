using ProductCatalog.Core.Models.Enums;

namespace ProductCatalog.Core.Data.Entities
{
    public class RolePermissionEntity
    {
        public int RoleId { get; set; }
        public Area Area { get; set; }
        public AreaAction Action { get; set; }

        public UserRoleEntity Role { get; set; }
        public AreaEntity AreaEntity { get; set; }
        public AreaActionEntity AreaActionEntity { get; set; }
    }
}
