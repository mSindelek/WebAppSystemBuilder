using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.DTO.Items.HWComponent.RAM {
    public class ModuleTypeDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public required string Name { get; set; }
        [StringLength(200)]
        public string? ShortDesc { get; set; }
    }
}
