using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public ObjectPool projectilePool {get; private set;}

    void Awake()
    {
        Equip();
    }
    /*
        TODO : instantiate weapon in weapon container, instantiate head, instantiate pant mat
    */
    public void Equip() {
        AssignWeapon();
    }
    public void AssignWeapon() {
        // TODO : instantiate weapon in container
        foreach (var pool in GameManager.instance.projectilesPool) {
            if (pool.GetPoolObj() == playerData.weaponData.projectile.gameObject) {
                projectilePool = pool;
            }
        }
    }
}