using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

/**
 *  A class to convert between TimeOnly and TimeSpan for use with Entity Framework/SQL DB
 */

namespace CitationWebAPI.Converters
{
    public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
    {
        public TimeOnlyConverter() : base(
            timeOnly => timeOnly.ToTimeSpan(),
            timeSpan => TimeOnly.FromTimeSpan(timeSpan)) { }
    }
}
