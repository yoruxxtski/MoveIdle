using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private WeaponData[] allWeapons;
    public Pant[] allPants;
    public Transform playerWeaponContainer;
    public GameObject playerLevel;
    public GameObject circleDetection;
    public ObjectPool projectilePool {get; private set;}
    public GameObject pantContainer;
    void OnEnable()
    {
        GameStateManager.StartGamePlayer += EnablePlayer;
        ShopManager.weaponEquipped += AssignWeapon;
        PantShopManager.pantEquipped += AssignPant;
    }

    void OnDisable()
    {
        GameStateManager.StartGamePlayer -= EnablePlayer;
        ShopManager.weaponEquipped -= AssignWeapon;
        PantShopManager.pantEquipped -= AssignPant;
    }
    void Awake()
    {
        if (PlayerPrefs.GetString("EquippedWeapon") == "") {
            PlayerPrefs.SetString("EquippedWeapon", "Axe");
        }
    }

    void Start()
    {
        AssignWeapon();
    }

    public void EnablePlayer() {
        playerLevel.SetActive(true);
        circleDetection.SetActive(true);
    }
    
    
    /*
        TODO : instantiate weapon in weapon container, instantiate head, instantiate pant mat
    */
    public void AssignWeapon() {

        // TODO : instantiate weapon in container
        foreach (WeaponData weaponData in allWeapons) {
            if (weaponData.weaponName == PlayerPrefs.GetString("EquippedWeapon")) {
                playerData.weaponData = weaponData;
            }
        }

        foreach (Transform child in playerWeaponContainer) {
            if (child.gameObject.name == playerData.weaponData.weapon.name) {
                child.gameObject.SetActive(true);
            } else {
                child.gameObject.SetActive(false);
            }
        }

        foreach (var pool in GameManager.instance.projectilesPool) {
            if (pool.GetPoolObj() == playerData.weaponData.projectile.gameObject) {
                projectilePool = pool;
            }
        }
    }

    public void AssignPant() {
        foreach(Pant pant in allPants) {
            if (pant.pantName == PlayerPrefs.GetString("EquippedPant")) {
                playerData.pants = pant.pantMat;
            }
        }
        pantContainer.GetComponent<SkinnedMeshRenderer>().material = playerData.pants;
    }
}