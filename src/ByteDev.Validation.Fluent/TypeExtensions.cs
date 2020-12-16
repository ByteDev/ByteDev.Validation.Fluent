using System;

namespace ByteDev.Validation.Fluent
{
    internal static class TypeExtensions
    {
        public static string GetJsonPropertyName(this Type source, string propertyName)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property name was null or empty.", nameof(propertyName));

            var propertyInfo = source.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"No property called: '{propertyName}' exists on type: {source.FullName}.");
            }

            return propertyInfo.GetJsonPropertyNameOrDefault();
        }
    }
}