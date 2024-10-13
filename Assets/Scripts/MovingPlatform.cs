using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float patrolPeriod = 2.0f;
    public float patrolDistance = 5.0f;
    private bool moveRightFirst = true;

    private int dirX;
    private float leftBound;
    private float rightBound;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        dirX = moveRightFirst ? 1 : -1;
        float initX = transform.position.x;
        float lastX = initX + patrolDistance * dirX;
        leftBound = Mathf.Min(initX, lastX);
        rightBound = Mathf.Max(initX, lastX);
        speed = patrolDistance / patrolPeriod;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Time.deltaTime * speed * dirX;
        transform.position += new Vector3(dx, 0.0f, 0.0f);

        float x = transform.position.x;
        if (x < leftBound) dirX = 1;
        if (x > rightBound) dirX = -1;
    }
}
