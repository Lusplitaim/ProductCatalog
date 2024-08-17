using Microsoft.AspNetCore.Mvc.Formatters;
using ProductCatalog.Core.Attributes;
using ProductCatalog.Core.Data;
using ProductCatalog.Core.Extensions;
using ProductCatalog.Core.Models.Enums;
using ProductCatalog.Core.Utils;
using System.Reflection;
using System.Text.Json;

namespace ProductCatalog.API.Formatters
{
    internal class AllowedPropertiesJsonOutputFormatter : SystemTextJsonOutputFormatter
    {
        private IEnumerable<UserRoleType> m_UserRoles;
        public AllowedPropertiesJsonOutputFormatter(JsonSerializerOptions jsonSerializerOptions) : base(jsonSerializerOptions)
        {
            m_UserRoles = [];
        }

        public override async Task WriteAsync(OutputFormatterWriteContext context)
        {
            if (context.Object is null)
            {
                return;
            }

            await LoadCurrentUserRolesAsync(context.HttpContext);

            NullifyDisallowedProperties(context.Object);

            await base.WriteAsync(context);
        }

        private async Task LoadCurrentUserRolesAsync(HttpContext context)
        {
            var uow = context.RequestServices.GetRequiredService<IUnitOfWork>();
            var authUtils = context.RequestServices.GetRequiredService<IAuthUtils>();

            var userRoleEntities = await uow.UserRoleRepository.GetByUserIdAsync(authUtils.GetAuthUserId());
            m_UserRoles = userRoleEntities.Select(e => (UserRoleType)e.RoleId).ToList();
        }

        private void NullifyDisallowedProperties(object obj)
        {
            if (obj is IEnumerable<object> collection)
            {
                foreach (var item in collection)
                {
                    NullifyDisallowedProperties(item);
                }
                return;
            }

            foreach (var pi in obj.GetType().GetProperties())
            {
                if (ShouldNullifyProperty(pi))
                {
                    pi.SetValue(obj, null);
                    continue;
                }

                bool isUserDefined = (pi.PropertyType.FullName ?? "").StartsWith("ProductCatalog.");
                if (pi.PropertyType.IsClass && isUserDefined || pi.PropertyType.IsNonStringEnumerable())
                {
                    var propObj = pi.GetValue(obj);
                    if (propObj is not null)
                    {
                        NullifyDisallowedProperties(propObj);
                    }
                    continue;
                }
            }
        }

        private bool ShouldNullifyProperty(PropertyInfo pi)
        {
            var requiredRolesAttr = pi.GetCustomAttribute<RequiredRolesAttribute>();

            return requiredRolesAttr is not null
                && requiredRolesAttr!.RequiredRoles.Intersect(m_UserRoles).Count() == 0;
        }
    }
}
