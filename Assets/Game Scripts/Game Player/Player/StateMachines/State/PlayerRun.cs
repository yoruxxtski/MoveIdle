using UnityEngine;

public class PlayerRun : StateBase
{
    private PlayerStateMachine playerController;
    private Vector3 moveDir;

    private float speed = 5f;

    public override void OnEnter(StateControllerBase stateController)
    {
        base.OnEnter(stateController);
        playerController = stateController as PlayerStateMachine;

        playerController.playerAnimation.SetIdleAnimation(false);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        // Read Input
        if (playerController.isAlive)
            ReadInput();
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        // Move
        if (playerController.isAlive)
            Move();
    }
    public override void OnExit()
    {
        base.OnExit();
    }

    public void ReadInput() {
        moveDir = playerController.playerInput.GetPlayerInput();
    }

    public void Move() {
        // If no input detected -> change to idle state
        if (moveDir == Vector3.zero) {
            playerController.ChangeState(playerController.playerIdle);
            playerController.playerDetect.isRunning = true;
            return;
        }
        // Move
        playerController.rgBD.MovePosition(
            playerController.transform.position + moveDir.normalized * speed * Time.fixedDeltaTime);
        // Rotate
        float angel = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
        playerController.transform.rotation = Quaternion.Euler(0, angel, 0);
    }
}