using System.Collections.Generic;
using UnityEngine;

public class EnemyEquip : MonoBehaviour
{
    [SerializeField] private GameObject weaponContainer;
    [SerializeField] private GameObject hatContainer;
    [SerializeField] private GameObject wingContainer;
    [SerializeField] private GameObject tailContainer;
    [SerializeField] private GameObject pants;
    public GameObject skin;
    [SerializeField] private List<Material> pantMats;
    [SerializeField] private List<Material> skinMats;

    public List<WeaponData> weaponDatas;
    public WeaponData enemyWeapon;
    public ObjectPool projectilePool {get; private set;}
    private EnemyStateMachine enemyStateMachine;
    private static int colorIndex = 0;
    public GameObject currentWeapon;
    public GameObject currentHat;
    
    void Awake()
    {
        enemyStateMachine = GetComponent<EnemyStateMachine>();
    }
    void OnEnable()
    {
        GetRandomWeapon();
        GetProjectilePool();
        GetRandomPantMaterials();
        GetRandomSkinMats();
        GetRandomHat();
    }
    public void GetRandomWeapon() {
        // Get a random weapon
        int count = weaponDatas.Count;
        // from 0 to count - 1
        int random = Random.Range(0, count); 
        enemyWeapon = weaponDatas[random];
        // Equip weapon
        foreach (Transform weaponChild in weaponContainer.transform) {
            if (weaponChild.gameObject.name == enemyWeapon.weapon.gameObject.name) {
                currentWeapon = weaponChild.gameObject;
                currentWeapon.SetActive(true);
            }
        }
    }
    public void GetProjectilePool() {
        foreach (var pool in GameManager.instance.projectilesPool) {
            if (pool.GetPoolObj() == enemyWeapon.projectile.gameObject) {
                projectilePool = pool;
            }
        }
    }
    public void GetRandomPantMaterials() {
        int rand = Random.Range(0, pantMats.Count);
        Material pantMat = pantMats[rand];
        pants.GetComponent<Renderer>().material = pantMat;
    }
    public void GetRandomSkinMats() {
        skin.GetComponent<Renderer>().material = skinMats[colorIndex];
        colorIndex ++;
        if (colorIndex >= skinMats.Count) colorIndex = 0;
    }
    public void GetRandomHat() {
        int random = Random.Range(0, hatContainer.transform.childCount);
        currentHat = hatContainer.transform.GetChild(random).gameObject;
        currentHat.SetActive(true);
    }
    
    void OnDisable()
    {
        currentWeapon.SetActive(false);
        currentHat.SetActive(false);
    }
}