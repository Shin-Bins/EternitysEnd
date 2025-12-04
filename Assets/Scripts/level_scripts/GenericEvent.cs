using UnityEngine;
using UnityEngine.Events;

public class GenericEvent : MonoBehaviour
{
    [SerializeField] UnityEvent triggerEntered;
    [SerializeField] UnityEvent triggerStayed;
    [SerializeField] UnityEvent triggerExited;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerEntered.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        triggerStayed.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        triggerExited.Invoke();
    }
}
