using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviation.Web.Models
{
    public enum DonationType
    {
        Food,
        Clothing,
        MedicalSupplies,
        Shelter,
        Other
    }

    public class Donation
    {
        public int Id { get; set; }

        [Required]
        public DonationType Type { get; set; }

        [Required, StringLength(100)]
        public string ItemName { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [StringLength(200)]
        public string? Notes { get; set; }

        public DateTime DonatedAt { get; set; } = DateTime.UtcNow;

        public string? DonorUserId { get; set; }

        public bool Allocated { get; set; }
    }
}
