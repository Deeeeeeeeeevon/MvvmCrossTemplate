using MvvmCrossTemplate.Core.Extensions;
using MvvmCrossTemplate.Core.Utils.Enums;

namespace MvvmCrossTemplate.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    namespace Onsight.Core.Utils
    {
        public static class Try
        {
            public static Result Action(Action operation, ErrorType error = ErrorType.Unspecified)
            {
                Result result;
                try
                {
                    operation();
                    result = Result.Ok();
                }
                catch (Exception e)
                {
                    return Result.Fail(operation, error, e);
                }
                return result;
            }
            public static Result<T> Action<T>(Func<T> operation, ErrorType error = ErrorType.Unspecified)
            {
                Result<T> result;
                try
                {
                    var res = operation();
                    result = Result.Ok(res);
                }
                catch (Exception e)
                {
                    return Result.Fail<T>(operation, error, e);
                }
                return result;
            }
            public static async Task<Result> Action(Func<Task> operation, ErrorType error = ErrorType.Unspecified)
            {
                Result result;
                try
                {
                    await operation();
                    result = Result.Ok();
                }
                catch (Exception e)
                {
                    return Result.Fail(operation, error, e);
                }
                return result;
            }
            public static async Task<Result<T>> Action<T>(Func<Task<T>> operation, ErrorType error = ErrorType.Unspecified)
            {
                Result<T> result;
                try
                {
                    var value = await operation();
                    result = Result.Ok(value);
                }
                catch (Exception e)
                {
                    return Result.Fail<T>(operation, error, e);
                }
                return result;
            }
            /// <summary>
            /// Try operation at most n times with delay in between until an exception is not thrown
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="operation"></param>
            /// <param name="n"></param>
            /// <param name="delayInMillis"></param>
            /// <param name="error"></param>
            /// <returns></returns>
            public static async Task<Result<T>> ActionNTimesWithWait<T>(Func<Task<T>> operation, int n, int delayInMillis = 0, ErrorType error = ErrorType.Unspecified)
            {
                List<Exception> exceptions = new List<Exception>();
                for (int i = 0; i < n; i++)
                {
                    try
                    {
                        var opResult = await operation();
                        return Result.Ok(opResult);
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }

                    await Task.Delay(delayInMillis);
                }
                Result<T> result = Result.Fail<T>(operation, error, new AggregateException(exceptions).Flatten());

                return result.AddData("tries", $"Tried operation {n} times");
            }


        }
    }

}