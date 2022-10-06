using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

/**
 *  A class to convert between DateOnly and DateTime for use with Entity Framework/SQL DB
 */

namespace CitationWebAPI.Converter
{
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter(): base(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime)) { }
    }
}
