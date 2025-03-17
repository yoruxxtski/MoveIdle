using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateControllerBase
{
    public EnemyIdle enemyIdle = new EnemyIdle();
    public EnemyRun enemyRun = new EnemyRun();
    public EnemyAttack enemyAttack = new EnemyAttack();
    public EnemyDie enemyDie = new EnemyDie();
    [HideInInspector] public EnemyAnimation enemyAnimation;
    [HideInInspector] public EnemyDetect enemyDetect;
    [HideInInspector] public EnemyDetail enemyDetail;
    [HideInInspector] public EnemyEquip enemyEquip;
    public GameObject attackPos;
    public bool hasPowerUp = false;
    public NavMeshAgent agent;
    public int walkableAreaMask;
     public GameObject enemyInfo;

    void Awake()
    {
        enemyEquip = GetComponent<EnemyEquip>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        agent = GetComponent<NavMeshAgent>();
        enemyDetect = GetComponent<EnemyDetect>();
        enemyDetail = GetComponent<EnemyDetail>();
        walkableAreaMask = 1 << NavMesh.GetAreaFromName("Walkable");
    }
    
    void OnEnable()
    {
        agent.isStopped = false;

        transform.gameObject.GetComponentInChildren<Collider>().enabled = true;
        transform.gameObject.GetComponent<EnemyDetect>().enabled = true;
        transform.gameObject.GetComponentInChildren<Collider>().gameObject.layer 
            = LayerMask.NameToLayer("Enemy");
        transform.gameObject.GetComponent<ArrowIndicator>().enabled = true;
        if (!enemyInfo.activeInHierarchy) enemyInfo.SetActive(true);
        ChangeState(enemyRun);
    }
}