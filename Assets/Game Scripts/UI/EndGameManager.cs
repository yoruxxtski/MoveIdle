using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyKillsText;
    [SerializeField] private TextMeshProUGUI playerPlacementText;

    [Header("PANELS")] 
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject deadPanel;
    public GameObject enemyName;
    public Image progressBar;
    public float percent;
    public List<LevelData> levelData = new();
    public PlayerData playerData;
    public GameObject killedText;
    public TextMeshProUGUI commentText;
    public GameObject open;
    public GameObject lockG;
    public GameObject circleBlack;
    public TextMeshProUGUI endGameGold;

    void OnEnable()
    {
        Projectile.playerDead += DeadPanel;
        PlayerWin.GameWin += DeadPanelWin;
    }

    void OnDisable()
    {
        Projectile.playerDead -= DeadPanel;
        PlayerWin.GameWin -= DeadPanelWin;
    }

    public void DeadPanel(GameObject enemy) {
        UpdatePercent();
        EnemyDetail enemyDetail = enemy.GetComponent<EnemyDetail>();
        commentText.SetText("You're doing good, try again to unlock next Zone.");
        if (enemyDetail != null) {
            enemyKillsText.SetText(enemyDetail.enemyName.text);
            playerPlacementText.SetText("#" + (EnemyManager.totalEnemies 
                + EnemyManager.instance.currentActiveEnemies.Count + 1)+"");
        } else {
            Debug.Log("Can't find");
        }
    }

    public void DeadPanelWin() {
        UpdatePercent();
        commentText.SetText("You've unlocked the 2nd zone");
        killedText.SetActive(false);
        playerPlacementText.SetText("#" + (EnemyManager.totalEnemies 
                + EnemyManager.instance.currentActiveEnemies.Count + 1)+"");
        enemyName.SetActive(false);
    }

    public void UpdatePercent() {
        percent = (1 - (EnemyManager.totalEnemies+ EnemyManager.instance.currentActiveEnemies.Count) /(float) levelData[playerData.level - 1].numsOfEnemies) * 100;
        
        if (playerData.enemyKilled < percent)
        playerData.enemyKilled = percent;

        if (percent == 100) {
            if (playerData.level == 2) {

            } else {
                playerData.level += 1;
                playerData.enemyKilled = 0;
            }
        }
        progressBar.fillAmount = percent / 100;

        if (playerData.level == 2) {
            open.SetActive(true);
            lockG.SetActive(false);
            circleBlack.GetComponent<Image>().color = new Color(114f / 255f, 255f / 255f, 0f / 255f);
        }

        endGameGold.SetText(GoldManager.instance.GetGameGold() +"");
        
        GoldManager.instance.SaveGold();

        inGamePanel.SetActive(false);
        deadPanel.SetActive(true);
    }
}
