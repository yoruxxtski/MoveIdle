using System;
using UnityEngine;

public class PlayerWin : StateBase
{
    private PlayerStateMachine playerController;
    public static event Action GameWin;
    public override void OnEnter(StateControllerBase stateController)
    {
        base.OnEnter(stateController);
        playerController = stateController as PlayerStateMachine;
        playerController.playerAnimation.SetWinAnimation(true);
        GoldManager.instance.AddGold(100);
        GameWin?.Invoke();
        StopGame();
    }

    public void StopGame() {
        playerController.GetComponentInChildren<PlayerInput>().enabled = false;
         // Change layer to dead layer
        playerController.GetComponentInChildren<Collider>().gameObject.layer 
            = LayerMask.NameToLayer("Die");
         // Deactive the collider
        playerController.GetComponentInChildren<Collider>().enabled = false;

        // Deactive the enemy Detection
        if (playerController.GetComponent<PlayerDetect>().enemySelected != null) {
            playerController.GetComponent<PlayerDetect>().circleDetection.SetActive(false);
            playerController.GetComponent<PlayerDetect>().enemySelected.gameObject.GetComponent<EnemySelectorIndicator>().TurnSelector(false);
            playerController.GetComponent<PlayerDetect>().enabled = false;
        }

        playerController.levelComponent.SetActive(false);
        playerController.isAlive = false;
    }
}