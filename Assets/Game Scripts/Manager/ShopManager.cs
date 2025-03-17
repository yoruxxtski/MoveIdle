using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public List<WeaponData> weapons; // Assign WeaponData assets in Inspector
    public Image weaponImage; // UI Image to show weapon
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI priceText;
    public Button buyButton;
    public Button equipButton;
    private int currentIndex = 0;
    public TextMeshProUGUI equipButtonText; // Reference to Equip button text
    public static event Action weaponEquipped;

    void Start()
    {
        foreach (WeaponData weapon in weapons)
        {
            PlayerPrefs.SetInt(weapon.weaponName, weapon.isUnlocked ? 1 : 0);
        }

        UpdateUI();
    }

     public void NextWeapon()
    {
        currentIndex = (currentIndex + 1) % weapons.Count; // Cycle forward
        UpdateUI();
    }

    public void PreviousWeapon()
    {
        currentIndex = (currentIndex - 1 + weapons.Count) % weapons.Count; // Cycle backward
        UpdateUI();
    }

     void UpdateUI()
    {
        WeaponData weapon = weapons[currentIndex];

        weaponImage.sprite = weapon.weaponIcon;

        weaponNameText.text = weapon.weaponName;

        priceText.text = weapon.isUnlocked ? "Unlocked" : "Price: " + weapon.price + " Gold";

        buyButton.gameObject.SetActive(!weapon.isUnlocked); // Show Buy button if locked

        equipButton.gameObject.SetActive(weapon.isUnlocked); // Show Equip button if unlocked

        string equippedWeapon = PlayerPrefs.GetString("EquippedWeapon");

        bool isEquipped = weapon.weaponName == equippedWeapon;
        
        equipButtonText.text = isEquipped ? "Equipped" : "Equip";

        equipButton.interactable = !isEquipped; // Disable the button if already equipped
    }


     public void BuyWeapon()
    {
        WeaponData weapon = weapons[currentIndex];

        int playerGold = PlayerPrefs.GetInt("TotalGold", 0); // Load player's gold

        if (playerGold >= weapon.price)
        {
            playerGold -= weapon.price;
            PlayerPrefs.SetInt("TotalGold", playerGold);
            
            GoldManager.instance.LoadGold();

            weapon.isUnlocked = true;

            PlayerPrefs.SetInt(weapon.weaponName, 1); // Save unlock state
            PlayerPrefs.Save(); // Save changes immediately

            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    public void EquipWeapon()
    {
        WeaponData weapon = weapons[currentIndex];
        if (!weapon.isUnlocked) return;
        // Save equipped weapon
        PlayerPrefs.SetString("EquippedWeapon", weapon.weaponName);
        PlayerPrefs.Save();
        weaponEquipped?.Invoke();
        UpdateUI(); // Refresh UI to update button text
    }

}