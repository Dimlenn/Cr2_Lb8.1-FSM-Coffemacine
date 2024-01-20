using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    public class Transition<S,E>
        where S : Enum
        where E : Enum
    {
        public S FromState { get; set; }
        public S ToState { get; set; }
        public E Event { get; set; }

    }
}
