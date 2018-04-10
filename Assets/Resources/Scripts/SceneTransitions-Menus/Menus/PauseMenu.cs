using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private PauseMenuInventory inventory;
    private Player player;
    private EnemyCounter counter;
    public KeyCode pauseMenuKey;

    public Text currentHealth;
    public Text enemeisLeft;

    void Awake()
    {
        TogglePauseMenu(false);

        inventory = this.GetComponent<PauseMenuInventory>();
        player = GameObject.FindObjectOfType<Player>();
        counter = GameObject.FindObjectOfType<EnemyCounter>();
    }

	void Update ()
    {
        if (KeyboardInput.IsKeyDown(pauseMenuKey))
        {
            TogglePauseMenu();
            if (inventory.gameObject.activeInHierarchy)
            { inventory.ToggleInventory(false); }
        }

        currentHealth.text = "Health: " + System.Math.Ceiling(player.Health) + " / " + player.MaxHealth;
        enemeisLeft.text = "Cheered: " + (counter.maxEnemies - counter.enemiesCount) + " / " + counter.maxEnemies;
    }

    public void ReturnToHubButton()
    {
        if (AsyncToggle.loadAsynchronously) { LevelToHubAsync.Instance.LoadHubWorld(); }
        else { SceneTransitionManager.ChangeScenes(Scenes.HubWorldScene); }
    }

    public void MainMenuButton()
    {
        if (AsyncToggle.loadAsynchronously) { LevelToHubAsync.Instance.LoadMainMenu(); }
        else { SceneTransitionManager.ChangeScenes(Scenes.MainMenuScene); }
    }

    public void ViewInventoryButton()
    {
        inventory.ToggleInventory(true);
    }

    private void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        PauseGame(pauseMenu.activeInHierarchy);
    }
    public void TogglePauseMenu(bool toggle)
    {
        pauseMenu.SetActive(toggle);
        PauseGame(toggle);
    }

    public static void PauseGame(bool paused)
    {
        if (paused)
        {
            GameObject.FindObjectOfType<PlayerUnityMovement>().enabled = false;
            foreach (NPCController npc in GameObject.FindObjectsOfType<NPCController>())
            {
                npc.enabled = false;
                npc.canMove = false;
            }
        }
        else
        {
            GameObject.FindObjectOfType<PlayerUnityMovement>().enabled = true;
            foreach (NPCController npc in GameObject.FindObjectsOfType<NPCController>())
            {
                if (npc.GetComponent<NPC>().combatState != CombatParticipant.CombatState.Dead) { npc.enabled = true; npc.canMove = true; }
            }
        }
    }
}
