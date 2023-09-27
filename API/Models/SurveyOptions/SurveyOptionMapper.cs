using Boxed.Mapping;
using MessagingContracts.Survey;

namespace API.Models.SurveyOptions;

public class SurveyOptionMapper : IMapper<Option, SurveyOption>, IMapper<SurveyOption, Option>
{
    public void Map(Option source, SurveyOption destination)
    {
        destination.Id = source.SurveyOptionId;
        destination.Text = source.Text;
        destination.TimesSelected = source.TimesSelected;
        destination.Position = source.Position;
    }

    public void Map(SurveyOption source, Option destination)
    {
        destination.SurveyOptionId = source.Id;
        destination.Text = source.Text;
        destination.TimesSelected = source.TimesSelected;
        destination.Position = source.Position;
    }
}