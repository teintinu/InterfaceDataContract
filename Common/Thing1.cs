using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface Thing1 : BaseForThings
    {
        Int32 Id { get; set; }
        String Name { get; set; }
        ThingN AnotherThing { get; set; }
    }
}
