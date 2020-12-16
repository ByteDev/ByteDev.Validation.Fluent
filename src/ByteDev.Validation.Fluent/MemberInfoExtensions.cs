using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ByteDev.Validation.Fluent
{
    internal static class MemberInfoExtensions
    {
        public static string GetJsonPropertyNameOrDefault(this MemberInfo source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var attribute = GetAttribute<JsonPropertyNameAttribute>(source);

            return attribute == null ? source.Name : attribute.Name;
        }
        
        private static TAttribute GetAttribute<TAttribute>(ICustomAttributeProvider type) where TAttribute : Attribute
        {
            var attributes = type.GetCustomAttributes(typeof(TAttribute), true);

            if (attributes.Length > 0)
            {
                return (TAttribute)attributes.Single();
            }

            return null;
        }
    }
}