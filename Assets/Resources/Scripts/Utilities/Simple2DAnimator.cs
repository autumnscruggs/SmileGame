using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

//Inspired by http://www.gamasutra.com/blogs/JoeStrout/20150807/250646/2D_Animation_Methods_in_Unity.php

public class Simple2DAnimator : MonoBehaviour
{
    public GameObject objectToAnimateOn;
    public bool UIImageAnimation;
    protected SpriteRenderer spriteRenderer;
    protected Image image;
    protected Simple2DAnimation currentAnim;
    protected float currentSpeed;
    protected float nextFrameTime;

    public bool playAutomatically;
    public int currentFrame { get; protected set; }
    public bool animationFinished { get { return currentFrame >= currentAnim.frames.Length; } }
    public bool isPlaying { get; protected set; }

    public List<Simple2DAnimation> animations = new List<Simple2DAnimation>();

    #region Unity Functions
    void Awake()
    {
        OnAwake();
    }
    void Start()
    {
        OnStart();
    }
    void Update()
    {
        OnUpdate();
    }
    #endregion

    protected virtual void OnAwake()
    {
        if (!UIImageAnimation) { spriteRenderer = objectToAnimateOn.GetComponent<SpriteRenderer>(); }
        else { image = objectToAnimateOn.GetComponent<Image>(); }
        if (animations.Count > 0 && playAutomatically) Play(0);
    }

    protected virtual void OnStart()
    {}

    protected virtual void OnUpdate()
    {
        if (!isPlaying) return;
        //Increment frames
        IncrementFramesDependingOnTime();
        if (currentFrame > 0)
        {
            //if we're about to run out of frams, decide whether to loop or stop
            if (currentFrame >= currentAnim.frames.Length)
            {
                if (currentAnim.loop)
                { Loop(); }
                else
                { Stop(); return; }

            }

            if (!UIImageAnimation) { spriteRenderer.sprite = currentAnim.frames[currentFrame]; }
            else { image.sprite = currentAnim.frames[currentFrame]; }
        }
    }

    public virtual void Play(string name)
    {
        int index = animations.FindIndex(a => a.animationName == name);
        if (index > 0)
        { Play(index); }
    }

    public virtual void Play(int index)
    {
        if (index <= animations.Count || index >= 0)
        {
            currentAnim = animations[index];
            currentSpeed = currentAnim.framesPerSecond;
            currentFrame = -1;
            isPlaying = true;
        }
    }

    public virtual void Loop()
    {
        currentFrame = 0;
    }

    public virtual void Stop()
    {
        isPlaying = false;
    }

    public virtual void Resume()
    {
        isPlaying = true;
    }

    private void IncrementFramesDependingOnTime()
    {
        float time = currentSpeed * Time.deltaTime;
        nextFrameTime += time;

        if (nextFrameTime >= 1)
        {
            currentFrame++;
            nextFrameTime = 0;
        }
    }

    [System.Serializable]
    public class Simple2DAnimation
    {
        public string animationName;
        public Sprite[] frames;
        public float framesPerSecond;
        public bool loop;
        public float duration { get { return frames.Length * framesPerSecond; } }
    }
}
