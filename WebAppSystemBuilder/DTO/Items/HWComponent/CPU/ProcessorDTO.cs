using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.DTO.Items.HWComponent.CPU {
    public class ProcessorDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [DisplayName("Platform")]
        public required string SocketName{ get; set; }
        [Required]
        [DisplayName("Platform")]
        public required int SocketId { get; set; }
        public int TDP { get; set; }
        [StringLength(200)]
        public string? ShortDesc { get; set; }
        public string? Description { get; set; }
    }
}
