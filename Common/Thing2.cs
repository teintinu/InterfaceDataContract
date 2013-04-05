using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface Thing2: BaseForThings
    {
        Int32 Id { get; set; }
        String Name { get; set; }
        Thing1 AnotherThing { get; set; }
    }
}
