using FluentValidation;
using NUnit.Framework;

namespace ByteDev.Validation.Fluent.UnitTests
{
    [TestFixture]
    public class RuleBuilderWithExtensionsTests : ValidatorTestBase
    {
        private ValidationErrorTestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ValidationErrorTestValidator();
        }

        [Test]
        public void WhenValidationPasses_ThenReturnValid()
        {
            var result = _sut.Validate(new DummyRequest { Url = "http://www.google.com/" });

            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void WhenValidationFails_ThenReturnInvalid()
        {
            var result = _sut.Validate(new DummyRequest { Url = null });

            AssertInvalid(result, "100", "Custom error message", "Url");
        }

        public class ValidationErrorTestValidator : AbstractValidator<DummyRequest>
        {
            public ValidationErrorTestValidator()
            {
                RuleFor(r => r.Url)
                    .IsUrl()
                    .WithValidationError(new ValidationError("100", "Custom error message"));
            }
        }
    }
}