using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.Models.Items.HWComponent.Shared
{
    public class CPUSocketModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [StringLength(200)]
        public string? ShortDesc { get; set; }
    }
}
