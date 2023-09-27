using API.Models.SurveyOptions;

namespace API.Models.Surveys
{
    public class Survey : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public ulong? ConnectedMessageId { get; set; }

        public IList<SurveyOption> SurveyOptions { get; set; } = new List<SurveyOption>();
    }
}