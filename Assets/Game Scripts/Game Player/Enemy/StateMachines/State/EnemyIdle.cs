using System.Collections;
using UnityEngine;

public class EnemyIdle : StateBase
{
    private EnemyStateMachine enemyController;
    private Coroutine idleCoroutine;
    public override void OnEnter(StateControllerBase stateController)
    {
        base.OnEnter(stateController);
        enemyController = stateController as EnemyStateMachine;

        if (enemyController.agent.isStopped) {
            enemyController.agent.isStopped = false;
        }
        
        enemyController.enemyAnimation.SetIdleAnimation(true);
        idleCoroutine = enemyController.StartCoroutine(StayIdle());
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        if (idleCoroutine != null) {
            enemyController.StopCoroutine(idleCoroutine);
            idleCoroutine = null;
        }
    }
    IEnumerator StayIdle() {
        yield return new WaitForSeconds(2f);
        enemyController.ChangeState(enemyController.enemyRun);
    }
}