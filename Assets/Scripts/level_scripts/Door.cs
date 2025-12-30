using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform openPosition; //Where the door opens to
    private Vector3 openPositionVec;
    private float moveSpeed = 1f;
    [SerializeField]private int keysNeeded;
    public int keysCollected = 0;

    private bool isOpen = false;

    void Start()
    {
        keysCollected = 0;
        openPositionVec = openPosition.position;
    }

    void Update()
    {
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, openPosition.position) < 0.01f)
            {
                isOpen = false;
            }
        }
    }
    public void KeyCollected()
    {
        keysCollected++;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("phiast") && keysCollected == keysNeeded)
        {
           isOpen = true;
        }
    }
}
