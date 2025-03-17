using UnityEngine;
using UnityEngine.AI;

public class EnemyRun : StateBase
{
    private EnemyStateMachine enemyController;
    private float walkRange;
    private Vector3 walkPoint;
    private bool walkPointSet;
    private float stuckTimer = 0f;
    private float stuckThreshold = 2f; // Time in seconds to consider stuck

    public override void OnEnter(StateControllerBase stateController)
    {
        base.OnEnter(stateController);
        enemyController = stateController as EnemyStateMachine;
        enemyController.agent.isStopped = false;
        enemyController.enemyAnimation.SetIdleAnimation(false);
        SearchWalkPoint();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (walkPointSet) {
            // move
            enemyController.agent.SetDestination(walkPoint);

            // If have moved 
            if (!enemyController.agent.pathPending 
                && enemyController.agent.remainingDistance <= enemyController.agent.stoppingDistance)
            {
                walkPointSet = false;
                enemyController.agent.isStopped = true;  // Stop movement immediately
                enemyController.enemyDetect.isRunning = true;
                enemyController.ChangeState(enemyController.enemyIdle);
            } 
            else {
                // Case : Stuck
                CheckIfStuck();
            }
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        enemyController.agent.isStopped = true;
        enemyController.agent.velocity = Vector3.zero;
    }

    // ? search for a destination
    public void SearchWalkPoint() {
        walkRange = Random.Range(10f, 20f);

        Vector3 randomPos = enemyController.transform.position 
            + walkRange * Random.insideUnitSphere;
        randomPos.y = 0;
        //? maxDistance tells Unity how far it should search for the nearest valid point on the NavMesh.      
        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit
        , walkRange , enemyController.walkableAreaMask)) {
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }
    public void CheckIfStuck() {
        if (enemyController.agent.velocity.magnitude < 0.1f) // Considered not moving
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer > stuckThreshold)
            {
                Debug.Log("Enemy is stuck, finding a new walk point.");
                stuckTimer = 0f;
                SearchWalkPoint();
            }
        }
        else
        {
            stuckTimer = 0f; // Reset if moving
        }
    }
}