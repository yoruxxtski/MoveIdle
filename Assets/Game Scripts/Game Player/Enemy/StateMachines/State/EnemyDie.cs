using System;
using System.Collections;
using UnityEngine;

public class EnemyDie : StateBase
{
    private EnemyStateMachine enemyController;
    private Coroutine dieC;
    public static event Action<GameObject> OnEnemyDeath; // Event for enemy death
    public static event Action OnEnemyDeathNoParam;
    public override void OnEnter(StateControllerBase stateController)
    {
        base.OnEnter(stateController);
        enemyController = stateController as EnemyStateMachine;
        DisableEnemy();
        enemyController.enemyAnimation.SetDeadAnimation(true);
        dieC = enemyController.StartCoroutine(Die());
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override void OnExit()
    {
        base.OnExit();
        if (dieC != null) {
            enemyController.StopCoroutine(dieC);
            dieC = null;
        }
    }

    
    IEnumerator Die() {
        OnEnemyDeath?.Invoke(enemyController.gameObject);
        OnEnemyDeathNoParam?.Invoke();
        yield return new WaitForSeconds(2.5f);
        enemyController.transform.gameObject.SetActive(false);
    }

    public void DisableEnemy() {
         // Deactive the agent
        enemyController.agent.isStopped = true;
        enemyController.agent.velocity = Vector3.zero;

        // Deactive the collider
        enemyController.GetComponentInChildren<Collider>().enabled = false;
        // Deactive the enemy Detection
        enemyController.GetComponent<EnemyDetect>().enabled = false;
        // Change layer to dead layer
        enemyController.GetComponentInChildren<Collider>().gameObject.layer 
            = LayerMask.NameToLayer("Die");
        enemyController.GetComponent<ArrowIndicator>().enabled = false;
        
        enemyController.enemyInfo.SetActive(false);
    }
}