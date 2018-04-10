using UnityEngine;
using System.Collections;

public class MouseInput
{
    public enum MouseButtons
    {
        LEFT,  //Mouse button 0
        RIGHT, //Mouse button 1
        MIDDLE //Mouse button 2
    }

    public static bool IsAnyMouseButtonDown()
    {
        if (Input.anyKeyDown) 
        {
            if(IsMouseButtonDown(MouseButtons.LEFT) || IsMouseButtonDown(MouseButtons.RIGHT) || IsMouseButtonDown(MouseButtons.MIDDLE))
            {
                return true;
            }
        }

        return false;
    }
    public static bool IsHoldingAnyMouseButton()
    {
        if (Input.anyKey)
        {
            if (IsHoldingMouseButton(MouseButtons.LEFT) || IsHoldingMouseButton(MouseButtons.RIGHT) || IsHoldingMouseButton(MouseButtons.MIDDLE))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsMouseButtonDown(MouseButtons button)
    {
        if (Input.GetMouseButtonDown((int)button))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool HasReleasedMouseButton(MouseButtons button)
    {
        if (Input.GetMouseButtonUp((int)button))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsHoldingMouseButton(MouseButtons button)
    {
        if (Input.GetMouseButton((int)button))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
