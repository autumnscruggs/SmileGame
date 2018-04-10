using UnityEngine;
using System.Collections;

[System.Serializable]
public class AudioSimple
{
    public AudioClip audioClip;

    public AudioSimple(AudioClip clip)
    {
        audioClip = clip;
    }
}

[System.Serializable]
public class MusicSimple : AudioSimple
{
    public MusicState audioState;
    public float currentTime = 0;

    public MusicSimple(MusicState state, AudioClip clip) : base(clip)
    {
        audioState = state;
    }
}

