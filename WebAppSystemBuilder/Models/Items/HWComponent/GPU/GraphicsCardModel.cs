using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.Models.Items.HWComponent.GPU
{
    // this should be used for listing individual GPU products 
    public class GraphicsCardModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required]
        public required GraphicsBaseModel GPUBaseModel { get; set; }
        public string? Description { get; set; }
    }
}
