using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.DTO.Items.HWComponent.Mobo
{
    public class MotherboardDTO
    {
        public int Id { get; set; }
        [StringLength(100)]
        public required string Name { get; set; }
        [DisplayName("Chipset")]
        public required string ChipsetName { get; set; }
        [DisplayName("Chipset")]
        public required int ChipsetId { get; set; }
        public string? SocketName { get; set; }
        public string? ShortDesc { get; set; }
        public string? Description { get; set; }
        

    }
}
