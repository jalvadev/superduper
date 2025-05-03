using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperDuper.Resources
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }
        public T Value { get; }

        private Result(bool success, string errorMessage, T value)
        {
            IsSuccess = success;
            ErrorMessage = errorMessage;
            Value = value;
        }

        public static Result<T> Success(T value) => new Result<T>(true, string.Empty, value);

        public static Result<T> Failure(string message) => new Result<T>(false, message, default);
    }
}
