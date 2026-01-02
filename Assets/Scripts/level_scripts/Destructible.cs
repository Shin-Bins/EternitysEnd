using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedModel;
    public GameObject destructionVFX;

    public void DestructObject()
    {
        if(destroyedModel != null && destructionVFX != null)
        {
            Instantiate(destroyedModel, transform.position, transform.rotation);
            Instantiate(destructionVFX, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
