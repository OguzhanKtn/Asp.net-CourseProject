

namespace Udemy.Web.Models.Services.ViewModels.User
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
