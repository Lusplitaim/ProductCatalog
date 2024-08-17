using System.Collections;

namespace ProductCatalog.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNonStringEnumerable(this Type type)
        {
            if (type == null || type == typeof(string))
            {
                return false;
            }
            return typeof(IEnumerable).IsAssignableFrom(type);
        }
    }
}
