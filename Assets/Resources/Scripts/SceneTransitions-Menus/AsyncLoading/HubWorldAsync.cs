using UnityEngine;
using System.Collections;

public class HubWorldAsync : MonoBehaviour
{
    private static HubWorldAsync singleton; // Singleton instance   
    public static HubWorldAsync Instance
    {
        get
        {
            if (singleton == null)
            {
                Debug.LogError("[HubWorldAsync]: Instance does not exist!");
                return null;
            }

            return singleton;
        }
    }

    public AsyncLoading[] asyncLevels = new AsyncLoading[6];

    void Awake()
    {
        #region Singleton
        // Create singleton instance
        singleton = this;
        #endregion
    }

    private void Start()
    {
        if (AsyncToggle.loadAsynchronously)
        {
            StartCoroutine(LoadAsync());
        }
    }

    public void LoadScene(string sceneName)
    {
        AsyncLoading levelToLoad = System.Array.Find(asyncLevels, item => item.sceneToLoadNext == sceneName);
        int index = System.Array.IndexOf(asyncLevels, levelToLoad);

        for (int x = 0; x < asyncLevels.Length; x++)
        {
            if (x != index)
            {
                asyncLevels[x].KillScene();
            }
        }

        asyncLevels[index].LoadScene();
    }
    
    IEnumerator LoadAsync()
    {
        yield return new WaitForSeconds(0.5f);

        asyncLevels[0] = this.gameObject.AddComponent<AsyncLoading>();
        asyncLevels[0].sceneToLoadNext = Scenes.Level1Scene;

        asyncLevels[1] = this.gameObject.AddComponent<AsyncLoading>();
        asyncLevels[1].sceneToLoadNext = Scenes.Level2Scene;

        asyncLevels[2] = this.gameObject.AddComponent<AsyncLoading>();
        asyncLevels[2].sceneToLoadNext = Scenes.Level2Scene;

        asyncLevels[3] = this.gameObject.AddComponent<AsyncLoading>();
        asyncLevels[3].sceneToLoadNext = Scenes.Level3Scene;

        asyncLevels[4] = this.gameObject.AddComponent<AsyncLoading>();
        asyncLevels[4].sceneToLoadNext = Scenes.Level4Scene;

        asyncLevels[5] = this.gameObject.AddComponent<AsyncLoading>();
        asyncLevels[5].sceneToLoadNext = Scenes.BossScene;
    }
}
