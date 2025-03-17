using UnityEngine;

public class PlayerStateMachine : StateControllerBase
{
    public PlayerIdle playerIdle = new PlayerIdle();
    public PlayerRun playerRun = new PlayerRun();
    public PlayerAttack playerAttack = new PlayerAttack();
    public PlayerDie playerDie = new PlayerDie();
    public PlayerWin playerWin = new PlayerWin();
    [HideInInspector] public PlayerAnimation playerAnimation;
    [HideInInspector] public Rigidbody rgBD;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public PlayerDetect playerDetect;
    public PlayerData playerData;
    public GameObject levelComponent;
    [HideInInspector] public PlayerEquip playerEquip;
    public GameObject attackPos;
    public bool hasPowerUp = false;
    public bool isAlive = true;
    
    void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        rgBD = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerDetect = GetComponent<PlayerDetect>();
        playerEquip = GetComponent<PlayerEquip>();
    }
    void Start()
    {
        ChangeState(playerIdle);
    }
    void OnEnable()
    {
        EnemyDie.OnEnemyDeathNoParam += PlayerWin;
    }

    void OnDisable()
    {
        EnemyDie.OnEnemyDeathNoParam -= PlayerWin;
    }

    public void PlayerWin() {
        if (isAlive && (EnemyManager.totalEnemies 
        + EnemyManager.instance.currentActiveEnemies.Count == 0)) {
            ChangeState(playerWin);
        }
    }
}