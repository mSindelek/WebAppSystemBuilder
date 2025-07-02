
using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.DTO.Items.HWComponent.GPU
{
    // this should be used for describtion of GPU base models
    public class GraphicsBaseDTO
    {
        public int Id { get; set; }
        [StringLength(50)]
        public required string Name { get; set; }
        public int TDP { get; set; }
        [StringLength(200)]
        public string? ShortDesc { get; set; }
    }
}
