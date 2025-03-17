using System;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public Transform rotatePart;
    public GameObject enemy;
    void Update()
    {
        if (enemy != null) {
            float screenPadding = 40f;
            Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);

             // Fix issue where WorldToScreenPoint flips X/Y when behind camera
            if (targetScreenPos.z < 0)
            {
                // If behind the camera, flip X and Y to correct the projection
                targetScreenPos.x = Screen.width - targetScreenPos.x;
                targetScreenPos.y = Screen.height - targetScreenPos.y;
            }

            Vector3 fromPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            Vector3 dir = (targetScreenPos - fromPos).normalized;
            float angle = Mathf.Atan2(- dir.x, dir.y) * Mathf.Rad2Deg;

            bool isOffScreen = targetScreenPos.x <= 0 || targetScreenPos.x >= Screen.width 
                || targetScreenPos.y  <= 0 || targetScreenPos.y >= Screen.height;

            Vector3 targetPos = targetScreenPos;
            if (isOffScreen) {
                if (targetPos.x <= 0) {
                    targetPos.x = screenPadding;
                }
                if (targetPos.x >= Screen.width) {
                    targetPos.x = Screen.width - screenPadding;
                }
                if (targetPos.y <= 0) {
                    targetPos.y = screenPadding;
                }
                if (targetPos.y >= Screen.height) {
                    targetPos.y = Screen.height - screenPadding;;
                }
            }
            transform.position = targetPos;
            rotatePart.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
    void OnDisable()
    {
        enemy = null;
    }
    
}