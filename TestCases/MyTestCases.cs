using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using Client;
using Common;
using System.Threading;

namespace TestCases
{
    [TestClass]
    public class MyTestCases
    {
        [AssemblyInitialize]
        public static void TestInitialize(TestContext pContext)
        {
            XHelper.ClientTypes = new Type[] { typeof(Thing1ProxyAtClient), typeof(Thing2ProxyAtClient), typeof(Thing2ProxyAtClient) };
            XHelper.ServerTypes = new Type[] { typeof(Thing1ProxyAtServer), typeof(Thing2ProxyAtServer), typeof(ThingNProxyAtServer) };
        }

        [TestMethod]
        public void Simple()
        {
            using (MyServer server = MyServer.CreateSimpleServer("127.0.0.1", "8088"))
            {
                Thread.Sleep(500);

                MyClient client = MyClient.CreateSimpleClient("127.0.0.1", "8088");

                Thing1 input = client.New<Thing1>();
                input.Id = 1;
                input.Name = "Thing";

                MyService service = client.Invoke<MyService>();
                Thing2 output = service.CopyThing(input);

                Assert.IsNotNull(output);
                Assert.AreEqual(input.Id + 1, output.Id);
                Assert.AreEqual(input.Name + " (copy)", output.Name);

                Assert.IsNotNull(output.AnotherThing);
                Assert.AreEqual(input.Id, output.AnotherThing.Id);
                Assert.AreEqual(input.Name, output.AnotherThing.Name);

                Assert.IsNull(output.AnotherThing.AnotherThing);

            }
        }

        [TestMethod]
        public void WithDataContractResolver()
        {
            // that test works but wsdl is wrong
            using (MyServer server = MyServer.CreateServerWithDataContractResolver("127.0.0.1", "8088"))
            {
                Thread.Sleep(500);
                MyClient client = MyClient.CreateServerWithDataContractResolver("127.0.0.1", "8088");

                Thing1 input = client.New<Thing1>();
                input.Id = 1;
                input.Name = "Thing";

                MyService service = client.Invoke<MyService>();
                Thing2 output = service.CopyThing(input);

                Assert.IsNotNull(output);
                Assert.AreEqual(input.Id + 1, output.Id);
                Assert.AreEqual(input.Name + " (copy)", output.Name);

                Assert.IsNotNull(output.AnotherThing);
                Assert.AreEqual(input.Id, output.AnotherThing.Id);
                Assert.AreEqual(input.Name, output.AnotherThing.Name);

                Assert.IsNull(output.AnotherThing.AnotherThing);
            }
        }

        [TestMethod]
        public void WithDataContractSerializerOperationBehavior()
        {
            // that test works but wsdl is wrong
            using (MyServer server = MyServer.CreateServerWithDataContractSerializerOperationBehavior("127.0.0.1", "8088"))
            {
                Thread.Sleep(500);
                MyClient client = MyClient.CreateServerWithDataContractSerializerOperationBehavior("127.0.0.1", "8088");

                Thing1 input = client.New<Thing1>();
                input.Id = 1;
                input.Name = "Thing";

                MyService service = client.Invoke<MyService>();
                Thing2 output = service.CopyThing(input);

                Assert.IsNotNull(output);
                Assert.AreEqual(input.Id + 1, output.Id);
                Assert.AreEqual(input.Name + " (copy)", output.Name);

                Assert.IsNotNull(output.AnotherThing);
                Assert.AreEqual(input.Id, output.AnotherThing.Id);
                Assert.AreEqual(input.Name, output.AnotherThing.Name);

                Assert.IsNull(output.AnotherThing.AnotherThing);
            }
        }
    }
}
