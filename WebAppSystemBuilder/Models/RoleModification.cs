using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.Models {
    public class RoleModification {
        [Required]
        public required string RoleName { get; set; }
        public required string RoleId { get; set; }
        public string[]? AddIds { get; set; }
        public string[]? DeleteIds { get; set; }



    }
}
