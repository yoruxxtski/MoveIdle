using System;
using TMPro;
using UnityEngine;

public class EnemyDetail : MonoBehaviour
{
    public int level;
    public float detectRange;
    public float originalDetectRange;
    public TextMeshProUGUI enemyLevel;
    public TextMeshProUGUI enemyName;
    public PlayerData playerData;

    void OnEnable()
    {
        InitialLevelRange();
        Projectile.EnemyKill += UpdateEnemyLevelUI;
    }
    void OnDisable()
    {
        Projectile.EnemyKill -= UpdateEnemyLevelUI;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

    public void UpdateEnemyLevelUI() {
        enemyLevel.SetText(level+"");
    }
    public void InitialLevelRange() {
        enemyName.SetText(RandomName.GetRandomName());
        if (playerData.playerLevel <= 3) {
            level = playerData.playerLevel + UnityEngine.Random.Range(0, 4);
        } else {
            level = playerData.playerLevel + UnityEngine.Random.Range(-3, 4);
        }
        for (int i = 1; i < level; i++) {
            detectRange += detectRange * 0.02f; 
        }
        originalDetectRange = detectRange;
        enemyLevel.SetText(level+"");
    }
}