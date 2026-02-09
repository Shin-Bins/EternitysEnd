using UnityEngine;

public class PhiastKiller : MonoBehaviour
{

    public Transform bossTarget;
    public AudioClip fire;
    private float speed = 20f;
    private bool shoot = false;

    void Update()
    {
        if(shoot)
        {
            Vector3 direction = (bossTarget.position - transform.position).normalized;
            
            transform.position += direction * speed * Time.deltaTime; 
        }
    }
    public void Activate()
    {
        if(!shoot)
        {
            AudioSource.PlayClipAtPoint(fire, transform.position);
            shoot = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if(other.CompareTag("Boss"))
        {
            BossStats boss = other.GetComponent<BossStats>();
            {
                Debug.Log("Damaged");
                boss.Damaged();
            }
            Destroy(gameObject);
        }
    }
}
