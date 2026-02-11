using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedModel;
    public GameObject destructionVFX;
    public AudioClip destAud;

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
        if(destAud!=null)
        {
            AudioSource.PlayClipAtPoint(destAud, transform.position);
        }
        Destroy(gameObject);
    }
}
