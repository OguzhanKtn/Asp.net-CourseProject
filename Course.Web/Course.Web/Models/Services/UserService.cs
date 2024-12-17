using Microsoft.AspNetCore.Identity;
using Udemy.Web.Models.Repository.Entities;
using Udemy.Web.Models.Services.ViewModels.Auth;

namespace Udemy.Web.Models.Services
{
    public class UserService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
    {
        public async Task<ServiceResult> SignUpAsync(SignUpViewModel model)
        {
            var randomUserName = $"{model.FirstName.First()}{model.LastName}_{new Random().Next(1,1000)}";

            var newUser = new AppUser()
            {
                UserName = randomUserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,      
            };

            var result = await userManager.CreateAsync(newUser,model.Password);

            return ServiceHelper.SuccessOrFail(result,"User created successfully!");
        }

        public async Task<ServiceResult> SignInAsync(SignInViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var passwordCheck = await userManager.CheckPasswordAsync(user,model.Password);
           
            return await ServiceHelper.LoginProcess(user, passwordCheck,signInManager,model);
        }
    }
}
