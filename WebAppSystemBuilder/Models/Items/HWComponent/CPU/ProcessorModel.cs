using System.ComponentModel.DataAnnotations;
using WebAppSystemBuilder.Models.Items.HWComponent.Shared;

namespace WebAppSystemBuilder.Models.Items.HWComponent.CPU
{
    public class ProcessorModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required]
        public required CPUSocketModel Socket { get; set; }
        public int TDP { get; set; }
        [StringLength(200)]
        public string? ShortDesc { get; set; }
        public string? Description { get; set; }
    }
}
