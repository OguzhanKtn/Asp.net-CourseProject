namespace Udemy.Web.Models.Services
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public bool IsFail => Errors is not null && Errors.Count > 0;
        public bool IsSuccess => !IsFail;

        public static ServiceResult<T> Success(T data) => new ServiceResult<T> { Data = data };
        public static ServiceResult<T> Success(T data, string message) => new ServiceResult<T> { Data = data };
        public static ServiceResult<T> Error(string error) => new ServiceResult<T> { Errors = new List<string> { error } };
        public static ServiceResult<T> Error(List<string> errors) => new ServiceResult<T> { Errors = errors };
    }
    public class ServiceResult
    {
        public List<string>? Errors { get; set; }
        public bool IsFail => Errors is not null && Errors.Count > 0;
        public bool IsSuccess => !IsFail;
        public static ServiceResult Success(string message) => new ServiceResult { };
        public static ServiceResult Error(string error) => new ServiceResult { Errors = new List<string> { error } };
        public static ServiceResult Error(List<string> errors) => new ServiceResult { Errors = errors };
    }
}

