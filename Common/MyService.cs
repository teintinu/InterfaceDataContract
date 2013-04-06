using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract(Namespace = MyProxyProvider.MyNamespace)]
    [ServiceKnownType("GetKnownTypes", typeof(XHelper))]
    public interface MyService
    {
        [OperationContract]
        Thing2 CopyThing(Thing1 input);
    }
}
