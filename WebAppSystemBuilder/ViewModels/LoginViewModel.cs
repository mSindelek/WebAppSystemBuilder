namespace WebAppSystemBuilder.ViewModels {
    public class LoginViewModel {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool Remember { get; set; }
    }
}
