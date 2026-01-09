using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bomba;
    public bool noBomba = false;
    public float spawnDelay = 3f;
    private float countDown;


void Start()
{
    countDown = spawnDelay;
}

void Update()
{
     // Check if there's no bomb at this position
    Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
    bool bombHere = false;
    
    foreach(Collider col in colliders)
    {
        if(col.CompareTag("PickUp"))
        {
            bombHere = true;
            break;
        }
    }
    
    if(!bombHere &&!noBomba)
    {
        noBomba = true;
        countDown = spawnDelay;
    }

    if(noBomba)
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0f)
        {
            Instantiate(bomba, transform.position, transform.rotation);
            noBomba = false;
            countDown = spawnDelay;
        }
    }
}
}
