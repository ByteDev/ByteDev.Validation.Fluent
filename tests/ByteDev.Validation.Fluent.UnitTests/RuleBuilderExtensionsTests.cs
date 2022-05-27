using System;
using System.Linq;
using FluentValidation;
using NUnit.Framework;

namespace ByteDev.Validation.Fluent.UnitTests
{
    [TestFixture]
    public class RuleBuilderExtensionsTests
    {
        private DummyRequest _request;

        private DummyRequestValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _request = DummyRequest.CreateValid();

            _sut = new DummyRequestValidator();
        }

        [Test]
        public void WhenRequestIsValid_ThenReturnValid()
        {
            var result = _sut.Validate(_request);

            Assert.That(result.IsValid, Is.True);
        }

        [TestFixture]
        public class IsGuid : RuleBuilderExtensionsTests
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            [TestCase("a")]
            public void WhenIsNotGuid_ThenReturnInvalid(string guid)
            {
                _request.Guid = guid;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo($"{nameof(DummyRequest.Guid)} must be a valid GUID."));
            }

            [Test]
            public void WhenIsNotGuid_AndHasOverriddenMessage_ThenReturnOverriddenMessage()
            {
                _request.GuidWithOverriddenMessage = "a";

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("This is a overridden message"));
            }
        }

        [TestFixture]
        public class IsGuidOrEmpty : RuleBuilderExtensionsTests
        {
            [TestCase("a")]
            public void WhenIsNotGuid_ThenReturnInvalid(string guid)
            {
                _request.GuidOrEmpty = guid;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Guid Or Empty must be a valid GUID."));
            }

            [TestCase(null)]
            [TestCase("")]
            public void WhenIsNullOrEmpty_ThenReturnValid(string guid)
            {
                _request.GuidOrEmpty = guid;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.True);
            }
        }

        [TestFixture]
        public class IsUrl : RuleBuilderExtensionsTests
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            [TestCase("a")]
            public void WhenIsNotUrl_ThenReturnInvalid(string url)
            {
                _request.Url = url;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Url must be a valid HTTP URL."));
            }
        }

        [TestFixture]
        public class IsUrlOrEmpty : RuleBuilderExtensionsTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenIsNullOrEmpty_ThenReturnValid(string value)
            {
                _request.UrlOrEmpty = value;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.True);
            }

            [Test]
            public void WhenIsNotUrl_ThenReturnInvalid()
            {
                _request.UrlOrEmpty = "notAUrl";

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Url Or Empty must be a valid HTTP URL or not set."));
            }
        }

        [TestFixture]
        public class IsEmailAddress : RuleBuilderExtensionsTests
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            [TestCase("a")]
            public void WhenIsNotEmailAddress_ThenReturnInvalid(string email)
            {
                _request.EmailAddress = email;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Email Address must be a valid email address."));
            }
        }

        [TestFixture]
        public class IsEmailAddressOrEmpty : RuleBuilderExtensionsTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenIsNullOrEmpty_ThenReturnValid(string value)
            {
                _request.EmailAddressOrEmpty = value;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.True);
            }

            [Test]
            public void WhenIsNotEmailAddress_ThenReturnInvalid()
            {
                _request.EmailAddressOrEmpty = "notEmaillAddress";

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Email Address Or Empty must be a valid email address or not set."));
            }
        }

        [TestFixture]
        public class IsDigits : RuleBuilderExtensionsTests
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            [TestCase("a")]
            public void WhenIsNotDigits_ThenReturnInvalid(string digits)
            {
                _request.Digits = digits;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Digits must contain only digits."));
            }
        }

        [TestFixture]
        public class IsDigitsOrEmpty : RuleBuilderExtensionsTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenIsNullOrEmpty_ThenReturnValid(string value)
            {
                _request.DigitsOrEmpty = value;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.True);
            }

            [TestCase("A")]
            [TestCase("A1")]
            [TestCase("0A")]
            public void WhenIsNotDigits_ThenReturnInvalid(string digits)
            {
                _request.DigitsOrEmpty = digits;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Digits Or Empty must contain only digits or not set."));
            }
        }

        [TestFixture]
        public class IsIn : RuleBuilderExtensionsTests
        {
            [Test]
            public void WhenIsNotInCollection_ThenReturnInvalid()
            {
                _request.In = "D";

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("In must contain one of the defined set of values."));
            }

            [TestCase("A")]
            [TestCase("B")]
            [TestCase("C")]
            [TestCase("")]
            [TestCase(null)]
            public void WhenIsInCollection_ThenReturnValid(string value)
            {
                _request.InIgnoreCase = value;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.True);
            }

            [Test]
            public void WhenIgnoreCase_AndIsNotInCollection_ThenReturnInvalid()
            {
                _request.InIgnoreCase = "D";

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("In Ignore Case must contain one of the defined set of values."));
            }

            [TestCase("a")]
            [TestCase("b")]
            [TestCase("c")]
            [TestCase("")]
            [TestCase(null)]
            public void WhenIgnoreCase_AndIsInCollection_ThenReturnValid(string value)
            {
                _request.InIgnoreCase = value;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.True);
            }
        }

        [TestFixture]
        public class IsDateTime : RuleBuilderExtensionsTests
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase("2000-01-01 12:00:00")]
            [TestCase("2000-01-01T24:00:00")]
            public void WhenIsNotInFormat_ThenReturnInvalid(string value)
            {
                _request.DateTime = value;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Date Time must be a valid date time in format: yyyy-MM-ddThh:mm:ss."));
            }
        }

        [TestFixture]
        public class IsDateTimeOrEmpty : RuleBuilderExtensionsTests
        {
            [TestCase(null)]
            [TestCase("")]
            public void WhenIsNullOrEmpty_ThenReturnValid(string value)
            {
                _request.DateTimeOrEmpty = value;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.True);
            }
            
            [TestCase("2000-01-01 12:00:00")]
            [TestCase("2000-01-01T24:00:00")]
            public void WhenIsNotInFormat_ThenReturnInvalid(string value)
            {
                _request.DateTimeOrEmpty = value;

                var result = _sut.Validate(_request);

                Assert.That(result.IsValid, Is.False);
                Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("Date Time Or Empty must be a valid date time in format: yyyy-MM-ddThh:mm:ss."));
            }
        }
    }

    public class DummyRequest
    {
        public string Guid { get; set; }

        public string GuidWithOverriddenMessage { get; set; }

        public string GuidOrEmpty { get; set; }

        public string Url { get; set; }

        public string UrlOrEmpty { get; set; }

        public string EmailAddress { get; set; }

        public string EmailAddressOrEmpty { get; set; }

        public string Digits { get; set; }

        public string DigitsOrEmpty { get; set; }

        public string In { get; set; }
        
        public string InIgnoreCase { get; set; }

        public string DateTime { get; set; }

        public string DateTimeOrEmpty { get; set; }

        public static DummyRequest CreateValid()
        {
            return new DummyRequest
            {
                Guid = System.Guid.NewGuid().ToString(),
                GuidWithOverriddenMessage = System.Guid.NewGuid().ToString(),
                GuidOrEmpty = System.Guid.NewGuid().ToString(),
                Url = "http://localhost/",
                UrlOrEmpty = "http://localhost/",
                EmailAddress = "someone@somewhere.com",
                EmailAddressOrEmpty = "someone@somewhere.com",
                Digits = "0123",
                DigitsOrEmpty = "0123",
                In = null,
                InIgnoreCase = null,
                DateTime = "2000-01-01T12:00:00",
                DateTimeOrEmpty = "2000-01-01T12:00:00"
            };
        }
    }

    public class DummyRequestValidator : AbstractValidator<DummyRequest>
    {
        public DummyRequestValidator()
        {
            RuleFor(r => r.Guid)
                .IsGuid();

            RuleFor(r => r.GuidWithOverriddenMessage)
                .IsGuid()
                .WithMessage("This is a overridden message");

            RuleFor(r => r.GuidOrEmpty)
                .IsGuidOrEmpty();

            RuleFor(r => r.Url)
                .IsUrl();

            RuleFor(r => r.UrlOrEmpty)
                .IsUrlOrEmpty();

            RuleFor(r => r.EmailAddress)
                .IsEmailAddress();

            RuleFor(r => r.EmailAddressOrEmpty)
                .IsEmailAddressOrEmpty();

            RuleFor(r => r.Digits)
                .IsDigits();

            RuleFor(r => r.DigitsOrEmpty)
                .IsDigitsOrEmpty();

            RuleFor(r => r.In)
                .IsIn(new[] {"A", "B", "C", "", null});

            RuleFor(r => r.InIgnoreCase)
                .IsIn(new[] {"A", "B", "C", "", null}, StringComparer.InvariantCultureIgnoreCase);

            RuleFor(r => r.DateTime)
                .IsDateTime("yyyy-MM-ddThh:mm:ss");

            RuleFor(r => r.DateTimeOrEmpty)
                .IsDateTimeOrEmpty("yyyy-MM-ddThh:mm:ss");
        }
    }
}