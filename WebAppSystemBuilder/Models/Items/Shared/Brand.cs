using System.ComponentModel.DataAnnotations;

// curently unused

namespace WebAppSystemBuilder.Models.Items.Shared
{
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        public string? ShortDesc { get; set; }
    }
}
