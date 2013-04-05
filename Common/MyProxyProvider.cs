using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class MyProxyProvider
    {

        private Dictionary<Type, Type> proxyMapping = new Dictionary<Type, Type>();

        public MyProxyProvider()
        {
            RegisterProxies();
        }

        public abstract void RegisterProxies();
        protected void MapProxy<INTERFACE, PROXYCLASS>()
        {
            proxyMapping[typeof(INTERFACE)] = typeof(PROXYCLASS);
        }

        public const String MyNamespace = "http://www.MyNamespace.com/";

        public DATA New<DATA>() where DATA : BaseForThings
        {
            Type impl = GetProxyClass(typeof(DATA));
            Object o = impl.GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
            return (DATA)o;
        }

        public Type GetProxyClass(Type Interface)
        {
            return proxyMapping[Interface];
        }

        public Type GetProxedInterface(Type Implementation)
        {
            return proxyMapping.Single(m => m.Value == Implementation).Key;
        }

        public IList<Type> KnownTypes()
        {
            return proxyMapping.Values.ToList();
        }
    }
}
