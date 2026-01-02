using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bomba;
    private bool noBomba = false;
    public float spawnDelay = 3f;
    private float countDown;

void OnTriggerExit(Collider other)
{
    if(other.CompareTag("PickUp"))
    {
        noBomba = true;
    }
}

void Start()
{
    countDown = spawnDelay;
}

void Update()
{
    if(noBomba)
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0f)
        {
            Instantiate(bomba, transform.position, transform.rotation);
            noBomba = false;
        }
    }
}
}
