using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public ParticleSystem smokeParticle;

    private Renderer rockRenderer;
    private Collider rockCollider;
    private Rigidbody rockRB;

    // Start is called before the first frame update
    void Start()
    {
        rockRenderer = GetComponent<Renderer>();
        rockCollider = GetComponent<Collider>();
        rockRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
