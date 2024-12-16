using Microsoft.AspNetCore.Identity;

namespace Udemy.Web.Models.Repository.Entities
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public string GetFullName => $"{FirstName} {LastName}";
    }
}
