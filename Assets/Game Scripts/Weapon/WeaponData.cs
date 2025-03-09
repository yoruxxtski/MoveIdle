using UnityEngine;
public enum weaponType {
        comeback,  
        topRotate
}
[CreateAssetMenu(menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public weaponType weaponType;
    public GameObject weapon;
    public Projectile projectile;
}