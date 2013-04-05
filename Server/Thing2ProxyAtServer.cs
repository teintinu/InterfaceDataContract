using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Server
{
    [DataContract(Namespace = MyProxyProvider.MyNamespace, Name = "Thing2", IsReference = false)]
    internal class Thing2ProxyAtServer : Thing2 // MongoDB (or another) persistence
    {
        BsonDocument bsonDoc = new BsonDocument();

        [DataMember(Name="Id")]
        public Int32 Id
        {
            get
            {
                return (Int32)bsonDoc.GetValue("_Id");
            }
            set
            {
                bsonDoc.SetValue("_Id", value);
            }
        }

        [DataMember(Name="Name")]
        public String Name
        {
            get
            {
                return (String)bsonDoc.GetValue("Name");
            }
            set
            {
                bsonDoc.SetValue("Name", value);
            }
        }

        [DataMember(Name="AnotherThing")]
        public Thing1 AnotherThing
        {
            get
            {
                return (Thing1)bsonDoc.GetValue("AnotherThing");
            }
            set
            {
                bsonDoc.SetValue("AnotherThing", value);
            }
        }
    }
}
