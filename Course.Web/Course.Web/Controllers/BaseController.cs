using Microsoft.AspNetCore.Mvc;
using Udemy.Web.Models.Services;

namespace Udemy.Web.Controllers
{
    public class BaseController : Controller
    {
        private void Error(string message)
        {
            TempData["status"] = false;
            TempData["errorMessage"] = message;
        }

        private void Success(string? message)
        {
            TempData["status"] = true;
            TempData["successMessage"] ??= message;
        } 
        private void Success()
        {
        }

        public void SuccessOrFail(ServiceResult result,string message) 
        {
            if (result.IsFail)
            {
                Error(result.Errors!.First());
            }
            else
            {
                Success(message);
            }
        }
        public void SuccessOrFail<T>(ServiceResult<T> result)
        {
            if (result.IsFail)
            {
                Error(result.Errors!.First());
            }
            else
            {
                Success();
            }
        }
    }
}
