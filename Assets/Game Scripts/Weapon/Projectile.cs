using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float duration = 1.5f;
    public float speed = 5f;
    [SerializeField] private float rotationSpeed = -360f; // Degrees per second
    public Vector3 dir;
    public weaponType weaponType;
    public Transform attackPos;
    private float timer;
    public float original_speed;
    public Vector3 original_scale;
    public GameObject thrower;

    public static event Action PlayerKill;
    public static event Action EnemyKill;
    public static event Action<GameObject> playerDead;

    void OnEnable()
    {
        timer = 0f;
        if (weaponType == weaponType.topRotate) {
            transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(0, -90, 0);
        }
    }

    void Update()
    {
        // Move
        transform.position += dir.normalized * speed * Time.deltaTime;
        // Advance the timer
        timer += Time.deltaTime;

        // Check weapon to rotate
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        if (timer > duration) {
            if (weaponType == weaponType.topRotate) {
                gameObject.SetActive(false);
            } else {   
                dir = (attackPos.position - transform.position); // Change direction toward attackPos
                // Disable when close enough
                if (Vector3.Distance(transform.position, attackPos.position) < 0.5f)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        
        if (thrower.tag == "Player") {
            if (thrower == other.transform.root.gameObject) return;
        }

        if (thrower.tag == "Enemy") {
            Transform collider = other.transform;
            while(collider.parent != null && collider.tag != "Enemy") {
                collider = collider.parent;
            }
            if (thrower == collider.gameObject) return;
        }
        
        // Find correct target (enemy or player) in hierarchy
        Transform target = other.transform;
        while (target.parent != null && !target.CompareTag("Enemy") && !target.CompareTag("Player")) 
        {
            target = target.parent;
        }

        // Handle Enemy Hit
        if (target.CompareTag("Enemy"))
        {
            EnemyStateMachine enemyStateMachine = target.GetComponent<EnemyStateMachine>();
            if (enemyStateMachine != null)
            {
                if (thrower.tag == "Player") {
                    Debug.Log(10 * target.GetComponent<EnemyDetail>().level);
                    GoldManager.instance.AddGold(10 * target.GetComponent<EnemyDetail>().level);

                    if (thrower.GetComponent<PlayerStateMachine>().playerData.playerLevel
                        <= target.GetComponent<EnemyDetail>().level) 
                    {
                        thrower.GetComponent<PlayerStateMachine>().playerData.playerLevel += 1;

                        thrower.GetComponent<PlayerStateMachine>().playerDetect.detectRange +=
                            thrower.GetComponent<PlayerStateMachine>().playerDetect.detectRange * 0.02f;

                        thrower.GetComponent<PlayerStateMachine>().playerDetect.originalDetectRange = 
                            thrower.GetComponent<PlayerStateMachine>().playerDetect.detectRange;

                        PlayerKill?.Invoke();
                    }
                }
                if (thrower.tag == "Enemy") {
                    if (thrower.GetComponent<EnemyDetail>().level 
                        <= target.GetComponent<EnemyDetail>().level) 
                    {
                        thrower.GetComponent<EnemyDetail>().level += 1;
                        thrower.GetComponent<EnemyDetail>().detectRange += 
                        thrower.GetComponent<EnemyDetail>().detectRange * 0.02f;
                        thrower.GetComponent<EnemyDetail>().originalDetectRange = 
                            thrower.GetComponent<EnemyDetail>().detectRange;
                        EnemyKill?.Invoke();
                    }
                }

                enemyStateMachine.ChangeState(enemyStateMachine.enemyDie);
            }
        }

        // Handle Player Hit
        if (target.CompareTag("Player"))
        {
            PlayerStateMachine playerStateMachine = target.GetComponent<PlayerStateMachine>();
            if (playerStateMachine != null)
            {
                playerStateMachine.ChangeState(playerStateMachine.playerDie);
                playerStateMachine.isAlive = false;
                if (thrower.tag == "Enemy") {
                    if (thrower.GetComponent<EnemyDetail>().level 
                        <= target.GetComponent<PlayerStateMachine>().playerData.playerLevel) 
                    {
                        thrower.GetComponent<EnemyDetail>().level += 1;
                        thrower.GetComponent<EnemyDetail>().detectRange += 
                        thrower.GetComponent<EnemyDetail>().detectRange * 0.02f;
                        thrower.GetComponent<EnemyDetail>().originalDetectRange = 
                            thrower.GetComponent<EnemyDetail>().detectRange;

                        EnemyKill?.Invoke();
                    }
                    playerDead?.Invoke(thrower);
                }
            }
        }

        gameObject.SetActive(false);
    }

    


    public void PowerUp() {
        speed *= 2;
        transform.localScale *= 2;
    }
    void OnDisable()
    {
        speed = original_speed;
        transform.localScale = original_scale;
    }
}