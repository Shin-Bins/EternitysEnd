using UnityEngine;
using UnityEngine.Events;

public class TriggerSystem : MonoBehaviour
{
    public UnityEvent enterTrigger;
    public UnityEvent stayTrigger;
    public UnityEvent exitTrigger;

    void OnTriggerEnter(Collider other)
    {
        enterTrigger.Invoke();
    }
      void OnTriggerStay(Collider other)
    {
        stayTrigger.Invoke();
    }
      void OnTriggerExit(Collider other)
    {
        exitTrigger.Invoke();
    }
}
