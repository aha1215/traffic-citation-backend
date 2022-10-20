using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace CitationWebAPI.Models;

/**
* Defines properties for a violation that will be stored in database
* Used by Entity Framework Core
*/

public class Violation
{
    [Key]
    public int violation_id { get; set; }
    public int citation_id { get; set; }
    public string group { get; set; } = String.Empty;
    public string code { get; set; } = String.Empty;
    public char degree { get; set; }
    public string desc { get; set; } = String.Empty;
}

