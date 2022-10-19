using System;
namespace CitationWebAPI.Models
{
    public class CitationWithViolations
    {
        public Citation citation { get; set; }
        public List<Violation> violations { get; set; }
    }
}

