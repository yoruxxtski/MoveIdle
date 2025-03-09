using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject poolObject;
    [SerializeField] private int numCreated = 20;
    private List<GameObject> createdObjects = new();

    //? keep tabs of current object
    private int index;

    void Awake()
    {
        for (int i = 0; i < numCreated; i++) {
            CreateObject();
        }
    }

    /*
        ? Create a new object, add to the list & return the object created
    */
    public GameObject CreateObject() {
        GameObject createdObj = Instantiate(poolObject, transform);
        createdObjects.Add(createdObj);
        return createdObj;
    }
    /*
        ? Get an object that is inactive, active that. If every obj is active -> create new obj
        ? return the first game object that is inactive in hierachy
        ? use index to the next time that check start at the index of last check
    */
    public GameObject GetObject() {
        for (int i = 0; i < createdObjects.Count; i++) {
            if (!createdObjects[index].activeInHierarchy) {
                return createdObjects[index];
            }
            index ++;
            if (index >= createdObjects.Count) index = 0;
        }
        return CreateObject();
    }

    public GameObject GetPoolObj() {
        return poolObject;
    }
}
