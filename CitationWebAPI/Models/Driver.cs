using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
* Defines properties for a driver that will be stored in database
* Used by Entity Framework Core
*/

namespace CitationWebAPI.Models
{
    public class Driver
    {
        [Key]
        // Auto generate primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int driver_id { get; set; }
        public string driver_name { get; set; } = String.Empty;
        public DateOnly? date_birth { get; set; }
        public char sex { get; set; }
        public string hair { get; set; } = String.Empty;
        public string eyes { get; set; } = String.Empty;
        public string height { get; set; } = String.Empty;
        public int weight { get; set; }
        public string race { get; set; } = String.Empty;
        public string address { get; set; } = String.Empty;
        public string city { get; set; } = String.Empty;
        public string state { get; set; } = String.Empty;
        public int zip { get; set; }

        // Must have unique license number for each driver
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string license_no { get; set; } = String.Empty;
        public string license_class { get; set; } = String.Empty;
    }
}
