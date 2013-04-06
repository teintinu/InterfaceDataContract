using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class XHelper
    {

        public static Boolean? IsClient = null;
        public static Type[] ClientTypes;
        public static Type[] ServerTypes;

        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider pProvider)
        {
            if (!IsClient.HasValue)
                throw new Exception("Invalid value");
            if (IsClient.Value)
                return ClientTypes;
            return ServerTypes;
        }
    }
}