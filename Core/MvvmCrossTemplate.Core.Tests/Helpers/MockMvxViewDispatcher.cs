using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform.Core;

namespace MvvmCrossTemplate.Core.Tests.Helpers
{
    public class MockMvxViewDispatcher : MvxMainThreadDispatcher, IMvxViewDispatcher
    {
        public readonly List<MvxViewModelRequest> Requests = new List<MvxViewModelRequest>();
        public readonly List<MvxPresentationHint> Hints = new List<MvxPresentationHint>();

        public bool RequestMainThreadAction(Action action)
        {
            action();
            return true;
        }

        public bool ShowViewModel(MvxViewModelRequest request)
        {
            Requests.Add(request);
            return true;
        }

        public bool ChangePresentation(MvxPresentationHint hint)
        {
            Hints.Add(hint);
            return true;
        }

        public bool ViewModelWasCalled(Type viewModelType)
        {
            if (Requests.Count == 0) return false;
            return Requests[0].ViewModelType == viewModelType;
        }

        public bool ViewModelWasCalledWithParameter(Type viewModelType, string parameter)
        {
            return Requests[0].ViewModelType == viewModelType
                && Requests[0].ParameterValues["parameter"] == parameter;
        }
    }

}