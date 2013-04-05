using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    public class MyDataContractSerializerBehavior
         : DataContractSerializerOperationBehavior
    {
        public readonly MyProxyProvider ProxyProvider;
        public MyDataContractSerializerBehavior(
                OperationDescription description, MyProxyProvider ProxyProvider)
            : base(description)
        {
            this.ProxyProvider = ProxyProvider;
        }

        public override XmlObjectSerializer CreateSerializer(Type type,
                string name, string ns, IList<Type> knownTypes)
        {
            if (type.GetInterfaces().Contains(typeof(BaseForThings)))
            {
                type = ProxyProvider.GetProxyClass(type);
                knownTypes = ProxyProvider.KnownTypes();
            }
            return new DataContractSerializer(type, knownTypes);
        }

        public override XmlObjectSerializer CreateSerializer(Type type,
                XmlDictionaryString name, XmlDictionaryString ns,
                IList<Type> knownTypes)
        {
            if (type.GetInterfaces().Contains(typeof(BaseForThings)))
            {
                type = ProxyProvider.GetProxyClass(type);
                knownTypes = ProxyProvider.KnownTypes();
            }
            return new DataContractSerializer(type, name, ns, knownTypes, 10, false, false, DataContractSurrogate, DataContractResolver);
        }
    }
}
