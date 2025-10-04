using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviation.Web.Models
{
    public class VolunteerTask
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime StartAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? EndAt { get; set; }

        [Range(1, 1000)]
        public int Capacity { get; set; } = 10;
    }

    public class VolunteerAssignment
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
