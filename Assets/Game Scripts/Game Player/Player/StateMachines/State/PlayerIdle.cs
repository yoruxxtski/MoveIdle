using UnityEngine;

public class PlayerIdle : StateBase
{
    private PlayerStateMachine playerController;
    public override void OnEnter(StateControllerBase stateController)
    {
        base.OnEnter(stateController);
        playerController = stateController as PlayerStateMachine;
        playerController.playerAnimation.SetIdleAnimation(true);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (playerController.playerInput.GetPlayerInput() != Vector3.zero 
            && playerController.isAlive) {
            playerController.ChangeState(playerController.playerRun);
        }
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}