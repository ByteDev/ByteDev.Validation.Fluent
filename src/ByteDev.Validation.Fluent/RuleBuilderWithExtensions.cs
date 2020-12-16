using System;
using FluentValidation;
using FluentValidation.Resources;

namespace ByteDev.Validation.Fluent
{
    /// <summary>
    /// Extension methods for FluentValidation.IRuleBuilderOptions.
    /// </summary>
    public static class RuleBuilderWithExtensions
    {
        /// <summary>
        /// Define a validation error to use if the validation is invalid.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="source">The current rule.</param>
        /// <param name="validationError">Validation error.</param>
        /// <param name="useJsonPropertyName">True uses value from <see cref="T:System.Text.Json.JsonPropertyName" /> based on the property name.</param>
        /// <param name="overrideType">Override the type of the object being checked for validation errors (defined by <typeparamref name="T" />). Provide when you want to define the base type but are validating a nested type.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> WithValidationError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> source,
            ValidationError validationError,
            bool useJsonPropertyName = false,
            Type overrideType = null)
        {
            if (useJsonPropertyName && validationError.IsPropertyNameSet)
            {
                if (overrideType == null)
                    source.OverridePropertyName(typeof(T).GetJsonPropertyName(validationError.PropertyName));
                else
                    source.OverridePropertyName(overrideType.GetJsonPropertyName(validationError.PropertyName));
            }

            return source.Configure(pr =>
            {
                pr.CurrentValidator.Options.ErrorMessageSource = new StaticStringSource(validationError.Message);
                pr.CurrentValidator.Options.ErrorCodeSource = new StaticStringSource(validationError.Code);

                pr.CurrentValidator.Options.CustomStateProvider = c => validationError;
            });
        }
    }
}