using UnityEngine;
using System.Collections;

namespace Commands
{
    public class CommandWithUndo : Command, ICommandWithUndo
    {
        protected GameObject gObject;   //Referenece to game object
        public UndoCommand UndoCommand { get; set; }

        public CommandWithUndo() : base()
        {

        }

        public override void Execute(GameObject _gameObject)
        {
            this.gObject = _gameObject;   //Hold a refernce to the game component this command was excuted on
            base.Execute(_gameObject);
        }
        public virtual void UnExecute()
        {
            this.UndoCommand.Execute(gObject);
        }
    }
}

