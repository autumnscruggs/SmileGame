using UnityEngine;
using System.Collections;

//This is very scripty but it's just figuring out when to trigger level completion 

public class LevelEndDoor : MonoBehaviour
{
	public EnemyCounter enemyCounter;
	protected SpriteRenderer spriteRenderer;
	public Sprite doorClosed;
	public Sprite doorOpen;
	public bool canOpenDoor { get; private set; }
	public GameObject warningUI;

	protected bool canActivateTrigger = true;

	protected virtual void Awake()
	{
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = doorClosed;
	}

	protected virtual void Start()
	{
		canOpenDoor = false;
		warningUI.SetActive(false);
	}

	void Update ()
	{
		if (enemyCounter.AllEnemiesDead)
		{
			EnemiesDead();
		}
		else
		{
			EnemiesAlive();

		}
	}

	protected virtual void EnemiesDead()
	{
		spriteRenderer.sprite = doorOpen;
		canOpenDoor = true;
	}

	protected virtual void EnemiesAlive()
	{
		spriteRenderer.sprite = doorClosed;
		canOpenDoor = false;
	}

	protected virtual void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.GetComponent<Player>() != null && canActivateTrigger)
		{
			PauseMenu.PauseGame(true);

			if (canOpenDoor)
			{
				ChangeScenes();
			}
			else
			{
				warningUI.SetActive(true);
			}
		}
	}

	protected virtual void ChangeScenes()
	{
		LevelManager.CompleteLevel();
		SceneTransitionManager.ChangeScenes(Scenes.HubWorldScene);
	}

	public void WarningUIYes()
	{
		SceneTransitionManager.ChangeScenes(Scenes.HubWorldScene);
	}

	public void WarningUINo()
	{
		warningUI.SetActive(false);
		PauseMenu.PauseGame(false);
	}
}
