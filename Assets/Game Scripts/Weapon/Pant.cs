using UnityEngine;

[CreateAssetMenu(menuName = "Pant")]
public class Pant : ScriptableObject
{
    public string pantName;
    public Material pantMat;
    public Texture2D pantTexture;
    public int price;
    public bool isUnlocked;
}