using Microsoft.AspNetCore.Identity;
using System.Collections;
using Udemy.Web.Models.Repository.Entities;
using Udemy.Web.Models.Services.ViewModels.Auth;

namespace Udemy.Web.Models.Services
{
    public class ServiceHelper
    {
        public static ServiceResult<TResult> CheckIfNullOrNot<T, TResult>(
           T data,
           Func<T, TResult> processFunc
           )
        {
            if (data == null || (data is ICollection collection && collection.Count == 0))
            {
                return ServiceResult<TResult>.Error("Not found !");
            }

            var result = processFunc(data);
            return ServiceResult<TResult>.Success(result);
        }

        public static ServiceResult<TResult> CheckIfNullOrNot<T, TResult>(
          T data,
          TResult result
          )
        {
            if (data == null || (data is ICollection collection && collection.Count == 0))
            {
                return ServiceResult<TResult>.Error("Not found !");
            }

            return ServiceResult<TResult>.Success(result);
        }

        public static ServiceResult SuccessOrFail(IdentityResult result,string message)
        {
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return ServiceResult.Error(errors);
            }
            return ServiceResult.Success(message);
        }

        public static async Task<ServiceResult> LoginProcess(AppUser user,bool passwordCheck,SignInManager<AppUser> manager, SignInViewModel model)
        {
            if(user is null)
            {
                return ServiceResult.Error("Email or Password is wrong!");
            }
            if (!passwordCheck) 
            {
                return ServiceResult.Error("Email or Password is wrong!");
            }
            await manager.SignInAsync(user, model.RememberMe);

            return ServiceResult.Success("User signed in successfully!");
        }
    }
}
