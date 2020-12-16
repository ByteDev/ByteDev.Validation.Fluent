using System.Collections.Generic;
using System.Linq;
using ByteDev.Strings;
using FluentValidation;

namespace ByteDev.Validation.Fluent
{
    public static class RuleBuilderExtensions
    {
        /// <summary>
        /// Defines a validator on the current rule builder that checks whether the provided string
        /// is a valid Guid.
        /// </summary>
        /// <typeparam name="T">Type of the object being validated.</typeparam>
        /// <param name="source">Rule builder on which the validator will be defined.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsGuid<T>(this IRuleBuilder<T, string> source)
        {
            return source
                .Must(s => s.IsGuid())
                .WithMessage("{PropertyName} must be a valid GUID.");
        }

        /// <summary>
        /// Defines a validator on the current rule builder that checks whether the provided string
        /// is a valid Guid, null or empty.
        /// </summary>
        /// <typeparam name="T">Type of the object being validated.</typeparam>
        /// <param name="source">Rule builder on which the validator will be defined.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsGuidOrEmpty<T>(this IRuleBuilder<T, string> source)
        {
            return source
                .Must(s =>
                {
                    if (string.IsNullOrEmpty(s))
                        return true;

                    return s.IsGuid();
                })
                .WithMessage("{PropertyName} must be a valid GUID.");
        }

        /// <summary>
        /// Defines a validator on the current rule builder that checks whether the provided string
        /// is a valid URL.
        /// </summary>
        /// <typeparam name="T">Type of the object being validated.</typeparam>
        /// <param name="source">Rule builder on which the validator will be defined.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsUrl<T>(this IRuleBuilder<T, string> source)
        {
            return source
                .Must(s => s.IsHttpUrl())
                .WithMessage("{PropertyName} must be a valid HTTP URL.");
        }

        /// <summary>
        /// Defines a validator on the current rule builder that checks whether the provided string
        /// is a valid URL, null or empty.
        /// </summary>
        /// <typeparam name="T">Type of the object being validated.</typeparam>
        /// <param name="source">Rule builder on which the validator will be defined.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsUrlOrEmpty<T>(this IRuleBuilder<T, string> source)
        {
            return source
                .Must(s =>
                {
                    if (string.IsNullOrEmpty(s))
                        return true;

                    return s.IsHttpUrl();
                })
                .WithMessage("{PropertyName} must be a valid HTTP URL or not set.");
        }

        /// <summary>
        /// Defines a validator on the current rule builder that checks whether the provided string
        /// is a valid email address.
        /// </summary>
        /// <typeparam name="T">Type of the object being validated.</typeparam>
        /// <param name="source">Rule builder on which the validator will be defined.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsEmailAddress<T>(this IRuleBuilder<T, string> source)
        {
            return source
                .Must(s => s.IsEmailAddress())
                .WithMessage("{PropertyName} must be a valid email address.");
        }

        /// <summary>
        /// Defines a validator on the current rule builder that checks whether the provided string
        /// is a valid email address, null or empty.
        /// </summary>
        /// <typeparam name="T">Type of the object being validated.</typeparam>
        /// <param name="source">Rule builder on which the validator will be defined.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsEmailAddressOrEmpty<T>(this IRuleBuilder<T, string> source)
        {
            return source
                .Must(s =>
                {
                    if (string.IsNullOrEmpty(s))
                        return true;
                    
                    return s.IsEmailAddress();
                })
                .WithMessage("{PropertyName} must be a valid email address or not set.");
        }

        /// <summary>
        /// Defines a validator on the current rule builder that checks whether the provided string
        /// contains only digits.
        /// </summary>
        /// <typeparam name="T">Type of the object being validated.</typeparam>
        /// <param name="source">Rule builder on which the validator will be defined.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsDigits<T>(this IRuleBuilder<T, string> source)
        {
            return source
                .Must(s => s.IsDigits())
                .WithMessage("{PropertyName} must contain only digits.");
        }

        /// <summary>
        /// Defines a validator on the current rule builder that checks whether the provided string
        /// contains only digits, null or empty.
        /// </summary>
        /// <typeparam name="T">Type of the object being validated.</typeparam>
        /// <param name="source">Rule builder on which the validator will be defined.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsDigitsOrEmpty<T>(this IRuleBuilder<T, string> source)
        {
            return source
                .Must(s =>
                {
                    if (string.IsNullOrEmpty(s))
                        return true;
                    
                    return s.IsDigits();
                })
                .WithMessage("{PropertyName} must contain only digits or not set.");
        }

        /// <summary>
        /// Defines a validator on the current rule builder that checks whether the provided string
        /// is contained in a collection of valid items.
        /// </summary>
        /// <typeparam name="T">Type of the object being validated.</typeparam>
        /// <param name="source">Rule builder on which the validator will be defined.</param>
        /// <param name="validItems">Collection of possible valid strings.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsIn<T>(this IRuleBuilder<T, string> source, IEnumerable<string> validItems)
        {
            return source
                .Must(validItems.Contains)
                .WithMessage("{PropertyName} must contain one of the defined set of values.");
        }

        /// <summary>
        /// Defines a validator on the current rule builder that checks whether the provided string
        /// is contained in a collection of valid items.
        /// </summary>
        /// <typeparam name="T">Type of the object being validated.</typeparam>
        /// <param name="source">Rule builder on which the validator will be defined.</param>
        /// <param name="validItems">Collection of possible valid strings.</param>
        /// <param name="comparer">Comparer to use when checking if the string is contained in the valid items collection.</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> IsIn<T>(this IRuleBuilder<T, string> source, IEnumerable<string> validItems, IEqualityComparer<string> comparer)
        {
            return source
                .Must(s => validItems.Contains(s, comparer))
                .WithMessage("{PropertyName} must contain one of the defined set of values.");
        }
    }
}