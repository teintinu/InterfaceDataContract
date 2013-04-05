using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    [DataContract(Namespace = MyProxyProvider.MyNamespace, Name = "Thing1", IsReference=false)]
    internal class Thing1ProxyAtClient : Thing1, INotifyPropertyChanged // WPF bindings
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private Int32 id;
        private String name;
        private ThingN anotherThing;

        [DataMember(Name = "Id")]
        public Int32 Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }

        [DataMember(Name = "Name")]
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        [DataMember(Name = "AnotherThing")]
        public ThingN AnotherThing
        {
            get
            {
                return anotherThing;
            }
            set
            {
                anotherThing = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("AnotherThing"));
            }
        }
    }
}
