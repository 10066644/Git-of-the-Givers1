using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviation.Web.Models
{
    public class Incident
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [StringLength(32)]
        public string Severity { get; set; } = "Moderate"; // Low, Moderate, High, Critical

        public string? ReporterUserId { get; set; }
    }
}
