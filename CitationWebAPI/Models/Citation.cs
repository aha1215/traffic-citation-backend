﻿
using DateOnlyTimeOnly.AspNet.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
/**
* A citation entity
*/
namespace CitationWebAPI.Models
{
    public class Citation
    {
        [Key]
        public int citation_id { get; set; }
        public int driver_id { get; set; }
        public int user_id { get; set; }
        public string type { get; set; } = String.Empty;
        public DateOnly? date { get; set; }
        public TimeOnly? time { get; set; }
        public bool owner_fault { get; set; }
        public string desc { get; set; } = String.Empty;
        public string violation_loc { get; set; } = String.Empty;
        public DateOnly? sign_date { get; set; }
        public string vin { get; set; } = String.Empty;
        public string vin_state { get; set; } = String.Empty;
        public string code_section { get; set; } = String.Empty;
        public string officer_name { get; set; } = String.Empty;
        public string officer_badge { get; set; } = String.Empty;
    }
}