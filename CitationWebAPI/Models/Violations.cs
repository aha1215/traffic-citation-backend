using System.ComponentModel.DataAnnotations;

/*
 * Might change this later to use an array instead ....or remove this and add it to citations as a string array
 */

namespace CitationWebAPI.Models
{
    public class Violations
    {
        [Key]
        public int violation_id { get; set; }
        public int citation_id { get; set; }
        public string violation1 { get; set; } = String.Empty;
        public string violation2 { get; set; } = String.Empty;
        public string violation3 { get; set; } = String.Empty;
        public string violation4 { get; set; } = String.Empty;
        public string violation5 { get; set; } = String.Empty;
        public string violation6 { get; set; } = String.Empty;
        public string violation7 { get; set; } = String.Empty;
        public string violation8 { get; set; } = String.Empty;
        public string violation9 { get; set; } = String.Empty;
        public string violation10 { get; set; } = String.Empty;
    }
}
