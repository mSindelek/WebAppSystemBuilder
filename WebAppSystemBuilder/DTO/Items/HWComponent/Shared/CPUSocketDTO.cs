using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.DTO.Items.HWComponent.Shared {
    public class CPUSocketDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [StringLength(200)]
        public string? ShortDesc { get; set; }
    }
}
