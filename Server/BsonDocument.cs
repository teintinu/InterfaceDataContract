using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{

    // emulation of MongoDB persistence for testing propose only
    public class BsonDocument
    {
        private Dictionary<String, Object> values = new Dictionary<string, object>();

        public Object GetValue(string name)
        {
            Object r;
            if (!values.TryGetValue(name, out r))
                return null;
            return r;
        }

        public void SetValue(string name, Object value)
        {
            values[name] = value;
        }
    }
}
