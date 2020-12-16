using System.Linq;
using FluentValidation.Results;
using NUnit.Framework;

namespace ByteDev.Validation.Fluent.UnitTests
{
    public abstract class ValidatorTestBase
    {
        protected void AssertInvalid(ValidationResult result, string expectedCode, string expectedMessage, string expectedPropertyName)
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Single().ErrorCode, Is.EqualTo(expectedCode));
            Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo(expectedMessage));
            Assert.That(result.Errors.Single().PropertyName, Is.EqualTo(expectedPropertyName));
        }
    }
}