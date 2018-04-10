using UnityEngine;
using System.Collections;

namespace Commands
{
    /// <summary>
    /// Undo is a ICommand With and UndoCommand
    /// </summary>
    public interface ICommandWithUndo : ICommand
    {
        UndoCommand UndoCommand { get; set; }

        void UnExecute();
    }
}
