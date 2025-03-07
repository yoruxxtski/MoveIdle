using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectPool powerPool;
    public static GameManager instance;
    public bool gameOver;
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this);
            return;
        }
        instance = this;
    }
    void Start()
    {
        StartCoroutine(startGame());
    }
    IEnumerator startGame() {

        // Game Run
        while(!gameOver) {
            
            yield return new WaitForSeconds(Random.Range(3, 15));
            if (PowerUp.total < 3)
                SpawnPowerUps();
        }
    }
    public void SpawnPowerUps() {
        GameObject pu = powerPool.GetObject();
        pu.SetActive(true);
        PowerUp.total ++;
    }
}