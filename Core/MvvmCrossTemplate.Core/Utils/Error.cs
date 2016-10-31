using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MvvmCrossTemplate.Core.Utils.Enums;

namespace MvvmCrossTemplate.Core.Utils
{
    public class Error
    {
        protected Error(object sender, ErrorType errorType, Exception exception, [CallerMemberName] string methodName = "")
        {
            ErrorType = errorType;
            MethodName = methodName;
            ClassName = sender.GetType().Name;
            Exception = exception;
        }

        public static Error Create(object sender, ErrorType type, Exception exception = null, [CallerMemberName] string methodName = "")
        {
            return new Error(sender, type, exception)
            {
                MethodName = methodName
            };
        }

        public static Error Update(object sender, Error previousError, string methodName = "")
        {
            return new Error(sender, previousError.ErrorType, null)
            {
                MethodName = methodName,
                PreviousError = previousError
            };
        }

        public Error AddData(string key, string value)
        {
            if (!AdditionalData.ContainsKey(key))
                AdditionalData.Add(key, value);
            else
                AdditionalData.Add(key + (AdditionalData.Keys.Count(x => x == key) + 1), value);
            return this;
        }

        public Error PreviousError { get; protected set; }
        public Error SourceError => ErrorStack.LastOrDefault();
        public List<Error> ErrorStack => GetErrorStack(new List<Error> { this });
        public string ErrorStackString => GetErrorStackString();

        private List<Error> GetErrorStack(List<Error> errors)
        {
            if (PreviousError == null) return errors;
            errors.Add(PreviousError);
            PreviousError.GetErrorStack(errors);
            return errors;
        }

        public ErrorType ErrorType { get; set; }
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public Exception Exception { get; }
        public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
        public string ErrorDescriptionString => GetErrorDescriptionString();

        public object ViewModelParameters => new
        {
            errorType = ErrorType.ToString(),
            description = ErrorDescriptionString,
            exceptionMessage = SourceError.Exception != null ? SourceError.Exception.Message : "",
            exceptionStackTrace = SourceError.Exception != null ? SourceError.Exception.StackTrace : "",
            innerExceptionMessage = SourceError.Exception != null && SourceError.Exception.InnerException != null ? SourceError.Exception.InnerException.Message : "",
            errorStack = GetErrorStackString()
        };

        private string GetErrorStackString()
        {
            return ErrorStack.Aggregate("", (current, error) => current + $"{error.ClassName}.{error.MethodName}()\n");
        }

        private string GetErrorDescriptionString()
        {
            return "";
        }
    }
}