using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Observer
{
    public interface IObserver
    {
        void ObserverUpdate(System.Object sender, System.Object message);
    }
}
