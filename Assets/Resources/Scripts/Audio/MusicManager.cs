using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MusicState { Main, MiniBoss, FinalBossApproach, FinalBoss, Victory, Defeat }

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private static MusicManager singleton; // Singleton instance   
    public static MusicManager Instance
    {
        get
        {
            if (singleton == null)
            {
                Debug.LogError("[MusicManager]: Instance does not exist!");
                return null;
            }

            return singleton;
        }
    }

    public bool playOnAwake = true;
    public bool loop = true;

    private AudioSource source;
    public MusicSimple currentMusicClip;
    public List<MusicSimple> clips = new List<MusicSimple>();

    void Awake()
    {
        #region Singleton
        // Found a duplicate instance of this class, destroy it!
        if (singleton != null && singleton != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            // Create singleton instance
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
        source = this.GetComponent<AudioSource>();
        SetCurrentMusicClip(clips[0]);
        
        if (playOnAwake) { source.Play(); }
        source.loop = loop;
    }

    public void ChangeMusicState(MusicState newState, bool restartOldClip = false, bool restartAllClips = false)
    {
        if (restartAllClips)
        {
            foreach(MusicSimple audioS in clips)
            {
                audioS.currentTime = 0;
            }
        }

        //Restart or pause old audio clip?
        if (restartOldClip) { currentMusicClip.currentTime = 0; }
        else   { currentMusicClip.currentTime = source.time; }

        //Change audio state
        MusicSimple aS = clips.Find(item => item.audioState == newState);
        SetCurrentMusicClip(aS);
        source.Play();
    }

    public void SetCurrentMusicClip(MusicSimple newClip)
    {
        currentMusicClip = newClip;
        source.clip = currentMusicClip.audioClip;
        source.time = currentMusicClip.currentTime;
    }

    public void PlaySoundEffect(AudioSimple audioSimple)
    {

    }
}
