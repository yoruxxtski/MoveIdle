using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int level;
    public int playerLevel;
    public float enemyKilled;
    public WeaponData weaponData;
    // ? place in player hat
    public GameObject hat;
    // ? set player pant mat
    public Material pants;

    public void ResetLevel() {
        playerLevel = 1;
    }

}