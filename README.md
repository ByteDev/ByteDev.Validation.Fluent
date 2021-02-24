[![Build status](https://ci.appveyor.com/api/projects/status/github/bytedev/ByteDev.Validation.Fluent?branch=master&svg=true)](https://ci.appveyor.com/project/bytedev/ByteDev-Validation-Fluent/branch/master)
[![NuGet Package](https://img.shields.io/nuget/v/ByteDev.Validation.Fluent.svg)](https://www.nuget.org/packages/ByteDev.Validation.Fluent)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://github.com/ByteDev/ByteDev.Validation.Fluent/blob/master/LICENSE)

# ByteDev.Validation.Fluent

Extensions and functionality to use with the [FluentValidation](https://www.nuget.org/packages/FluentValidation/) library.

## Installation

ByteDev.Validation.Fluent has been written as a .NET Standard 2.0 library, so you can consume it from a .NET Core or .NET Framework 4.6.1 (or greater) application.

ByteDev.Validation.Fluent is hosted as a package on nuget.org.  To install from the Package Manager Console in Visual Studio run:

`Install-Package ByteDev.Validation.Fluent`

Further details can be found on the [nuget page](https://www.nuget.org/packages/ByteDev.Validation.Fluent/).

## Release Notes

Releases follow semantic versioning.

Full details of the release notes can be viewed on [GitHub](https://github.com/ByteDev/ByteDev.Validation.Fluent/blob/master/docs/RELEASE-NOTES.md).

## Usage

### Method extensions

IRuleBuilder<T, string>:
- IsDigits
- IsDigitsOrEmpty
- IsEmailAddress
- IsEmailAddressOrEmpty
- IsIn
- IsGuid
- IsGuidOrEmpty
- IsUrl
- IsUrlOrEmpty

```csharp
using FluentValidation;

// Request DTO

public class DummyRequest
{
    public string Guid { get; set; }

    public string GuidWithOverriddenMessage { get; set; }
}

// FluentValidation Validator for the request DTO

public class DummyRequestValidator : AbstractValidator<DummyRequest>
{
    public DummyRequestValidator()
    {
        RuleFor(r => r.Guid)
            .IsGuid();

        RuleFor(r => r.GuidWithOverriddenMessage)
            .IsGuid()
            .WithMessage("This is a overridden message");
    }
}
```

---

### ValidationError

The package also provides a type `ValidationError` and extension method `WithValidationError` to help when creating specific validation errors.

These can be useful for example in the case of an API where the client sends a request that fails validation. A `ValidationError` can be created that encapsulates the error `Code`, `Message` and `PropertyName` that caused the error and this can then be reported back to the caller.

```csharp
// Define some ValidationErrors

public static class ApiValidationErrors
{
    public static ValidationError GuidIsNotValid(string propertyName) => new ValidationError("1001", "Must be a valid GUID.", propertyName);

    // ... add more
}

// Use the ValidationError from within the Validator

public class ValidationErrorTestValidator : AbstractValidator<DummyRequest>
{
    public ValidationErrorTestValidator()
    {
        RuleFor(r => r.Guid)
            .IsGuid()
            .WithValidationError(ApiValidationErrors.GuidIsNotValid(nameof(DummyRequest.Guid)));
    }
}
```