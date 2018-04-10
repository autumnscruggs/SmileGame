using UnityEngine;
using System.Collections;

public class NPCLoseAnimation : Simple2DAnimator
{
    public GameObject npcAnimationObject;
    public float delayCloseTime;
    private bool startDelay;
    private float delay;

    protected override void OnAwake()
    {
        npcAnimationObject.SetActive(false);
        base.OnAwake();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (startDelay)
        {
            delay += Time.deltaTime;
            if(delay > delayCloseTime)
            {
                npcAnimationObject.SetActive(false);
                this.enabled = false;
            }
        }
    }

    public override void Stop()
    {
        base.Stop();
        startDelay = true;
    }

    public void StartAnimation()
    {
        npcAnimationObject.SetActive(true);
        this.Play(0);
    }
}
