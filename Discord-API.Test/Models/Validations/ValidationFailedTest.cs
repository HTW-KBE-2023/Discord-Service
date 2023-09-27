using API.Models.Validations;
using FluentValidation.Results;

namespace API.Test.Models.Validations
{
    public class ValidationFailedTest
    {
        [Fact]
        public void WhenValidationFailedIsCreatedWithOneValidationFailureThenErrorsShouldContainOnlyOneValidationFailure()
        {
            var validationError = new ValidationFailure("Test", "Test");
            var validationFailed = new ValidationFailed(validationError);

            Assert.Single(validationFailed.Errors, validationError);
            Assert.Equal("Test", validationFailed.Errors.First().PropertyName);
            Assert.Equal("Test", validationFailed.Errors.First().ErrorMessage);
        }

        [Fact]
        public void WhenValidationFailedIsCreatedWithManyValidationFailureThenErrorsShouldContainAllValidationFailure()
        {
            var validationError1 = new ValidationFailure("Test", "Test");
            var validationError2 = new ValidationFailure("Test2", "Test2");

            var validationFailed = new ValidationFailed(new List<ValidationFailure>() { validationError1, validationError2 });

            Assert.Contains(validationError1, validationFailed.Errors);
            Assert.Contains(validationError2, validationFailed.Errors);
            Assert.Equal(2, validationFailed.Errors.Count());
            Assert.Equal("Test", validationFailed.Errors.ElementAt(0).PropertyName);
            Assert.Equal("Test", validationFailed.Errors.ElementAt(0).ErrorMessage);
            Assert.Equal("Test2", validationFailed.Errors.ElementAt(1).PropertyName);
            Assert.Equal("Test2", validationFailed.Errors.ElementAt(1).ErrorMessage);
        }
    }
}