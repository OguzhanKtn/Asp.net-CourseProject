using MassTransit;
using Microsoft.AspNetCore.Identity;
using Shared.Events;
using Udemy.Web.Caching;
using Udemy.Web.Models.Repository.Entities;
using Udemy.Web.Models.Services.ViewModels.Auth;

namespace Udemy.Web.Models.Services
{
    public class UserService(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IPublishEndpoint publishEndpoint,ICacheService cacheService)
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

            if (result.Succeeded) 
            {
                var cacheKey = $"Discount:{newUser.Id}";

                var discountCode = $"WELCOME{new Random().Next(1, 1000)}";

                await cacheService.Set(cacheKey, discountCode);

                await SendUserCreatedEvent(model, discountCode);
            }

            return ServiceHelper.SuccessOrFail(result,"User created successfully!");
        }

        public async Task<ServiceResult> SignInAsync(SignInViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var passwordCheck = await userManager.CheckPasswordAsync(user,model.Password);
           
            return await ServiceHelper.LoginProcess(user, passwordCheck,signInManager,model);
        }

        private async Task SendUserCreatedEvent(SignUpViewModel model,string discountCode)
        {
            var userCreatedEvent = new UserCreatedEvent(model.FirstName,model.LastName,model.Email,discountCode);

            await publishEndpoint.Publish(userCreatedEvent);
        }
    }
}
