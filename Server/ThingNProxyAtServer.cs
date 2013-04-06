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
    [DataContract(Namespace = MyProxyProvider.MyNamespace, Name = "ThingN", IsReference = false)]
    public class ThingNProxyAtServer : ThingN // MongoDB (or another) persistence
    {
        private BsonDocument _bsonDoc;

        private BsonDocument bsonDoc
        {
            get
            {
                if (_bsonDoc == null)
                    _bsonDoc = new BsonDocument();
                return _bsonDoc;
            }
        }

        [DataMember(Name = "Id")]
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

        [DataMember(Name = "Name")]
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
    }
}
