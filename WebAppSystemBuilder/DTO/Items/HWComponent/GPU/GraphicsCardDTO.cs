using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.DTO.Items.HWComponent.GPU {
    // this should be used for listing individual GPU products 
    public class GraphicsCardDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [DisplayName("Based on")]
        public required string BaseModelName { get; set; }
        [Required]
        [DisplayName("Based on")]
        public required int GPUBaseModelId { get; set; }
        public string? ShortDesc { get; set; }
        public string? Description { get; set; }
    }
}
