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

    [SerializeField] float flashInterval = 0.2f;
    private Color activeFlash = Color.red;
    private Renderer rend;
    private Color originalColour;

    private PickUpSkull holdScript;
    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColour = rend.material.color;
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == true)
        {
            countDown -= Time.deltaTime;
            StartCoroutine(FlashEffect());
        
            if(countDown <= 0f && !hasExploded)
            {
              Explode();
              hasExploded = true;
            }
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
        }
        else{
            isActive = true;
        }
        if(collision.gameObject.CompareTag("Boss"))
        {
            BossStats boss = collision.gameObject.GetComponent<BossStats>();
            {
                boss.Damaged();
            }
            Explode();
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Spawner"))
        {
            isActive = true;
        }
    }

    IEnumerator FlashEffect()
    {
        float elapsed = 0f;
        while (elapsed < delay)
        {
            rend.material.color = activeFlash;
            yield return new WaitForSeconds(flashInterval);
            
            rend.material.color = originalColour;
            yield return new WaitForSeconds(flashInterval);
            
            elapsed += flashInterval * 2;
        }        
        //resets material and bool
        rend.material.color = originalColour;
    }

    void Explode()
    {
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
