using System.Collections;
using UnityEngine;

public class EnemyAttack : StateBase
{
    private EnemyStateMachine enemyController;
    private Vector3 direction;
    private Coroutine attackCoroutine;
    public override void OnEnter(StateControllerBase stateController)
    {
        base.OnEnter(stateController);
        enemyController = stateController as EnemyStateMachine;
        enemyController.enemyAnimation.SetAttackAnimation(true);
        if (enemyController.hasPowerUp) {
            enemyController.enemyAnimation.SetUltiAnimation(true);
        }

        enemyController.gameObject.transform.LookAt(enemyController.enemyDetect.enemyFound.transform);
        direction = - enemyController.transform.position + enemyController.enemyDetect.enemyFound.transform.position;
        attackCoroutine = enemyController.StartCoroutine(startAttack());
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override void OnExit()
    {
        base.OnExit();
        if (attackCoroutine != null) {
            enemyController.StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
    IEnumerator startAttack() {
        yield return new WaitForSeconds(0.3f);
        Attack();
        yield return new WaitForSeconds(1f);

        enemyController.agent.isStopped = false;
        enemyController.ChangeState(enemyController.enemyRun);
    }

    public void Attack() {
        GameObject projectile = enemyController.enemyEquip.projectilePool.GetObject();
        projectile.transform.position = enemyController.attackPos.transform.position;

        Projectile prj = projectile.GetComponent<Projectile>();

        prj.dir = direction;

        prj.weaponType = enemyController.enemyEquip.enemyWeapon.weaponType;
        prj.attackPos = enemyController.attackPos.transform;

        prj.original_scale = enemyController.enemyEquip.enemyWeapon.projectile.transform.localScale;
        prj.original_speed = enemyController.enemyEquip.enemyWeapon.projectile.speed;

        prj.thrower = enemyController.transform.gameObject;

        
        if (enemyController.hasPowerUp) {
            prj.PowerUp();
            enemyController.hasPowerUp = false;
            enemyController.enemyDetail.detectRange = enemyController.enemyDetail.originalDetectRange;
        }
        projectile.SetActive(true);
    }


}