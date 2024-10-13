using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBomb : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public GameObject hitBox;
    public float ignitionTime = 2.0f;

    private Renderer bombRenderer;
    private Collider bombCollider;

    // Start is called before the first frame update
    void Start()
    {
        bombRenderer = GetComponent<Renderer>();
        bombCollider = GetComponent<Collider>();
        StartCoroutine(Ignite(ignitionTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Ignite(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        explosionParticle.Play();
        bombRenderer.enabled = false;
        bombCollider.enabled = false;

        hitBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitBox.SetActive(false);

        Destroy(gameObject, 1.0f);
    }
}
