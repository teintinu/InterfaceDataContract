using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MyServer : IDisposable
    {
        private MyServer(Type service, Object implementation, String host, String port)
        {
            String uri = "http://" + host + ":" + port + "/" + service.FullName.Replace('.', '/');

            servicehost = new ServiceHost(implementation, new Uri(uri));
            servicehost.Description.Behaviors.Find<ServiceBehaviorAttribute>().InstanceContextMode = InstanceContextMode.Single;
            servicehost.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;

            MyServerErrorHandling errorHandling = new MyServerErrorHandling();
            errorHandling.OnException += OnException;
            servicehost.Description.Behaviors.Add(errorHandling);

            endpoint = new ServiceEndpoint(ContractDescription.GetContract(service, implementation), new WSHttpBinding(), new EndpointAddress(uri));
            servicehost.AddServiceEndpoint(endpoint);

            ServiceMetadataBehavior wsdl = new ServiceMetadataBehavior();
            wsdl.HttpGetEnabled = true;
            wsdl.MetadataExporter.PolicyVersion = PolicyVersion.Default;
            servicehost.Description.Behaviors.Add(wsdl);
        }

        private void OnException(Exception obj)
        {
            throw new NotImplementedException(obj.Message);
        }

        private ServiceHost servicehost;
        private ServiceEndpoint endpoint;

        public static MyServer CreateSimpleServer(String host, String port)
        {
            MyServiceImplementation impl = new MyServiceImplementation();
            MyServer srv = new MyServer(typeof(MyService), impl, host, port);
            srv.servicehost.Open();
            return srv;
        }

        public static MyServer CreateServerWithDataContractResolver(String host, String port)
        {
            MyServiceImplementation impl = new MyServiceImplementation();
            MyServer srv = new MyServer(typeof(MyService), impl, host, port);
            foreach (var op in srv.endpoint.Contract.Operations)
            {
                var sb = op.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>();
                sb.DataContractResolver = new MyDataContractResolver(impl);
            }
            srv.servicehost.Open();
            return srv;
        }

        public static MyServer CreateServerWithDataContractSerializerOperationBehavior(String host, String port)
        {
            MyServiceImplementation impl = new MyServiceImplementation();
            MyServer srv = new MyServer(typeof(MyService), impl, host, port);
            foreach (var op in srv.endpoint.Contract.Operations)
            {
                var sb = op.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>();
                if (sb != null)
                    op.Behaviors.Remove(sb);
                sb = new MyDataContractSerializerBehavior(op, impl);
                sb.DataContractResolver = new MyDataContractResolver(impl);
                op.Behaviors.Add(sb);
            }
            srv.servicehost.Open();
            return srv;
        }
        public void Dispose()
        {
            servicehost.Close();
        }
    }
}
