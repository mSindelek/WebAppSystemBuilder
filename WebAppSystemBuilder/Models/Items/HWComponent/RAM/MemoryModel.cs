using System.ComponentModel.DataAnnotations;
using WebAppSystemBuilder.Models.Items.HWComponent.Shared;

namespace WebAppSystemBuilder.Models.Items.HWComponent.RAM
{
    public class MemoryModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required]
        public required RamTypeModel RamType { get; set; }
        [Required]
        public required ModuleTypeModel ModuleType { get; set; }
        [Required]
        public required bool ECC { get; set; }

        //public int Clock { get; set; }
        //public int CapacityGb { get; set; }

        [StringLength(200)]
        public string? ShortDesc { get; set; }
        public string? Description { get; set; }
    }
}
