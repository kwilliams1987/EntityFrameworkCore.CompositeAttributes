using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.EntityFrameworkCore
{
    static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetTypesByPropertyAttribute(this AppDomain appDomain, Type attribute)
        {
            var types = appDomain.GetAssemblies().SelectMany(s => s.GetTypes());
            return types.Where(t => t.GetProperties().Any(p => Attribute.IsDefined(p, attribute)));
        }

        public static IEnumerable<PropertyInfo> GetPropertiesByAttribute(this Type type, Type attribute)
        {
            return type.GetProperties().Where(p => Attribute.IsDefined(p, attribute));
        }

        public static T GetAttribute<T>(this PropertyInfo property)
            where T : class
        {
            return Attribute.GetCustomAttribute(property, typeof(T)) as T;
        }
    }
}
