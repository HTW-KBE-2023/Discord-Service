using API.Models.Validations;
using API.Models.Validations.Responses;
using API.Services.Validations;
using Boxed.Mapping;
using FluentValidation.Results;

namespace API.Test.Services.Validations
{
    public class ValidationFailedMapperTest

    {
        [Fact]
        public void WhenValidationFailedIsMappedToValidationFailureResponseThenPropertiesShouldBeTheSame()
        {
            IMapper<ValidationFailed, ValidationFailureResponse> mapper = new ValidationFailedMapper();

            var validationError1 = new ValidationFailure("Test", "Test");
            var validationError2 = new ValidationFailure("Test2", "Test2");
            var validationFailed = new ValidationFailed(new List<ValidationFailure>() { validationError1, validationError2 });

            var response = mapper.Map(validationFailed);

            Assert.NotNull(response);
            Assert.NotNull(response.Errors);
            Assert.Equal(2, response.Errors.Count());
            Assert.Equal(validationError1.PropertyName, response.Errors.ElementAt(0).PropertyName);
            Assert.Equal(validationError1.ErrorMessage, response.Errors.ElementAt(0).Message);
            Assert.Equal(validationError2.PropertyName, response.Errors.ElementAt(1).PropertyName);
            Assert.Equal(validationError2.ErrorMessage, response.Errors.ElementAt(1).Message);
        }
    }
}