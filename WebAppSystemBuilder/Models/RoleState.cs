using Microsoft.AspNetCore.Identity;

namespace WebAppSystemBuilder.Models {
    public class RoleState {
        public required IdentityRole Role { get; set; }

        public required IEnumerable<AppUser> Members { get; set; }
        public required IEnumerable<AppUser> NonMembers { get; set; }


    }
}
