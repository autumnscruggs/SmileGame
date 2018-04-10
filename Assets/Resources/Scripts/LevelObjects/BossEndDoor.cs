using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEndDoor : LevelEndDoor
{
    protected override void ChangeScenes()
    {
        EndSlateManager.Instance.endSlate = EndState.Victory;
        SceneTransitionManager.ChangeScenes(Scenes.EndGameScene);
    }

    protected override void EnemiesAlive()
    {
        base.EnemiesAlive();
        this.spriteRenderer.enabled = false;
        this.canActivateTrigger = false;
    }

    protected override void EnemiesDead()
    {
        base.EnemiesDead();
        this.spriteRenderer.enabled = true;
        this.canActivateTrigger = true;
    }
}
