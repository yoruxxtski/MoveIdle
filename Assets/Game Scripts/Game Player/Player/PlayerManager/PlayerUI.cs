using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI playerLevel;
    public PlayerData playerData;

    void Awake()
    {
        playerData.ResetLevel();
        playerLevel.SetText(playerData.playerLevel + "");
    }
    public void UpdatePlayerLevel() {
        playerLevel.SetText(playerData.playerLevel + "");
    }
    
    void OnEnable()
    {
        Projectile.PlayerKill += UpdatePlayerLevel;
    }
    void OnDisable()
    {
        Projectile.PlayerKill -= UpdatePlayerLevel;
    }
}
