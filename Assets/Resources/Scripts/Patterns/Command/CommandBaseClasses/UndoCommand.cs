using UnityEngine;
using System.Collections;

namespace Commands
{
    public class UndoCommand : Command
    {
        protected CommandWithUndo command;

        public UndoCommand(CommandWithUndo command)
        {
            this.commandName = "Undo " + command.commandName;
            this.command = command;
        }
    }
}