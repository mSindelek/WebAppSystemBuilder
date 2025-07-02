using System.ComponentModel.DataAnnotations;

namespace WebAppSystemBuilder.ViewModels {
    public class UserViewModel {
        public required string Name { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
