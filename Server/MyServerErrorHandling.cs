using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Server
{
    public class MyServerErrorHandling : IServiceBehavior
    {
        public event Action<Exception> OnException;

        public void Validate(ServiceDescription pServiceDescription, ServiceHostBase pServiceHost)
        {
        }

        public void AddBindingParameters(ServiceDescription pServiceDescription, ServiceHostBase pServiceHostBase, Collection<ServiceEndpoint> pEndPoint, BindingParameterCollection pBindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            MyErrorHandler h = new MyErrorHandler();
            h.OnException += OnException;
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
                dispatcher.ErrorHandlers.Add(h);
        }
    }

    public class MyErrorHandler : IErrorHandler
    {
        public event Action<Exception> OnException;

        public void ProvideFault(Exception pErro, MessageVersion pVersao, ref Message pCulpa)
        {
        }

        public Boolean HandleError(Exception pErro)
        {
            if (OnException != null)
                OnException(pErro);
            return false;
        }
    }
}