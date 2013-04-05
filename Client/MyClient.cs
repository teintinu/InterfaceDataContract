using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class MyClient : MyProxyProvider
    {

        private String host;
        private String port;
        private int mode;

        private MyClient(String host, String port, int mode)
        {
            this.host = host;
            this.port = port;
            this.mode = mode;
        }

        public static MyClient CreateSimpleClient(String host, String port)
        {
            return new MyClient(host, port, 0);
        }

        public static MyClient CreateServerWithDataContractResolver(String host, String port)
        {
            return new MyClient(host, port, 1);
        }

        public static MyClient CreateServerWithDataContractSerializerOperationBehavior(String host, String port)
        {
            return new MyClient(host, port, 2);
        }

        public SERVICE Invoke<SERVICE>()
        {
            String uri = "http://" + host + ":" + port + "/" + typeof(SERVICE).FullName.Replace('.', '/');
            ChannelFactory<SERVICE> factory = new ChannelFactory<SERVICE>(new WSHttpBinding(), new EndpointAddress(uri));

            if (mode == 1)
                foreach (var op in factory.Endpoint.Contract.Operations)
                {
                    var sb = op.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>();
                    if (sb != null)
                        sb.DataContractResolver = new MyDataContractResolver(this);
                }
            else if (mode == 2)
                foreach (var op in factory.Endpoint.Contract.Operations)
                {
                    var sb = op.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>();
                    if (sb != null)
                        op.Behaviors.Remove(sb);
                    sb = new MyDataContractSerializerBehavior(op, this);
                    sb.DataContractResolver = new MyDataContractResolver(this);
                    op.Behaviors.Add(sb);
                }

            SERVICE proxy = factory.CreateChannel();
            return proxy;
        }

        public override void RegisterProxies()
        {
            MapProxy<Thing1, Thing1ProxyAtClient>();
            MapProxy<Thing2, Thing2ProxyAtClient>();
            MapProxy<ThingN, ThingNProxyAtClient>();
        }
    }
}
