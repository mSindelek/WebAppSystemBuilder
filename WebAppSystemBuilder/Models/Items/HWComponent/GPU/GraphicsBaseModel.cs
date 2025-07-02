using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.Models.Items.HWComponent.GPU
{
    // this should be used for describtion of GPU base models
    public class GraphicsBaseModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        public int TDP { get; set; }
        [StringLength(200)]
        public string? ShortDesc { get; set; }
    }
}
