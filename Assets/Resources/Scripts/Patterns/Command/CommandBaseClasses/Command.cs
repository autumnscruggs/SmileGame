using UnityEngine;
using System.Collections;

namespace Commands
{
    public class Command : ICommand
    {
        public string commandName;      

        public Command()
        {

        }

        public virtual void Execute(GameObject gc)
        {
            this.Log();     //Log that command happened;
        }

        protected virtual string Log()
        {

            string LogString = string.Format("{0} executed.", commandName);
            #if DEBUG
            //Only write to console if run in Debug
            //Debug.Log(LogString);
            #endif
            return LogString;
        }
    }
}
