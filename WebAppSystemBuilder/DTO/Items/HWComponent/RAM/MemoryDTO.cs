using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.DTO.Items.HWComponent.RAM {
    public class MemoryDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        public required string RamTypeName { get; set; }
        [Required]
        public required int RamTypeId { get; set; }
        public required string ModuleTypeName { get; set; }
        [Required]
        public required int ModuleTypeId { get; set; }
        [Required]
        public required bool ECC { get; set; }

        //public int Clock { get; set; }
        //public int CapacityGb { get; set; }

        [StringLength(200)]
        public string? ShortDesc { get; set; }
        public string? Description { get; set; }
    }
}
