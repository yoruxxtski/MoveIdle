using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagStill : MonoBehaviour
{
    private Camera mainCam;
    void Awake()
    {
        mainCam = Camera.main;
    }
    void Update()
    {
        transform.LookAt(- mainCam.transform.position - transform.forward);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
