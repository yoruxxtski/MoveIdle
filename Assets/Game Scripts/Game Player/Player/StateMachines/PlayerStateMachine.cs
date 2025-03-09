using UnityEngine;

public class PlayerStateMachine : StateControllerBase
{
    public PlayerIdle playerIdle = new PlayerIdle();
    public PlayerRun playerRun = new PlayerRun();
    public PlayerAttack playerAttack = new PlayerAttack();
    [HideInInspector] public PlayerAnimation playerAnimation;
    [HideInInspector] public Rigidbody rgBD;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public PlayerDetect playerDetect;
    public PlayerData playerData;
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
}