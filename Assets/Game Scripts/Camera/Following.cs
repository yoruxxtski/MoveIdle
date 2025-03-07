using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;
    private float camSpeed = 1.5f;

    /* 
        ? Following player after player has moved in FixedUpdate (physics) 
        ? using Lerp Lerp(a, b, t) = a + (b - a) * t, using clamp to force it to be (0,1)
        TODO : When add game start to game manager -> only do this func when game start
    */
    void LateUpdate()
    {
        Vector3 targetPos = target.transform.position;

        transform.position = Vector3.Lerp(
            transform.position, 
            targetPos + offset,
            Mathf.Clamp01(camSpeed * Time.deltaTime)
        );
        transform.LookAt(targetPos);
    }
}
