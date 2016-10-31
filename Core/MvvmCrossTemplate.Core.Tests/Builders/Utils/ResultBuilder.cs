using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;

namespace MvvmCrossTemplate.Core.Tests.Builders.Utils
{
    public class ResultBuilder : BaseBuilder<Result>
    {
        private bool _isSuccess;
        private Error _error;
        private object _sender;

        public ResultBuilder()
        {
            _error = Error.Create(this, ErrorType.Unspecified);
            _sender = this;
        }

        public override Result Create()
        {
            if (_isSuccess)
                return Result.Ok();
            return Result.Fail(_sender, _error.ErrorType);
        }
        public Result<T> Create<T>(T value)
        {
            if (_isSuccess)
                return Result.Ok(value);
            return Result.Fail<T>(_sender, _error.ErrorType);
        }
        public ResultBuilder With_IsSuccess(bool isSuccess)
        {
            _isSuccess = isSuccess;
            return this;
        }

        public ResultBuilder With_Error(Error errorMessage)
        {
            _error = errorMessage;
            return this;
        }

        public ResultBuilder With_Sender(object sender)
        {
            _sender = sender;
            return this;
        }

    }
}