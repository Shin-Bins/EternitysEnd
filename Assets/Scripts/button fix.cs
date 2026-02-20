using UnityEngine;
using UnityEngine.EventSystems;

public class buttonfix : MonoBehaviour
{
    void Awake()
    {
        if (FindObjectsByType<EventSystem>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
