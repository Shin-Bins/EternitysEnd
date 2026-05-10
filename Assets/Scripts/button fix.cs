using UnityEngine;
using UnityEngine.EventSystems;

public class buttonfix : MonoBehaviour
{
    void Awake()
    {
        EventSystem[] systems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);//Whole script just to stop multiple event systems breaking the ui
        if (systems.Length > 1)
        {
            foreach (EventSystem es in systems)
            {
                if (es != this.GetComponent<EventSystem>())
                {
                    Destroy(es.gameObject);
                    return;
                }
            }
        }
    }
}