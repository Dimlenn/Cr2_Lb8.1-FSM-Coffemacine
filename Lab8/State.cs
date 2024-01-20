using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    internal class State<S> where S : Enum
    {
        public S Name { get; set; }

        public Action OnEnter;
        public Action OnExit;
        public Action OnUpdate;
    }
}
