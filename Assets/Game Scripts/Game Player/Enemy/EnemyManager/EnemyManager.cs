using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public static int totalEnemies;
    public List<GameObject> currentActiveEnemies;
    public int numOfEnemiesOnMap;
    public PlayerData playerData;
    public LevelData[] levelData;
    public List<Transform> spawnPos;
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
        totalEnemies = levelData[playerData.level - 1].numsOfEnemies;
    }

    public void SpawnEnemies() {
        for (int i = 0; i < numOfEnemiesOnMap; i++) {
            GameObject enemy = SpawnEnemy();
        }
    }


    void OnEnable() {
        EnemyDie.OnEnemyDeath += HandleEnemyDeath; // Subscribe to death event
    }

    void OnDisable() {
        EnemyDie.OnEnemyDeath -= HandleEnemyDeath; // Unsubscribe on disable
    }

    void HandleEnemyDeath(GameObject enemy) {
        if (currentActiveEnemies.Contains(enemy)) {
            currentActiveEnemies.Remove(enemy);
        }
        SpawnEnemy();
    }

    public GameObject SpawnEnemy() {
        
        if (GameManager.gameOver) return null;

        if (totalEnemies <= 0) return null; // No more enemies to spawn

        GameObject enemy = GameManager.instance.enemyPool.GetObject();
        enemy.transform.position = SetPosition();

        enemy.SetActive(true);
        currentActiveEnemies.Add(enemy);

        totalEnemies --;
        return enemy;
    }
    public Vector3 SetPosition() {
        int rand = Random.Range(0, spawnPos.Count);
        Transform pos = spawnPos[rand];
        // Generate a random position inside a circle (XZ plane)
        Vector2 randomCircle = Random.insideUnitCircle * 13f; // 13f radius

        // Convert the 2D circle coordinates to a 3D position
        Vector3 enemySpawn = new Vector3(pos.position.x + randomCircle.x
            , pos.position.y, pos.position.z + randomCircle.y);
        return enemySpawn;
    }

}
