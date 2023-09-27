using API.Models.Validations.Responses;
using Boxed.Mapping;
using FluentValidation.Results;

namespace API.Models.Validations;

public class ValidationFailureMapper : IMapper<ValidationFailure, ValidationResponse>
{
    public void Map(ValidationFailure source, ValidationResponse destination)
    {
        destination.PropertyName = source.PropertyName;
        destination.Message = source.ErrorMessage;
    }
}