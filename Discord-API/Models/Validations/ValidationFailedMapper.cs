using API.Models.Validations.Responses;
using Boxed.Mapping;

namespace API.Models.Validations;

public class ValidationFailedMapper : IMapper<ValidationFailed, ValidationFailureResponse>
{
    private ValidationFailureMapper _validationFailureMapper = new();

    public void Map(ValidationFailed source, ValidationFailureResponse destination)
    {
        destination.Errors = _validationFailureMapper.MapList(source.Errors);
    }
}