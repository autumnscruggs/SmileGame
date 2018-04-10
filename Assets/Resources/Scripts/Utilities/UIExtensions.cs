using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIExtensions : MonoBehaviour {}

public static class GraphicExtensions
{
    /// <summary>
    /// Fade methods forUI elements;
    /// </summary>
    /// <param name="g"></param>
    public static void FadeIn(this Graphic g, float duration)
    {
        g.GetComponent<CanvasRenderer>().SetAlpha(0f);
        g.CrossFadeAlpha(1f, duration, false);//second param is the time
    }
    public static void FadeOut(this Graphic g, float duration)
    {
        g.GetComponent<CanvasRenderer>().SetAlpha(1f);
        g.CrossFadeAlpha(0f, duration, false);
    }
}
