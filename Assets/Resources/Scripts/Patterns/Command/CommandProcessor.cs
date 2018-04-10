using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Commands
{
    /// <summary>
    /// Similar to yours without the input checking
    /// That's handled elsewhere
    /// </summary>

    public class CommandProcessor : MonoBehaviour
    {
        //List of previously processed commands
        Stack<ICommand> commandsStack = new Stack<ICommand>();
        public Command lastCommand; //for GUI purposes


        public CommandProcessor() : base()
        {

        }


        internal void ExecuteCommand(Command command, GameObject gameObject)
        {
            if (command != null)
            {
                if (command is ICommandWithUndo)
                {
                    commandsStack.Push((ICommandWithUndo)command); //only push commands with undo to the stack
                }

                lastCommand = command; //I added this bit to store the last command so even if 
                                       //it isn't added to the stack, you can still reference it (GUI)

                if (command is CombatCommand)
                {
                    CombatCommand cc = (CombatCommand)command;
                    cc.TakeCombatAction(gameObject);
                    //Debug.Log(cc.commandName);
                }
                else
                {
                    command.Execute(gameObject);
                }
            }
        }
       
    }
}