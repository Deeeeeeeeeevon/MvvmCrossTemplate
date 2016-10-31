using System;
using System.Collections.Generic;
using MvvmCrossTemplate.Core.Tests.Builders.Base;
using MvvmCrossTemplate.Core.Utils;
using MvvmCrossTemplate.Core.Utils.Enums;

namespace MvvmCrossTemplate.Core.Tests.Builders.Utils
{
    public class ErrorBuilder : BaseBuilder<Error>
    {
        private Exception _exception;
        private readonly Dictionary<string, object> _errorData = new Dictionary<string, object>();
        private string _class = "";
        private string _method = "";
        private ErrorType _errorType = ErrorType.Unspecified;

        public override Error Create()
        {
            var error = Error.Create(this, _errorType, _exception);
            error.AdditionalData = _errorData;
            if (_class != "") error.ClassName = _class;
            if (_method != "") error.MethodName = _method;
            return error;
        }

        public ErrorBuilder With_ErrorType(ErrorType errorType)
        {
            _errorType = errorType;
            return this;
        }
        public ErrorBuilder With_Exception(Exception exception)
        {
            _exception = exception;
            return this;
        }

        public ErrorBuilder With_ErrorData(string key, object value)
        {
            _errorData.Add(key, value);
            return this;
        }

        public ErrorBuilder With_Class(string myclassname)
        {
            _class = myclassname;
            return this;
        }

        public ErrorBuilder With_Method(string mymethodname)
        {
            _method = mymethodname;
            return this;
        }

    }
}