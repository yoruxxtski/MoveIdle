using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour
{
    public static StartGameManager instance;
    [SerializeField] private GameObject progressStar;
    [SerializeField] private GameObject goldPanel;
    [SerializeField] private GameObject soundPanel;
    [SerializeField] private GameObject namePanel;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private GameObject weaponButton;
    [SerializeField] private GameObject skinButton;
    public TextMeshProUGUI zoneText;
    public PlayerData playerData;
    public List<LevelData> levelData = new();
    public GameObject startwo;
    public GameObject starone;
    public GameObject soundOn;
    public GameObject soundOff;
    public Image progress;
    public GameObject weaponShop;
    public GameObject pantShop;
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
        zoneText.SetText("Zone " + playerData.level + " - " + "BEST #" + ((1 - playerData.enemyKilled / 100) * (float) (levelData[playerData.level - 1].numsOfEnemies) + 1));
        if (playerData.level == 1) {
            starone.SetActive(true);
            startwo.SetActive(false);
        } else {
            starone.SetActive(false);
            startwo.SetActive(true);
        }
        progress.fillAmount = playerData.enemyKilled / 100;
    }

    public void GoOut() {
        progressStar.GetComponent<Animator>().SetTrigger("isOut");
        goldPanel.GetComponent<Animator>().SetTrigger("isOut");
        soundPanel.GetComponent<Animator>().SetTrigger("isOut");
        namePanel.GetComponent<Animator>().SetTrigger("isOut");
        playPanel.GetComponent<Animator>().SetTrigger("isOut");
        weaponButton.GetComponent<Animator>().SetTrigger("isOut");
        skinButton.GetComponent<Animator>().SetTrigger("isOut");
    } 
    public void GoIn() {
        progressStar.GetComponent<Animator>().SetTrigger("isIn");
        goldPanel.GetComponent<Animator>().SetTrigger("isIn");
        soundPanel.GetComponent<Animator>().SetTrigger("isIn");
        namePanel.GetComponent<Animator>().SetTrigger("isIn");
        playPanel.GetComponent<Animator>().SetTrigger("isIn");
        weaponButton.GetComponent<Animator>().SetTrigger("isIn");
        skinButton.GetComponent<Animator>().SetTrigger("isIn");
    }

    public void TurnOnOffMusic() {
        if (soundOn.activeInHierarchy && !soundOff.activeInHierarchy) {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
            AudioManager.instance.GetComponent<AudioSource>().Stop();
        } 
        else if (!soundOn.activeInHierarchy && soundOff.activeInHierarchy) {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
            AudioManager.instance.GetComponent<AudioSource>().Play();
        }
    }
    public void OpenShop() {
        weaponShop.SetActive(true);
    }
    public void CloseShop() {
        weaponShop.SetActive(false);
    }
    public void OpenPantShop() {
        pantShop.SetActive(true);
    }
    public void ClosePantShop() {
        pantShop.SetActive(false);
    }
}
