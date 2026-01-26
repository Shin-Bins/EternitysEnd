using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedModel;
    public GameObject destructionVFX;

    public void DestructObject()
    {
        if(destroyedModel != null)
        {
            Instantiate(destroyedModel, transform.position, transform.rotation);
        }
        if(destructionVFX != null)
        {
            Instantiate(destructionVFX, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
