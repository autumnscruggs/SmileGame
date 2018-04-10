using UnityEngine;
using System.Collections;

namespace Commands
{
    /// <summary>
    /// ICommand Exceutes on a GameComponent
    /// </summary>
    public interface ICommand
    {
        void Execute(GameObject gc);
    }

    public interface ICommandWVector2 : ICommand
    {
        void Execute(GameObject gc, Vector2 v1);
    }
}
