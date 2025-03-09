using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int playerLevel;
    public WeaponData weaponData;
    // ? place in player hat
    public GameObject hat;
    // ? set player pant mat
    public Material pants;

    public void ResetLevel() {
        playerLevel = 1;
    }

}