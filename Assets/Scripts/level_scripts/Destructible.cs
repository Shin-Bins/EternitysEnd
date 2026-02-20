using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedModel;
    public GameObject destructionVFX;
    public AudioClip destAud;
    public AudioSource src;

    public void DestructObject()
    {
        if(destroyedModel != null && src != null)
        {
            Instantiate(destroyedModel, transform.position, transform.rotation);
        }
        if(destructionVFX != null && src != null)
        {
            Instantiate(destructionVFX, transform.position, transform.rotation);
        }
        if(destAud!=null && src != null)
        {
            //AudioSource.PlayClipAtPoint(destAud, transform.position);
            src.PlayOneShot(destAud);
        }
        Destroy(gameObject);
    }
}
