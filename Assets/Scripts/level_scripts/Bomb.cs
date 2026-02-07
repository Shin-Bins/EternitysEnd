using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomb : MonoBehaviour
{
    [SerializeField]private float delay = 3f;
    [SerializeField]private float radius = 5f;
    [SerializeField]private float vfxDuration = 1f;
    private float countDown;
    public bool isActive = false;
    [SerializeField]private bool hasExploded = false;
    public GameObject explosionEffect;

    private PickUpSkull holdScript;

    private AudioSource src;
    public AudioClip countdownAud;
    public AudioClip boomAud;

    void Start()
    {
        src = GetComponent<AudioSource>();
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == true)
        {
            countDown -= Time.deltaTime;

            if(countDown > 0 && !src.isPlaying && isActive)
            {
                   src.clip = countdownAud;
                   src.volume = 1f;
                   src.Play();
            }
            if(countDown <= 0f && !hasExploded)
            {
              Explode();
              hasExploded = true;
            }
        }
           if(!isActive)
           {
                src.clip = null;
                src.Stop();
           }
    }

    public void SetHoldScript(PickUpSkull script)
    {
        holdScript = script;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Spawner"))
        {
            isActive = false;
            countDown = delay;
            src.Stop();
        }
        else{
            isActive = true;
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Spawner") && !isActive)
        {
            Activate();
        }
    }

    void Activate()
    {
        if(isActive) return;
        isActive = true;       
    }

    void Explode()
    {

        src.Stop();
        AudioSource.PlayClipAtPoint(boomAud, transform.position);

        if(explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(explosion,vfxDuration);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if(dest != null)
            {
                dest.DestructObject();
            }
        }
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if(holdScript != null)
        {
            holdScript.Drop();
        }
    }
}
