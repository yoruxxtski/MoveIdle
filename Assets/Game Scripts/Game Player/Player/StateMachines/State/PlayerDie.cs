using UnityEngine;

public class PlayerDie : StateBase
{
    private PlayerStateMachine playerController;
    public override void OnEnter(StateControllerBase stateController)
    {
        base.OnEnter(stateController);
        playerController = stateController as PlayerStateMachine;
        playerController.playerAnimation.SetDeadAnimation(true);
    }
}