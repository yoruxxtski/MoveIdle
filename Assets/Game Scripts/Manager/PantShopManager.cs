using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PantShopManager : MonoBehaviour
{
    public List<Pant> pants = new();
    public Image pantImage;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI pantNameText;
    public Button buyButton;
    public Button equipButton;
    private int currentIndex = 0;
    public TextMeshProUGUI equipButtonText; // Reference to Equip button text
    public static event Action pantEquipped;
    void Start() {
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.Save();
        UpdateUI();
    }

    void UpdateUI() {
        Pant pant = pants[currentIndex];

        pantImage.sprite = TextureToSprite(pant.pantTexture);

        pantNameText.text = pant.pantName;

        priceText.text = pant.isUnlocked ? "Unlocked" : "Price: " + pant.price + " Gold";

        buyButton.gameObject.SetActive(!pant.isUnlocked); // Show Buy button if locked

        equipButton.gameObject.SetActive(pant.isUnlocked); // Show Equip button if unlocked

        string equippedPant = PlayerPrefs.GetString("EquippedPant");

        bool isEquipped = pant.pantName == equippedPant;
        
        equipButtonText.text = isEquipped ? "Equipped" : "Equip";
        
        equipButton.interactable = !isEquipped; // Disable the button if already equipped
    }
     public void NextPant()
    {
        currentIndex = (currentIndex + 1) % pants.Count; // Cycle forward
        UpdateUI();
    }

    public void PreviousPant()
    {
        currentIndex = (currentIndex - 1 + pants.Count) % pants.Count; // Cycle backward
        UpdateUI();
    }

    public void BuyPant() {
        Pant pant = pants[currentIndex];
        int playerGold = PlayerPrefs.GetInt("TotalGold", 0); // Load player's gold
        if (playerGold >= pant.price)
        {
            playerGold -= pant.price;
            PlayerPrefs.SetInt("TotalGold", playerGold);
            GoldManager.instance.LoadGold();

            pant.isUnlocked = true;

            PlayerPrefs.SetInt(pant.pantName, 1); // Save unlock state
            PlayerPrefs.Save(); // Save changes immediately
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

     public void EquipPant()
    {
        Pant pant = pants[currentIndex];
        if (!pant.isUnlocked) return;
        // Save equipped weapon
        PlayerPrefs.SetString("EquippedPant", pant.pantName);
        PlayerPrefs.Save();
        pantEquipped?.Invoke();
        UpdateUI(); // Refresh UI to update button text
    }

     private Sprite TextureToSprite(Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }
}