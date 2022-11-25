namespace CitationWebAPI.Models
{
    public class CompleteCitation
    {
        public Citation citation { get; set; }
        public Driver driver { get; set; }
        public List<Violation> violations { get; set; }

    }
}
