using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;
    private int gold;       // Gold for the current game
    private int totalGold;  // Total gold across all games
    public TextMeshProUGUI goldText;
    void Awake()
    {
        if (instance != this && instance != null) {
            Destroy(this);
            return;
        }
        instance = this;
    }
    void Start()
    {
        // ResetGold();
        LoadGold();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.Save();
    }

    public void SaveGold()
    {
        totalGold = PlayerPrefs.GetInt("TotalGold", 0) + gold; // Add to total
        PlayerPrefs.SetInt("TotalGold", totalGold);
        PlayerPrefs.SetInt("Gold", 0); // Reset gold for new game
        PlayerPrefs.Save();
    }

     // Load total gold when the game starts
    public void LoadGold()
    {
        gold = PlayerPrefs.GetInt("Gold", 0);
        totalGold = PlayerPrefs.GetInt("TotalGold", 0);
        goldText.SetText(totalGold+"");
    }

    public int GetGameGold() {
        return PlayerPrefs.GetInt("Gold" , 0);
    }

    public void ResetGold() {
        PlayerPrefs.DeleteKey("Gold");
        PlayerPrefs.DeleteKey("TotalGold");
        PlayerPrefs.Save();
    }
}