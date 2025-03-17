using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    public TMP_InputField nameInputField;  // Reference to the Input Field
    [SerializeField] private TextMeshProUGUI playerName;

    void Start()
    {
        // Load saved name if available
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            string savedName = PlayerPrefs.GetString("PlayerName");
            nameInputField.text = savedName;
            playerName.SetText(savedName);
        } else {
            playerName.SetText("YOU");
        }
    }

    public void OnNameChanged()
    {
        string newName = nameInputField.text;
        nameInputField.text = newName; // Update display text
        PlayerPrefs.SetString("PlayerName", newName); // Save name
        PlayerPrefs.Save();
    }
}
