namespace Udemy.Web.Models.Services.ViewModels.Auth
{
    public class SignInViewModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public bool RememberMe { get; set; }
    }
}
