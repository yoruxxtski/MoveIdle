using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowIndicator : MonoBehaviour
{
    private GameObject arrow;
    public static Dictionary<GameObject, GameObject> arrow_Enemy = new(); 
    private EnemyEquip enemyEquip;
    private EnemyDetail enemyDetail;

    void Awake()
    {
        enemyEquip = GetComponent<EnemyEquip>();
        enemyDetail = GetComponent<EnemyDetail>();
    }

    void OnEnable()
    {
        if (!arrow_Enemy.ContainsKey(this.gameObject)) {
            // case : if this enemy does not have an arrow
            for (int i = 0; i < GameManager.instance.arrowPool.createdObjects.Count; i++) {
                if (!arrow_Enemy.ContainsValue(GameManager.instance.arrowPool.createdObjects[i])) {
                    arrow = GameManager.instance.arrowPool.createdObjects[i];
                    arrow_Enemy[this.gameObject] = arrow;
                    return;
                }
            } 
        } else {
            arrow = arrow_Enemy[this.gameObject];
        }
    }

    void Update()
    {
        if (arrow == null) {
            return;
        }
        Vector3 enemy_ViewPort = Camera.main.WorldToViewportPoint(transform.position);
        bool isOffScreen = enemy_ViewPort.x < 0 || enemy_ViewPort.x > 1 || enemy_ViewPort.y < 0 || enemy_ViewPort.y > 1;
        if (isOffScreen) {
            arrow.GetComponent<ArrowStats>().arrowImage.GetComponent<Image>().color 
                = enemyEquip.skin.GetComponent<Renderer>().material.color;
            arrow.GetComponent<ArrowStats>().levelImage.GetComponent<Image>().color 
                = enemyEquip.skin.GetComponent<Renderer>().material.color;
            arrow.GetComponent<ArrowStats>().arrowLevel.SetText(enemyDetail.level + "");
            arrow.transform.position = Camera.main.ViewportToWorldPoint(new Vector3 (0, 0, 0));
            arrow.GetComponent<ArrowMovement>().enemy = this.gameObject;
            arrow.SetActive(true);    
        } else {
            arrow.SetActive(false);
        }
    }
    void OnDisable()
    {
        if (arrow != null) arrow.SetActive(false);
    }
}