using System;
using System.Threading.Tasks;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;

namespace MvvmCrossTemplate.Core.Extensions
{
    public static class ResultExtensions
    {

        public static Result AddData(this Result result, string key, string value)
        {
            result.Error.AddData(key, value);
            return result;
        }

        public static Result<T> AddData<T>(this Result<T> result, string key, string value)
        {
            result.Error.AddData(key, value);
            return result;
        }

        #region Continuation Methods
        public static Result OnComplete(this Result result, Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                return Result.Fail(action, ErrorType.Unspecified, e);
            }
            return result;
        }
        public static Result OnComplete(this Result result, Func<Result> next)
        {
            Result res;
            try
            {
                res = next();
            }
            catch (Exception e)
            {
                res = Result.Fail(result, ErrorType.Unspecified, e);
            }
            return Result.Combine(result, res);
        }

        public static Result<T> OnComplete<T>(this Result result, Func<Result<T>> next)
        {
            Result<T> res;
            try
            {
                res = next();
            }
            catch (Exception e)
            {
                res = Result.Fail<T>(result, ErrorType.Unspecified, e);
            }
            if (result.IsFailure) return Result.Fail<T>(result, ErrorType.Unspecified);
            return res;
        }

        public static async Task<Result> OnComplete(this Result result, Func<Task<Result>> next)
        {
            Result res;
            try
            {
                res = await next();
            }
            catch (Exception e)
            {
                res = Result.Fail(result, ErrorType.Unspecified, e);
            }
            if (result.IsFailure) return Result.Fail(result, ErrorType.Unspecified);
            return res;
        }

        public static async Task<Result<T>> OnComplete<T>(this Result result, Func<Task<Result<T>>> next)
        {
            Result<T> res;
            try
            {
                res = await next();
            }
            catch (Exception e)
            {
                res = Result.Fail<T>(result, ErrorType.Unspecified, e);
            }
            if (result.IsFailure) return Result.Fail<T>(result, ErrorType.Unspecified);
            return res;
        }
        public static async Task<Result> OnComplete(this Task<Result> resultTask, Action<Result> next,
            TaskContinuationOptions option = TaskContinuationOptions.None)
        {
            Result res;
            try
            {
                res = await resultTask.ContinueWith(antecedent => {
                    next.Invoke(antecedent.Result);
                    return antecedent.Result;
                }, option);
            }
            catch (Exception e)
            {
                res = Result.Fail(resultTask, ErrorType.Unspecified, e);
            }
            return res;
        }
        public static async Task<Result> OnComplete(this Task<Result> resultTask, Task<Result> next,
            TaskContinuationOptions option = TaskContinuationOptions.None)
        {
            Task<Result> res;
            try
            {
                res = await resultTask.ContinueWith(antecedent => next, option);
            }
            catch (Exception e)
            {
                res = Task.FromResult(Result.Fail(resultTask, ErrorType.Unspecified, e));
            }
            return await res;
        }

        public static async Task<Result<T>> OnComplete<T>(this Task<Result> result, Task<Result<T>> next,
            TaskContinuationOptions option = TaskContinuationOptions.None)
        {
            Task<Result<T>> res;
            try
            {
                res = await result.ContinueWith(antecedent => next, option);
            }
            catch (Exception e)
            {
                res = Task.FromResult(Result.Fail<T>(result, ErrorType.Unspecified, e));
            }
            return await res;
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            try
            {
                if (result.IsSuccess) action();
            }
            catch (Exception e)
            {
                return Result.Fail(action.GetType().Name, ErrorType.Unspecified, e);
            }

            return result;
        }

        public static async Task<Result> OnSuccess(this Task<Result> resultTask, Action<Result> success)
        {
            Result res;
            try
            {
                res = await resultTask;
            }
            catch (Exception e)
            {
                res = Result.Fail(typeof(ResultExtensions), ErrorType.Unspecified, e);
            }
            if (res.IsSuccess)
                success(res);
            return res;
        }

        public static async Task<Result<T>> OnSuccess<T>(this Task<Result<T>> resultTask, Action<Result<T>> success)
        {
            Result<T> res;
            try
            {
                res = await resultTask;
            }
            catch (Exception e)
            {
                res = Result.Fail<T>(typeof(ResultExtensions), ErrorType.Unspecified, e);
            }
            if (res.IsSuccess)
                success(res);
            return res;
        }

        public static Result OnFailure(this Result result, Action action)
        {
            try
            {
                if (result.IsFailure) action();
            }
            catch (Exception e)
            {
                result.AddData("OnFailureAction", e.Message);
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action<Result> failure)
        {
            try
            {
                if (result.IsFailure) failure(result);
            }
            catch (Exception e)
            {
                result.AddData("OnFailureAction", e.Message);
            }
            return result;
        }
        public static Result<T> OnFailure<T>(this Result<T> result, Action<Result<T>> failure)
        {
            try
            {
                if (result.IsFailure) failure(result);
            }
            catch (Exception e)
            {
                result.AddData("OnFailureAction", e.Message);
            }
            return result;
        }

        public static async Task<Result> OnFailure(this Task<Result> resultTask, Action<Result> failure)
        {
            Result res;
            try
            {
                res = await resultTask;
            }
            catch (Exception e)
            {
                res = Result.Fail(typeof(ResultExtensions), ErrorType.Unspecified, e);
            }
            if (res.IsFailure)
                failure(res);
            return res;
        }

        public static Result OnComplete(this Result result, Action success, Action failure)
        {
            try
            {
                if (result.IsSuccess) success();
                else failure();
            }
            catch (Exception e)
            {
                var key = result.IsSuccess ? "OnSuccessAction" : "OnFailureAction";
                result.AddData(key, e.Message);
            }

            return result;
        }

        public static Result OnComplete(this Result result, Action<Result> success, Action<Result> failure)
        {
            try
            {
                if (result.IsSuccess) success(result);
                else failure(result);
            }
            catch (Exception e)
            {
                var key = result.IsSuccess ? "OnSuccessAction" : "OnFailureAction";
                result.AddData(key, e.Message);
            }

            return result;
        }

        public static T OnComplete<T>(this Result result, Func<Result, T> success, Func<Result, T> failure)
        {
            T res = default(T);
            try
            {
                res = result.IsSuccess ? success(result) : failure(result);
            }
            catch (Exception e)
            {
                var key = result.IsSuccess ? "OnSuccessAction" : "OnFailureAction";
                result.AddData(key, e.Message);
            }

            return res;
        }

        #endregion


    }
}