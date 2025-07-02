using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.DTO.Items.HWComponent.Mobo
{
    public class ChipsetDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [DisplayName("Platform")]
        public required string SocketName { get; set; }
        [Required]
        [DisplayName("Platform")]
        public required int SocketId { get; set; }
        [DisplayName("Supported Memory")]
        public required string RamTypeName { get; set; }
        [Required]
        [DisplayName("Supported Memory")]
        public required int RamTypeId { get; set; }
        [StringLength(200)]
        public string? ShortDesc { get; set; }
    }
}
