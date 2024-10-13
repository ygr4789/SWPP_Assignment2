using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public bool collected = false;

    private Renderer mRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collect()
    {
        mRenderer.enabled = false;
        explosionParticle.Play();
        collected = true;
    }
}
