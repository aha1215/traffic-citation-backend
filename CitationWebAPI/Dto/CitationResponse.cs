using CitationWebAPI.Models;

namespace CitationWebAPI.Dto
{
    public class CitationResponse
    {
        public List<CompleteCitation> CompleteCitationList { get; set; } = new List<CompleteCitation>();
        public int TotalCitationsCount { get; set; } // How many items in database
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
