using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float vfxDuration = 1f;
    private float countDown;
    private bool isActive = false;
    private bool hasExploded = false;
    public GameObject explosionEffect;


    
    void Start()
    {
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive == true)
        {
            countDown -= Time.deltaTime;
        
            if(countDown <= 0f && !hasExploded)
            {
              Explode();
              hasExploded = true;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Spawner"))
        {
            isActive = false;
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Spawner"))
        {
            isActive = true;
        }
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
}
