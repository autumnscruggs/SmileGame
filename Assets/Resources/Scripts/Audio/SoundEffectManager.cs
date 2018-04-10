using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager singleton; // Singleton instance   
    public static SoundEffectManager Instance
    {
        get
        {
            if (singleton == null)
            {
                Debug.LogError("[SoundEffectManager]: Instance does not exist!");
                return null;
            }

            return singleton;
        }
    }

    private AudioSource source;
    public AudioSimple currentSoundEffect;
    public List<AudioSimple> clips = new List<AudioSimple>();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        foreach(Button b in GameObject.FindObjectsOfTypeAll(typeof(Button)))
        {
            if (b.GetComponent<PlayerCombatGUIButton>() == null) { b.onClick.AddListener(PlayUIClick); }
        }
    }

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
    }

    public void PlayItemCollect()
    {
        source.PlayOneShot(clips[0].audioClip);
    }

    public void PlayUIClick()
    {
        source.PlayOneShot(clips[1].audioClip);
    }

    public void PlayUIHover()
    {
        source.PlayOneShot(clips[2].audioClip);
    }
}
