using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.Models.Items.HWComponent.Mobo
{
    public class MotherboardModel 
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required]
        public required ChipsetModel Chipset { get; set; }
        public string? Description { get; set; }
        

    }
}
