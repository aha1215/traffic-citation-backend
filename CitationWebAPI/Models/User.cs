using System.ComponentModel.DataAnnotations;

namespace CitationWebAPI.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string username { get; set; } = String.Empty;
        public string password { get; set; } = String.Empty;
        public string email { get; set; } = String.Empty;
        public string officer_badge { get; set; } = String.Empty;
        public string officer_name { get; set; } = String.Empty;
    }
}
