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