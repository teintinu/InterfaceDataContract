using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MyServiceImplementation : MyProxyProvider, MyService
    {
        public Thing2 CopyThing(Thing1 input)
        {
            Thing2 output = New<Thing2>();
            output.Id = input.Id + 1;
            output.Name = input.Name + " (copy)";
            output.AnotherThing = input;
            return output;
        }

        public override void RegisterProxies()
        {
            MapProxy<Thing1, Thing1ProxyAtServer>();
            MapProxy<Thing2, Thing2ProxyAtServer>();
            MapProxy<ThingN, ThingNProxyAtServer>();
        }
    }
}
