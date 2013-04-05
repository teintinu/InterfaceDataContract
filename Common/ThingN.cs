using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ThingN: BaseForThings
    {
        Int32 Id { get; set; }
        String Name { get; set; }
    }
}
