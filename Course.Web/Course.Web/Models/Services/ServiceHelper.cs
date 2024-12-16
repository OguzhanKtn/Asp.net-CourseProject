using System.Collections;
using System.Net;

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
    }
}
