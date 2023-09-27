namespace API.Models.Validations.Responses;

public class ValidationFailureResponse
{
    public IEnumerable<ValidationResponse>? Errors { get; set; } = Enumerable.Empty<ValidationResponse>();
}