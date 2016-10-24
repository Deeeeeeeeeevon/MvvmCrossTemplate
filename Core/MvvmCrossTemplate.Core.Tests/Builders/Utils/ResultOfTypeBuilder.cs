using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;

namespace MvvmCrossTemplate.Core.Tests.Builders.Utils
{
    public class ResultOfTypeBuilder<T> : BaseBuilder<Result<T>>
    {
        private T _value;
        private bool _isSuccess;
        private Error _error;

        public ResultOfTypeBuilder()
        {
            _error = Error.Create(this, ErrorType.Unspecified);
        }

        public override Result<T> Create()
        {
            return new Result<T>(_value, _isSuccess, _error);
        }

        public ResultOfTypeBuilder<T> With_IsSuccess(bool isSuccess)
        {
            _isSuccess = isSuccess;
            return this;
        }

        public ResultOfTypeBuilder<T> With_Error(Error errorMessage)
        {
            _isSuccess = false;
            _error = errorMessage;
            return this;
        }

        public ResultOfTypeBuilder<T> With_Value(T value)
        {
            _isSuccess = true;
            _value = value;
            return this;
        }

        public ResultOfTypeBuilder<T> With_Error_ClassName(string className)
        {
            _isSuccess = false;
            _error.ClassName = className;
            return this;
        }

        public ResultOfTypeBuilder<T> With_Error_Type(ErrorType errorType)
        {
            _isSuccess = false;
            _error.ErrorType = errorType;
            return this;
        }

        public ResultOfTypeBuilder<T> With_Sender(object sender)
        {
            _error.ClassName = sender.GetType().Name;
            return this;
        }
    }
}