using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MvvmCrossTemplate.Core.Utils.Enums;

namespace MvvmCrossTemplate.Core.Utils
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        protected static Result Fail(Error error)
        {
            return new Result(false, error);
        }

        protected static Result<T> Fail<T>(Error error)
        {
            return new Result<T>(default(T), false, error);
        }

        public static Result Return(object sender, Result previousResult)
        {
            return previousResult.IsSuccess
                ? Ok(previousResult)
                : Fail(sender, previousResult);
        }

        public static Result<T> Return<T>(object sender, Result<T> previousResult)
        {
            return previousResult.IsSuccess
                ? Ok(previousResult.Value)
                : Fail<T>(sender, previousResult);
        }


        public static Result Fail(object sender, Result previousResult, [CallerMemberName] string methodName = "")
        {
            return Fail(Error.Update(sender, previousResult.Error, methodName));
        }

        public static Result<T> Fail<T>(object sender, Result previousResult, [CallerMemberName] string methodName = "")
        {
            return Fail<T>(Error.Update(sender, previousResult.Error, methodName));
        }

        public static Result Fail(object sender, ErrorType errorType, Exception exception = null, [CallerMemberName] string methodName = "")
        {
            return Fail(Error.Create(sender, errorType, exception, methodName));
        }

        public static Result<T> Fail<T>(object sender, ErrorType errorType, Exception exception = null, [CallerMemberName] string methodName = "")
        {
            return Fail<T>(Error.Create(sender, errorType, exception, methodName));
        }

        public static Result Ok()
        {
            return new Result(true, null);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, null);
        }

        //Extension Methods
        /// <summary>
        /// Checks all values for failures
        /// </summary>
        /// <param name="results"></param>
        /// <returns>Success if all are success else fail with error updated with all failures in order received</returns>
        public static Result Combine(params Result[] results)
        {
            foreach (var result in results.Where(result => result.IsFailure))
            {
                return Fail(typeof(Result), result);
            }

            return Ok();
        }
        /// <summary>
        /// Checks all values for failures
        /// </summary>
        /// <param name="results"></param>
        /// <returns>Success if all are success else fail with error updated with all failures in order received</returns>
        public static Result Combine(IEnumerable<Result> results)
        {
            foreach (var result in results.Where(result => result.IsFailure))
            {
                return Fail(typeof(Result), result);
            }

            return Ok();
        }

        /// <summary>
        /// Checks all values for failures
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns>List of type values if all Succeed else Fail with error updated with all failures in order received</returns>
        public static Result<List<T>> Combine<T>(IEnumerable<Result<T>> results)
        {
            List<T> values = new List<T>();
            foreach (var result in results)
            {
                if (result.IsFailure)
                {
                    return Fail<List<T>>(typeof(Result), result);
                }
                values.Add(result.Value);
            }
            return Ok(values);
        }

    }

    public class Result<T> : Result
    {
        public T Value { get; }
        public Result(T value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            Value = value;
        }

    }

}