using API.Models.Validations.Responses;
using Boxed.Mapping;
using FluentValidation.Results;
using API.Services.Validations;

namespace API.Test.Services.Validations
{
    public class ValidationFailureMapperTest

    {
        [Fact]
        public void WhenValidationFailureIsMappedToValidationResponseThenPropertiesShouldBeTheSame()
        {
            IMapper<ValidationFailure, ValidationResponse> mapper = new ValidationFailureMapper();

            var validationError = new ValidationFailure("Test", "Test");

            var response = mapper.Map(validationError);

            Assert.NotNull(response);
            Assert.Equal(validationError.PropertyName, response.PropertyName);
            Assert.Equal(validationError.ErrorMessage, response.Message);
        }
    }
}