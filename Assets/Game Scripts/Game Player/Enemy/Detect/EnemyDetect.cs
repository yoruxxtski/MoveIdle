using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;
    [HideInInspector] public GameObject enemyFound;
    [HideInInspector] public bool isRunning = false;
    private EnemyDetail enemyDetail;
    private EnemyStateMachine enemyStateMachine;
    void Awake()
    {
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        enemyDetail = GetComponent<EnemyDetail>();
        enemyFound = null;
    }

    void Update()
    {
        CheckEnemies();
        if (enemyFound != null && isRunning) {
            isRunning = false;
            enemyStateMachine.ChangeState(enemyStateMachine.enemyAttack);
        }
    }
    public void CheckEnemies() {
        // Found Player
        Collider[] playerCollider = Physics.OverlapSphere(transform.position, enemyDetail.detectRange, playerLayerMask);

        if (playerCollider.Length > 0) {
            enemyFound = playerCollider[0].transform.root.gameObject;
            return;
        }


        Collider[] enemyColliders = Physics.OverlapSphere(transform.position, enemyDetail.detectRange, enemyLayerMask);
        if (enemyColliders.Length > 0) {
            // Get the first collider of enemy found
            foreach (Collider enemyCollider in enemyColliders) {

                Transform potentialEnemy = enemyCollider.transform;
                while (potentialEnemy.parent != null && !potentialEnemy.gameObject.CompareTag("Enemy")) {
                    potentialEnemy = potentialEnemy.parent; 
                }
                if (potentialEnemy.gameObject != this.gameObject) { // Ensure it's not detecting itself
                    enemyFound = potentialEnemy.gameObject;
                    return;
                }
            }
        }

        enemyFound = null;
    }
}
