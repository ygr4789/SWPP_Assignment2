using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float initialDelay = 0.0f;
    public float genInterval = 7.0f;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnObject", initialDelay, genInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnObject()
    {
        Instantiate(prefab, transform);
    }
}
