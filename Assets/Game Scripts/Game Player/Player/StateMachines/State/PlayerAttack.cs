using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : StateBase
{
    private PlayerStateMachine playerController;
    private Coroutine startAttack;
    /*
        ? I want player can't flickering & attack many times
        ? I have to put it in a coroutine 
        ? & after x seconds it can attack else it will just exit & stop the coroutine
    */
    public override void OnEnter(StateControllerBase stateController)
    {
        base.OnEnter(stateController);
        playerController = stateController as PlayerStateMachine;
        playerController.playerAnimation.SetAttackAnimation(true);  
        if (playerController.hasPowerUp){
            playerController.playerAnimation.SetUltiAnimation(true);
        }
        // After attack -> have to run again before can attack
        playerController.playerDetect.isRunning = false;
        playerController.transform.LookAt(playerController.playerDetect.enemySelected.transform);
        startAttack = playerController.StartCoroutine(StartAttack());

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (playerController.playerInput.GetPlayerInput() != Vector3.zero) {
            playerController.ChangeState(playerController.playerRun);
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        if (startAttack != null) {
            playerController.StopCoroutine(startAttack);
            startAttack = null;
        }
    }

    // * 3f = time after experimented to look as smooth as possible
    IEnumerator StartAttack() {
        yield return new WaitForSeconds(0.3f);
        Attack();
    }
    // TODO : throw projectile (should I use a predefined attack pos or offset & Vector.forward)
    public void Attack() {
        GameObject projectile = playerController.playerEquip.projectilePool.GetObject();
        // projectile.transform.position = 
        //     playerController.transform.position + playerController.transform.forward * offset;
        projectile.transform.position = playerController.attackPos.transform.position;
        Projectile prj = projectile.GetComponent<Projectile>();

        prj.original_scale = playerController.playerData.weaponData.projectile.transform.localScale;
        prj.original_speed = playerController.playerData.weaponData.projectile.speed;

        prj.dir = playerController.transform.forward;
        prj.weaponType = playerController.playerData.weaponData.weaponType;
        prj.attackPos = playerController.attackPos.transform;

        if (playerController.hasPowerUp) {
            prj.PowerUp();
            playerController.hasPowerUp = false;
            playerController.playerDetect.circleDetection.transform.localScale /= 2;
            playerController.playerDetect.detectRange = playerController.playerDetect.originalDetectRange;
        }


        projectile.SetActive(true);
    }
}