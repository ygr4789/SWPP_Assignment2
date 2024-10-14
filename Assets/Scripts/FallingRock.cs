using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public ParticleSystem smokeParticle;

    private Renderer rockRenderer;
    private Collider rockCollider;
    private Rigidbody rockRB;

    private float size = 0.0f;
    private float delay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        rockRenderer = GetComponent<Renderer>();
        rockCollider = GetComponent<Collider>();
        rockRB = GetComponent<Rigidbody>();
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        if(size < 1.0f)
        {
            size += Time.fixedDeltaTime / delay;
            rockRB.velocity = Vector3.zero;
        }
        else
        {
            size = 1.0f;
        }
        transform.localScale = new Vector3(size, size, size);
    }

    private void OnCollisionEnter(Collision collision)
    {
        smokeParticle.Play();
        rockRenderer.enabled = false;
        rockCollider.enabled = false;
        rockRB.isKinematic = true;

        Destroy(gameObject, 3.0f);
    }
}
