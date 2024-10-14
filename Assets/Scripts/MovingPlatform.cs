using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float patrolPeriod = 2.0f;
    public Vector3 destinationOffest = 4.0f * Vector3.up;

    private float t;
    private int increase = 1;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        t += (Time.deltaTime / patrolPeriod) * increase;
        transform.position = initialPosition + t * destinationOffest;

        float x = transform.position.x;
        if (t < 0) increase = 1;
        if (t > 1) increase = -1;
    }
}
