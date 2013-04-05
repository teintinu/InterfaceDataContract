using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Common
{
    //http://msdn.microsoft.com/en-us/library/ee358759.aspx    

    public sealed class MyDataContractResolver : DataContractResolver
    {

        public MyProxyProvider ProxyProvider;

        public MyDataContractResolver(MyProxyProvider ProxyProvider)
        {
            this.ProxyProvider = ProxyProvider;
        }

        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
        {
            if (typeNamespace.StartsWith(MyProxyProvider.MyNamespace))
                return ProxyProvider.GetProxyClass(declaredType);
            return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null);
        }

        public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
        {
            if (type.GetInterfaces().Contains(typeof(BaseForThings)))
            {
                XmlDictionary dictionary = new XmlDictionary();
                Type intf = ProxyProvider.GetProxedInterface(type);
                typeName = dictionary.Add(intf.Name);
                typeNamespace = dictionary.Add(MyProxyProvider.MyNamespace + intf.Namespace.Replace('.', '/'));
                return true;
            }
            return knownTypeResolver.TryResolveType(type, declaredType, null, out typeName, out typeNamespace);
        }
    }
}
