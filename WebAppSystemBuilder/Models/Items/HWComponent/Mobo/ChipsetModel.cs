using System.ComponentModel.DataAnnotations;
using WebAppSystemBuilder.Models.Items.HWComponent.Shared;

namespace WebAppSystemBuilder.Models.Items.HWComponent.Mobo
{
    public class ChipsetModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        public required CPUSocketModel Socket { get; set; }
        [Required]
        public required RamTypeModel RamType { get; set; }
        [StringLength(200)]
        public string? ShortDesc { get; set; }
    }
}
