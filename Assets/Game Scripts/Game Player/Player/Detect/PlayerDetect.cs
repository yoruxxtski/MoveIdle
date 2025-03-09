using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    [SerializeField]public float detectRange;
    [HideInInspector] public float originalDetectRange; // Store original range
    [SerializeField] private LayerMask enemyMask;
    [HideInInspector] public GameObject enemySelected;
    private GameObject prevSelected;
    private PlayerStateMachine playerStateMachine;
    public GameObject circleDetection;
    [HideInInspector] public bool isRunning = false;
    void Awake()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
        originalDetectRange = detectRange; // Store the initial detect range
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
    void Update()
    {
        CheckEnemies();
        if (enemySelected != null && isRunning && playerStateMachine.isAlive) {
            playerStateMachine.ChangeState(playerStateMachine.playerAttack);
        }
    }

    public void CheckEnemies() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange, enemyMask);
        // Case : Already found an enemy already -> Check if that enemy 
        // is already checked then keep else change
        if (enemySelected != null) {
            bool sameE = false;
            foreach (Collider collider in colliders) {
                if (collider.transform.root.gameObject == prevSelected) {
                    sameE = true;
                    break;
                }
            }
            if (!sameE) {
                // Enemy not present anymore
                enemySelected.gameObject.GetComponent<EnemySelectorIndicator>().TurnSelector(false); 
                enemySelected = null;
                prevSelected = null;
            }
            return;
        }

        // Didn't found an enemy
        if (colliders.Length > 0) {
            // Get the first enemy found 
            enemySelected = colliders[0].transform.root.gameObject;
            prevSelected = enemySelected;
            enemySelected.gameObject.GetComponent<EnemySelectorIndicator>().TurnSelector(true); 
        }
    }
}